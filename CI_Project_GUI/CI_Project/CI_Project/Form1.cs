using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CI_Project
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        public Form1()
        {
            
            Console.WriteLine("Started");
            InitializeComponent();
            RefreshComPorts();
            numericUpDown1.Value = 1;
            label1.BackColor = Color.Gray;
            setAutoMode();
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
        }
        private void RefreshComPorts()
        {
            
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void ConnectToSelectedPort()
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid COM port.");
                return;
            }

            string selectedPort = comboBox1.SelectedItem.ToString();
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                serialPort = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                serialPort.DataReceived += serialPort_DataReceived;
                serialPort.Open();
                label1.Text = "Connected";
                label1.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to port {selectedPort}: {ex.Message}");
            }
        }


        private void DisconnectFromPort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                    serialPort = null; // Reset to avoid accidental reuse
                    label1.Text = "Disconnected";
                    label1.ForeColor = Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error disconnecting from port: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No port to disconnect.");
            }
        }

        private void setAutoMode()
        {
            button3.Enabled = true;
            button3.BackColor = Color.DarkOrange;
            numericUpDown1.Enabled = true;
            numericUpDown1.BackColor = Color.AntiqueWhite;
            label1.BackColor = Color.Yellow;
            label1.ForeColor = Color.Green;

            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            textBox1.Enabled = false;

            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button4.BackColor = Color.Gray;
            textBox1.BackColor = Color.Gray;
        }
        private void setManualMode()
        {
            button3.Enabled = false;
            button3.BackColor = Color.Gray;
            numericUpDown1.Enabled = false;
            numericUpDown1.BackColor = Color.Gray;
            label1.BackColor = Color.Gray;
            label1.ForeColor = Color.Green;

            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            textBox1.Enabled = false;

            button1.BackColor = Color.DarkOrange;
            button2.BackColor = Color.DarkOrange;
            button4.BackColor = Color.DarkOrange;
            textBox1.BackColor = Color.AntiqueWhite;
            //label1.BackColor = Color.Gray;
        }
        //private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    string indata = serialPort.ReadExisting(); // Read all data from the serial port

        //    if (indata.Contains("L:")) // Check if the data contains "L:"
        //    {
        //        string[] lines = indata.Split('\n'); // Split the received data into lines
        //        foreach (var line in lines)
        //        {
        //            if (line.StartsWith("L:")) // Look for a line starting with "L:"
        //            {
        //                try
        //                {
        //                    // Extract the LDR value after "L:"
        //                    int ldrValue = int.Parse(line);
        //                    Invoke(new Action(() =>
        //                    {
        //                        textBox1.Text = ldrValue.ToString(); // Update Textbox1 with the LDR value
        //                    }));
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Handle any parsing errors
        //                    MessageBox.Show($"Error parsing LDR value: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    MessageBox.Show(indata);
        //                }
        //            }
        //        }
        //    }
        //}
        //private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    string indata = serialPort.ReadExisting(); // Read all data from the serial port
        //    Console.WriteLine(indata); // Log the raw data for debugging

        //    if (indata.Contains("L:")) // Check if the data contains "L:"
        //    {
        //        string[] lines = indata.Split('\n'); // Split the received data into lines
        //        foreach (var line in lines)
        //        {
        //            if (line.StartsWith("L:")) // Look for a line starting with "L:"
        //            {
        //                string ldrValueString = line.Substring(2); // Extract the value after "L:"
        //                if (int.TryParse(ldrValueString, out int ldrValue)) // Safe parsing
        //                {
        //                    Invoke(new Action(() =>
        //                    {
        //                        textBox1.Text = ldrValue.ToString(); // Update Textbox1 with the LDR value
        //                    }));
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"Invalid data: {line}");
        //                }
        //            }
        //        }
        //    }
        //}

        //private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        string indata = serialPort.ReadExisting();
        //        Console.WriteLine($"Raw Data Received: {indata}");

        //        string[] lines = indata.Split('\n'); // Split the received data into lines
        //        foreach (var line in lines)
        //        {
        //            Console.WriteLine($"Processing Line: {line}");
        //            if (line.StartsWith("L:") || line.StartsWith("l:"))
        //            {
        //                string ldrValueString = line.Substring(2);
        //                if (int.TryParse(ldrValueString, out int ldrValue))
        //                {
        //                    Invoke(new Action(() =>
        //                    {
        //                        textBox1.Text = ldrValue.ToString();
        //                    }));
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"Invalid data format: {line}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in DataReceived handler: {ex.Message}");
        //    }
        //}

        private StringBuilder dataBuffer = new StringBuilder(); // Buffer to accumulate data

        //private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        string indata = serialPort.ReadExisting(); // Read all available data
        //        dataBuffer.Append(indata); // Append to the buffer

        //        // Process complete lines
        //        string[] lines = dataBuffer.ToString().Split('\n'); // Split buffer into lines
        //        for (int i = 0; i < lines.Length - 1; i++) // Process all complete lines
        //        {
        //            string line = lines[i].Trim(); // Get a complete line
        //            Console.WriteLine($"Processing Line: {line}");

        //            if (line.StartsWith("L:"))
        //            {
        //                string ldrValueString = line.Substring(2); // Extract the value after "L:"
        //                if (int.TryParse(ldrValueString, out int ldrValue))
        //                {
        //                    Invoke(new Action(() =>
        //                    {
        //                        textBox1.Text = ldrValue.ToString(); // Update the TextBox with the LDR value
        //                    }));
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"Invalid data format: {line}");
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Ignoring irrelevant line: {line}");
        //            }
        //        }

        //        // Keep any remaining incomplete data in the buffer
        //        dataBuffer.Clear();
        //        if (!string.IsNullOrEmpty(lines[^1]))
        //        {
        //            dataBuffer.Append(lines[^1]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in DataReceived handler: {ex.Message}");
        //    }
        private void serialPort_DataReceived    (object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string indata = serialPort.ReadExisting(); // Read all available data
                dataBuffer.Append(indata); // Append to the buffer

                // Process complete lines
                string[] lines = dataBuffer.ToString().Split('\n'); // Split buffer into lines
                for (int i = 0; i < lines.Length - 1; i++) // Process all complete lines
                {
                    string line = lines[i].Trim(); // Get a complete line
                    Console.WriteLine($"Processing Line: {line}");

                    if (line.StartsWith("L:", StringComparison.OrdinalIgnoreCase))
                    {
                        string ldrValueString = line.Substring(2); // Extract the value after "L:"
                        if (int.TryParse(ldrValueString, out int ldrValue))
                        {
                            Invoke(new Action(() =>
                            {
                                textBox1.Text = ldrValue.ToString(); // Update the TextBox with the LDR value
                            }));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid data format: {line}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ignoring irrelevant line: {line}");
                    }
                }

                // Keep any remaining incomplete data in the buffer
                dataBuffer.Clear();
                if (lines.Length > 0 && !string.IsNullOrEmpty(lines[lines.Length - 1]))
                {
                    {
                        dataBuffer.Append(lines[lines.Length - 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DataReceived handler: {ex.Message}");
            }
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                if (AutomaticMode.Checked)
                {
                    serialPort.Write("a");
                    setAutoMode();
                }
            }
            else
            {
                MessageBox.Show("Serial port is not connected. Please connect to a port first.");
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Manual_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                if (Manual.Checked)
                {
                    serialPort.Write("b");
                    setManualMode();
                }
            }
            else
            {
                MessageBox.Show("Serial port is not connected. Please connect to a port first.");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Write("r"); // Send 'r' to request LDR reading
                }
                else
                {
                    MessageBox.Show("Serial port is not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Write("c");
            }
            else
            {
                MessageBox.Show("Serial port is not connected.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort.Write("d");
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (serialPort != null && serialPort.IsOpen)
        //        {
        //            string time = numericUpDown1.Value.ToString(); // Get the value from NumericUpDown1
        //            serialPort.Write("t"); // Send the command to set time
        //            serialPort.Write(time); // Send the time duration to the AVR
        //        }
        //        else
        //        {
        //            MessageBox.Show("Serial port is not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error sending data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the serial port is initialized and open
                if (serialPort != null && serialPort.IsOpen)
                {
                    // Get the value from NumericUpDown
                    string time = numericUpDown1.Value.ToString();

                    // Prepare the command with a newline as a delimiter
                    string command = $"t{time}\n";

                    // Send the command to the AVR
                    serialPort.Write(command);

                    // Display a confirmation message for debugging
                    MessageBox.Show($"Command sent: {command}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Display an error if the serial port is not open
                    MessageBox.Show("Serial port is not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle and display any errors during the data transmission
                MessageBox.Show($"Error sending data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConnectToSelectedPort();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisconnectFromPort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
