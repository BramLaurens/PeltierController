using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;



//Title: Peltier controller GUI
//Author: Bram Laurens
//February 2025

//This program controls a peltier module, connected to a Roboteq Motor Controller.
//The goal of the program is to modulate a PWM signal's duty length through sending serial commands to the controller
//The serial commands are sent through the COM port / USB in ASCII format


namespace PeltierController
{
    public partial class Form1 : Form
    {
        //Class variables
        static int coolingStatus = 0; //0 = off, 1 = heating, 2 = cooling
        static bool peltierStatus; //False = turned off, true = power on
        string dataReceived;
        string dataStripped;
        int Vntc;

        //Make a new serialport object
        private SerialPort Serial = new SerialPort();

        //Make a new timer
        private static System.Threading.Timer _timer;

        public Form1()
        {
            //Inititalize labels
            InitializeComponent();

            //Call the form load eventhandler
            this.Load += new EventHandler(Form1_Load);

        }

        //Function to update com port's list when called
        private void UpdateCOMportList()
        {
            //Refresh the COM ports, put the ports in an array and clear the comboBox. 
            String[] Ports = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.Clear();

            //Add all ports from the array to the comboBox
            foreach (var item in Ports)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Call the update COM ports function upon loading the window
            UpdateCOMportList();

            //New timer object
            _timer = new System.Threading.Timer(Callback, null, 0, 1000);

            label2.Text = "Peltier Disabled";
            label6.Text = "Not enabled";
            label4.Text = "0";
        }

        //Reset peltier button click event
        private void button1_Click(object sender, EventArgs e)
        {

            //Check if serial port is open
            if (Serial.IsOpen)
            {


                //Format ASCII string to selected com port with converted PWM promille value
                string command = $"!G 1 0_";
                //Disable watchdog
                Serial.Write("^RWD 0_");
                //Send formatted command
                Serial.Write(command);
                MessageBox.Show("Sent PWM command: " + command);

                label4.Text = "0";
                label6.Text = "No PWM";
                label2.Text = "Powered off";

                coolingStatus = 0;
                peltierStatus = false;
            }
        }

        //Refresh COM ports button click event
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateCOMportList();
        }

        //Connect COM button click event
        private void button3_Click(object sender, EventArgs e)
        {

            //If the serial port is not open yet
            if (false == Serial.IsOpen)
            {
                //Set the Serial port object according to selected comboBox item
                try
                {
                    Serial.PortName = comboBox1.Text;
                }
                catch
                {
                    MessageBox.Show("No COM port selected!");
                    return;
                }

                //Set fixed Serial port object arguments 
                Serial.BaudRate = 9600;
                Serial.DataBits = 8;
                Serial.ReceivedBytesThreshold = 1;
                Serial.StopBits = StopBits.One;
                Serial.Handshake = Handshake.None;
                Serial.WriteTimeout = 3000;
                Serial.DataReceived += SerialPort_DataReceived;

                //Open the serial port
                try
                {
                    Serial.Open();

                    //Change button text to disconnect, and disable the combobox & COM ports button
                    button3.Text = "Disconnect";
                    comboBox1.Enabled = false;
                    button2.Enabled = false;

                }
                catch
                {
                    MessageBox.Show("Error: The COM port is not accesible. Close all applications using this port.");
                    return;
                }
            }
            //If the serial port is already open, close it instead
            else
            {
                //Close the serial port
                try
                {
                    Serial.Close();

                    //Change button text to connect, and enable the combobox & COM ports button
                    button3.Text = "Connect";
                    comboBox1.Enabled = true;
                    button2.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Error: Unable to close COM port. Close all applications using this port");
                    return;
                }
            }

        }

        //PWM send command click event
        private void button4_Click(object sender, EventArgs e)
        {
            //If no cooling mode selected, return function and give error
            if(coolingStatus == 0)
            {
                MessageBox.Show("Please select cooling or heating mode first");
                return;
            }

            //Parse text from textbox to pwmpercentageraw int
            if (int.TryParse(textBox1.Text, out int pwmPercentageRaw))
            {
            }
            else
            {
                MessageBox.Show("Invalid input! Please enter a valid value");
            }

            //Check if input is within bounds
            if (pwmPercentageRaw > 100 || pwmPercentageRaw < 0)
            {
                MessageBox.Show("Input out of range, enter a value between 0 and 100");
                return;
            }

            //convert pwmpercentage to promille for serial format
            int pwmPromille = pwmPercentageRaw * 10;

            //Check if serial port is open
            if (Serial.IsOpen)
            {

                //Cool if coolingstatus is 2
                if(coolingStatus == 2)
                {
                    //Format ASCII string to selected com port with converted PWM promille value
                    string command = $"!G 1 {pwmPromille}_";
                    //Disable watchdog
                    Serial.Write("^RWD 0_");
                    //Send formatted command
                    Serial.Write(command);
                    MessageBox.Show("Sent PWM command: " + command);

                    label4.Text = pwmPromille.ToString();
                    label6.Text = "Cooling";
                }

                if(coolingStatus == 1)
                {
                    //Check if input is within physical safety limit
                    if(pwmPromille > 400)
                    {
                        MessageBox.Show("Selected PWM value too high, module will overheat. Please select a value below 400");
                        return;
                    }

                    //Format ASCII string to selected com port with converted PWM promille value, add a minus for heating mode
                    string command = $"!G 1 -{pwmPromille}_";
                    //Disable watchdog
                    Serial.Write("^RWD 0_");
                    //Send formatted command
                    Serial.Write(command);
                    MessageBox.Show("Sent PWM command: " + command);

                    label4.Text = pwmPromille.ToString();
                    label6.Text = "Heating";
                }

                label2.Text = "Powered on";
                peltierStatus = true;
            }

            //Throw exception if Serial port is closed
            else
            {
                MessageBox.Show("Unable to send command: Serial port is not open.");
            }


        }

        //Cooling button click event
        private void button6_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Function executed at {DateTime.Now}");
            coolingStatus = 2; //Cooling
            label6.Text = "Cooling";
        }

        //Heating button click event
        private void button7_Click(object sender, EventArgs e)
        {
            coolingStatus = 1; //Heating
            label6.Text = "Heating";
        }

        //Timer callback threat for reading temperature. Runs every second in background
        private void Callback(object state)
        {
            Debug.WriteLine($"Function executed at {DateTime.Now}");

            if (Serial.IsOpen) {
                Serial.Write("?AI 1_");
            }
            
        }

        //Serial datareceived event
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //Read incoming data and parse it (strip first ID characters)
            SerialPort sp = (SerialPort)sender;
            dataReceived = sp.ReadExisting(); // Read incoming data
            dataStripped = dataReceived.Substring(3);

            //Convert stripped data string to int
            if (int.TryParse(dataStripped, out Vntc))
            {
            }
            else
            {
            }

            Debug.WriteLine("Received: " + dataReceived);
            Debug.WriteLine("Stripped string: " + dataStripped);
            Debug.Write("Vntc (int): " + Vntc);
        }
    }

}
