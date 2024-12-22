#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdlib.h>
#include <stdio.h>

#define F_CPU 16000000UL

// UART Configuration
void UART_init(unsigned int baud) {
	unsigned int ubrr = F_CPU / 16 / baud - 1;
	UBRRH = (unsigned char)(ubrr >> 8);
	UBRRL = (unsigned char)ubrr;
	UCSRB = (1 << RXEN) | (1 << TXEN); // Enable receiver and transmitter
	UCSRC = (1 << URSEL) | (1 << UCSZ1) | (1 << UCSZ0); // 8-bit data
}

void UART_send(char data) {
	while (!(UCSRA & (1 << UDRE))); // Wait for empty transmit buffer
	UDR = data;
}

void UART_sendString(const char *str) {
	while (*str) {
		UART_send(*str++);
	}
}

char UART_receive(void) {
	while (!(UCSRA & (1 << RXC))); // Wait for data to be received
	return UDR;
}

// Custom delay function for variable delays
void variable_delay_ms(uint16_t delay) {
	while (delay--) {
		_delay_ms(1); // Call _delay_ms with a constant value
	}
}

// LED and Sensor Control
void LED_control(uint8_t state) {
	if (state) {
		PORTB |= (1 << PB0); // Turn on LED
		} else {
		PORTB &= ~(1 << PB0); // Turn off LED
	}
}

int read_LDR() {
	ADMUX = 0x40; // Select ADC0
	ADCSRA = 0x87; // Enable ADC with prescaler 128
	ADCSRA |= (1 << ADSC); // Start conversion
	while (ADCSRA & (1 << ADSC)); // Wait for conversion to complete
	return ADC;
}

int read_IR() {
	return (PIND & (1 << PD2)); // Read digital input from PD2 (IR sensor)
}

// Main Program
int main(void) {
	DDRB = 0xFF; // Set PORTB as output (LEDs)
	DDRD &= ~(1 << PD2); // Set PD2 as input (IR sensor)

	UART_init(9600); // Initialize UART
	sei(); // Enable global interrupts

	char mode = 'M'; // Default mode: Manual
	uint16_t led_on_time = 1000; // Default LED on time in ms
	int ldr_value;

	while (1) {
		if (UCSRA & (1 << RXC)) { // Check for received data
			char received = UART_receive();

			if (received == 'a') {
				mode = 'A'; // Automatic Mode
				} else if (received == 'b') {
				mode = 'M'; // Manual Mode
				} else if (received == 'c') {
				LED_control(1); // Turn on LED in Manual Mode
				} else if (received == 'd') {
				LED_control(0); // Turn off LED in Manual Mode
				} else if (received == 'r') {
				// Send LDR reading to GUI
				ldr_value = read_LDR();
				char buffer[10];
				sprintf(buffer, "L:%d\n", ldr_value);
				UART_sendString(buffer);
				} else if (received == 't') {
				// Receive time for LED in Automatic Mode
				char time_buffer[5];
				uint8_t index = 0;

				while (index < 5) {
					char time_char = UART_receive();
					if (time_char == '\n') break; // End of number
					time_buffer[index++] = time_char;
				}
				time_buffer[index] = '\0'; // Null-terminate the string
				led_on_time = atoi(time_buffer); // Convert to integer
			}
		}

		if (mode == 'A') { // Automatic Mode
			if (read_IR()) { // If motion is detected
				LED_control(1);
				variable_delay_ms(led_on_time); // Keep LED on for the specified time
				LED_control(0);
			}
		}
	}
}
