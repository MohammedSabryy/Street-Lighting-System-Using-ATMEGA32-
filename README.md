1.	Project Objective:  
  
  	The Project is a street light control system using Atmega32,    	We have two modes that can a user select between them:  
  
*	Manual mode: The microcontroller sends the intensity of the light that is detected by the LDR sensor to the GUI then Gui decides to turn on or not the streetlight based on the value of the light intensity sent.  
  
*	Automatic mode: The microcontroller sends if there is movement of the vehicles detected by the IR sensor to Gui to set a fixed time to turn on lights and then turn them off automatically.     

    
2.	System Block Diagram:  
2.1.	Block Diagram:  
  
    
      
![image](https://github.com/user-attachments/assets/5293c0a1-f492-4c4b-bee9-4ee39fb0493f)

2.2.	Block Diagram Description:  

ATmega32
![image](https://github.com/user-attachments/assets/656698e5-8337-4716-a8e4-9c70aadc618a)
  The ATmega32 provides the following features: 32Kbytes of In-System Programmable Flash Program memory with Read-While-Write capabilities, 1024bytes EEPROM, 2Kbyte SRAM, 32 general purpose I/O lines, 32 general purpose working registers, a JTAG interface for Boundaryscan, On-chip Debugging support and programming, three flexible Timer/Counters with compare modes, Internal and External Interrupts, a serial programmable USART, a byte oriented Two-wire Serial Interface, an 8-channel, 10-bit ADC with optional differential input stage with programmable gain (TQFP package only), a programmable Watchdog Timer with Internal Oscillator, an SPI serial port, and six software selectable power saving modes. The Idle mode stops the CPU while allowing the USART, Two-wire interface, A/D Converter, SRAM, Timer/Counters, SPI port, and interrupt system to continue functioning. The Power-down mode saves the register contents but freezes the Oscillator, disabling all other chip functions until the next External Interrupt or Hardware Reset. In Power-save mode, the Asynchronous Timer continues to run, allowing the user to maintain a timer base while the rest of the device is sleeping. The ADC Noise Reduction mode stops the CPU and all I/O modules except Asynchronous Timer and ADC, to minimize switching noise during ADC conversions. In Standby mode, the crystal/resonator Oscillator is running while the rest of the device is sleeping. This allows very fast start-up combined with low-power consumption. In Extended Standby mode, both the main Oscillator and the Asynchronous Timer continue to run.

-	GUI: the user interface through which our system is operated. When the manual mode is chosen, the user can manually control the lights by pressing the on and off buttons. When the automatic mode is chosen, the user can set a fixed time for LEDs to turn on and the GUI has an indicator to determine whether there is movement in the street.
-	
IR (infrared sensor)  
![image](https://github.com/user-attachments/assets/688391ee-8280-49fc-b153-45f3e9b14d24)
An infrared (IR) sensor is an electronic device that detects infrared radiation, typically emitted by objects in the form of heat or light. These sensors are widely used in various applications such as motion detection, temperature measurement, proximity sensing, and remote control systems. The basic principle behind IR sensors is that they detect the infrared light emitted by an object or reflected from it, converting this information into an electrical signal for further processing.
 
LDR (light-dependent sensor)  
![image](https://github.com/user-attachments/assets/b60fe83a-614a-4f19-8d71-8e3ce5ea3524)



An LDR (Light Dependent Resistor), also known as a photoresistor, is a type of resistor whose resistance changes in response to the amount of light falling on it. LDRs are widely used as light sensors in various applications, including light detection, light-sensitive circuits, and automatic lighting systems.
  In Darkness: The resistance of an LDR is very high, meaning it doesn't allow much current to flow through it.
 In Light: The resistance of the LDR decreases significantly, allowing more current to pass through
  
GUI
     the user interface through which our system is operated. When the manual mode is chosen, the user can manually control the lights by pressing the on and off buttons. When the automatic mode is chosen, the user can set a fixed time for LEDs to turn on and the GUI has an indicator to determine whether there is movement in the street.
  






  


The connections in our system:  
GUI switches between two modes, sending the active mode to the MCU. The MCU then takes the reads from the LDR sensor or the IR sensor to detect movement. When in manual mode, the MCU sends the GUI the light's intensity, and the GUI turns on or off the lights. In the automated mode, an IR sensor detects movement and sends the information to an MCU, which determines whether to turn on or off the LEDS and sends the information to a GUI to alter the indicator's color based on the IR read. The GUI then sets a fixed time for the LEDS to turn on and sends it to the MCU.       

MCU: microcontroller unit facilitates communication between GUI and sensors using (UART Protocol)  
  
Manual Mode: has two component LDR that detects the intensity of the light and LEDS that are controlled by GUI.   
  
Automatic Mode: has two component IR sensor that detect the movement of vehicles and LEDS that are controlled by MCU based on the IR sensor's reads.  

  
3.	Schematic Diagram (Circuit Diagram):  
  ![image](https://github.com/user-attachments/assets/11967f47-dda0-442e-863e-752f8f119ec2)
  
  
  
  4.	List Of Components:  
  
 
1  	Microcontroller  	Atmega32  	To provide communication between GUI and sensors and control the system  	1  
2  	Obstacle Sensor  	IR (infrared sensor)  	To detect the movement of vehicles  	1  
3  	Light Sensor  	LDR (light-dependent sensor)  	To sense the intensity of light 	 	1  
4  	diodes  	LEDS  	To act as lights in the street  	        1
5  	Resistors  	  	To protect LEDS and build circuits of IR and LDR  	2  


5.	Real-Time Hardware Photo:  

![image](https://github.com/user-attachments/assets/f5f5a179-0e34-4ea3-b1ad-3548804261e0)

  6.	GUI Screenshots:
![image](https://github.com/user-attachments/assets/5407aabd-1d7e-4e74-918e-3e2ce7fc7ef7)


![image](https://github.com/user-attachments/assets/a51c5c8d-b4bc-40fc-9478-0686e1add494)


![image](https://github.com/user-attachments/assets/524b31ba-2576-4e85-825b-f26c6c7e4fb4)















