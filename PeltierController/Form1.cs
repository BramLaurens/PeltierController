using System.IO;
using System.IO.Ports;


//Peltier controller GUI
//Author: Bram Laurens
//February 2025
//Dit programma heeft als doel het aansturen van een peltier module.
//De module is aangesloten op een Roboteq Motor Controller.
//De controller genereert PWM signalen met een duty cycle in promille op basis van
//seriele commando's in ASCII formaat


namespace PeltierController
{
    public partial class Form1 : Form
    {
        //Class variables
        static bool coolingStatus;
        static int peltierOutput;

        //Make a new serialport object
        private SerialPort Serial = new SerialPort();

        public Form1()
        {
            InitializeComponent();
            label2.Text = "Peltier Disabled";

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
        }

        //Reset peltier button click event
        private void button1_Click(object sender, EventArgs e)
        {
            coolingStatus = true;
            peltierOutput = 0;
            label2.Text = "Peltier Disabled";
            label4.Text = peltierOutput.ToString();
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
                }

                //Set fixed Serial port object arguments 
                Serial.BaudRate = 9600;
                Serial.DataBits = 8;
                Serial.ReceivedBytesThreshold = 1;
                Serial.StopBits = StopBits.One;
                Serial.Handshake = Handshake.None;
                Serial.WriteTimeout = 3000;

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
            //If the serial port is already open
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
    }
}
