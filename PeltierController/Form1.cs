using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.Intrinsics.X86;
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

        //NTC reading and calculation variables
        int VntcRaw;
        double Vntc;
        double Rntc;
        int Rfixed = 9930;
        double Tkelvin;
        double Tcelsius;
        double A = 2.60 * Math.Pow(10, -3);
        double B = -5.76 * Math.Pow(10, -6);
        double C = 1.02 * Math.Pow(10, -6);
        double Vrfixed;
        double Vin = 5.063;
        double Itot;
        double Intc;
        double Iimp;
        int Rimp = 53000;
        double lnR;

        //PID variables
        double Kp = 10;
        double Ki = 0.1;
        double Kd = 0.2;

        double PID_Gain = 1;

        double Pterm;
        double Iterm;
        double Dterm;

        double Setpoint;
        double error;
        double previousError;

        double PID_PWMcorrection;

        int PWM_PID_Out;

        bool PID_enabled = false;

        private Stopwatch PID_timer;

        double dT;




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

            label2.Text = "Power Off";
            label6.Text = "Not enabled";
            label4.Text = "0";

            button1.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

            PID_timer = new Stopwatch();

            comboBox2.Items.Add("Dark");
            comboBox2.Items.Add("Light");
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

                    button9.Enabled = true;
                    button8.Enabled = true;

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
            if (coolingStatus == 0)
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
                if (coolingStatus == 2)
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

                if (coolingStatus == 1)
                {
                    //Check if input is within physical safety limit
                    if (pwmPromille > 400)
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

            button4.Enabled = true;
        }

        //Heating button click event
        private void button7_Click(object sender, EventArgs e)
        {
            coolingStatus = 1; //Heating
            label6.Text = "Heating";
            button4.Enabled = true;
        }

        //Timer callback thread for reading temperature and PID control. Runs every second in background
        private void Callback(object state)
        {
            Debug.WriteLine($"Function executed at {DateTime.Now}");

            //Only request temperature from controller if serial port is open
            if (Serial.IsOpen)
            {
                Serial.Write("?AI 1_");
                Thread.Sleep(100);
            }


            //Temperature calculations
            Vntc = Convert.ToDouble(VntcRaw) / 1000;
            Vrfixed = Vin - Vntc;
            Itot = Vrfixed / Rfixed;
            Iimp = Vntc / Rimp;
            Intc = Itot - Iimp;
            Rntc = Vntc / Intc;

            lnR = Math.Log(Rntc);

            Tkelvin = 1.0 / (A + B * lnR + C * Math.Pow(lnR, 3));
            Tcelsius = Tkelvin - 273.15;

            //Invoke action label edit because this is a different thread than the forms thread
            if (label11.InvokeRequired)
            {
                label11.Invoke(new Action(() =>
                {
                    label11.Text = Tcelsius.ToString("F1");
                }
                ));
            }
            else
            {
                label11.Text = Tcelsius.ToString("F1");
            }

            //PID control
            if (PID_enabled == true)
            {
                error = Setpoint - Tcelsius;
                dT = Convert.ToDouble(PID_timer.ElapsedMilliseconds) / 1000;
                PID_timer.Restart();

                Pterm = error;

                Iterm += error * dT;

                Dterm = (error - previousError) / dT;

                previousError = error;

                PID_PWMcorrection = -PID_Gain * (Kp * Pterm + Ki * Iterm + Kd * Dterm);

                if (PID_PWMcorrection > 100)
                {
                    PID_PWMcorrection = 100;
                }
                if (PID_PWMcorrection < -50)
                {
                    PID_PWMcorrection = -50;
                }

                try
                {
                    PWM_PID_Out = Convert.ToInt32(PID_PWMcorrection) * 10;
                }
                catch
                {
                }

                //Update labels (needs invoke because seperate thread
                if (label4.InvokeRequired)
                {
                    label4.Invoke(new Action(() =>
                    {
                        label4.Text = PWM_PID_Out.ToString();
                    }
                    ));
                }
                else
                {
                    label4.Text = PWM_PID_Out.ToString();
                }

                if (PWM_PID_Out > 0)
                {
                    if (label6.InvokeRequired)
                    {
                        label6.Invoke(new Action(() =>
                        {
                            label6.Text = "Cooling";
                        }
                        ));
                    }
                    else
                    {
                        label6.Text = "Cooling";
                    }
                }

                if (PWM_PID_Out < 0)
                {
                    if (label6.InvokeRequired)
                    {
                        label6.Invoke(new Action(() =>
                        {
                            label6.Text = "Heating";
                        }
                        ));
                    }
                    else
                    {
                        label6.Text = "Heating";
                    }
                }

                if (Serial.IsOpen)
                {
                    //Format ASCII string to selected com port with converted PWM promille value
                    string command = $"!G 1 {PWM_PID_Out}_";
                    //Disable watchdog
                    Serial.Write("^RWD 0_");

                    //Send formatted command
                    Serial.Write(command);


                }


                Debug.WriteLine("");
                Debug.WriteLine("Tcelsius: " + Tcelsius);
                Debug.WriteLine("Setpoint: " + Setpoint);
                Debug.WriteLine("PID Enabled: " + PID_enabled);
                Debug.WriteLine("Error: " + error);
                Debug.WriteLine("dT: " + dT);
                Debug.WriteLine("PID_PWMcorrection [Double]:" + PID_PWMcorrection);
                Debug.WriteLine("PID_PWM_out: [INT] " + PWM_PID_Out);
            }


        }

        //Serial datareceived eventhandler
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //Read incoming data and parse it
            SerialPort sp = (SerialPort)sender;
            dataReceived = sp.ReadExisting(); // Read incoming data


            //Check is the received data is a valid temperature datapoint,should not contain anything else then A=
            if (dataReceived.Contains("!G 1") || dataReceived.Contains("+"))
            {
                return;
            }

            //Strip first three identifying characters
            dataStripped = dataReceived.Substring(3);

            //Convert stripped data string to int
            if (int.TryParse(dataStripped, out VntcRaw))
            {
            }
            else
            {
            }

            /*
            Debug.WriteLine("");
            Debug.WriteLine("Received: " + dataReceived);
            Debug.WriteLine("Stripped string: " + dataStripped);
            Debug.WriteLine("VntcRaw: " + VntcRaw);
            Debug.WriteLine("Vntc: " + Vntc);
            Debug.WriteLine("Vfixed: " + Vrfixed);
            Debug.WriteLine("Intc: " + Intc);
            Debug.WriteLine("Rntc: " + Rntc);

            Debug.WriteLine("");
            Debug.WriteLine("Tkelvin: " + Tkelvin);
            Debug.WriteLine("Tcelsius: " + Tcelsius);
            */

        }

        //Temperature set event
        private void button5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double SetpointInput))
            {
            }
            else
            {
            }

            if (SetpointInput > 50 || SetpointInput < -10)
            {
                MessageBox.Show("Temperature outside of limits, please enter a temperature between -10 and 50c");
                return;
            }

            label2.Text = "Power On (AUTO)";
            PID_enabled = true;
            Setpoint = SetpointInput;
            PID_timer.Start();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            button5.Enabled = false;

            PID_enabled = false;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button4.Enabled = false;

            button5.Enabled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Dark")
            {
                groupBox1.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                groupBox2.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                groupBox3.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                groupBox4.ForeColor = ColorTranslator.FromHtml("#FBF8EF");

                button1.ForeColor = SystemColors.ControlText;
                button2.ForeColor = SystemColors.ControlText;
                button3.ForeColor = SystemColors.ControlText;
                button4.ForeColor = SystemColors.ControlText;
                button5.ForeColor = SystemColors.ControlText;
                button6.ForeColor = SystemColors.ControlText;
                button7.ForeColor = SystemColors.ControlText;
                button8.ForeColor = SystemColors.ControlText;
                button9.ForeColor = SystemColors.ControlText;

                label1.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label2.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label3.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label4.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label5.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label6.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label7.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label8.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label9.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label10.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label11.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label12.ForeColor = ColorTranslator.FromHtml("#FBF8EF");
                label13.ForeColor = ColorTranslator.FromHtml("#FBF8EF");

                button1.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button2.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button3.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button4.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button5.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button6.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button7.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button8.BackColor = ColorTranslator.FromHtml("#C9E6F0");
                button9.BackColor = ColorTranslator.FromHtml("#C9E6F0");

                this.BackColor = ColorTranslator.FromHtml("#23486A");
            }

            if (comboBox2.Text == "Light")
            {
                groupBox1.ForeColor = SystemColors.ControlText;
                groupBox2.ForeColor = SystemColors.ControlText;
                groupBox3.ForeColor = SystemColors.ControlText;
                groupBox4.ForeColor = SystemColors.ControlText;

                label1.ForeColor = SystemColors.ControlText;
                label2.ForeColor = SystemColors.ControlText;
                label3.ForeColor = SystemColors.ControlText;
                label4.ForeColor = SystemColors.ControlText;
                label5.ForeColor = SystemColors.ControlText;
                label6.ForeColor = SystemColors.ControlText;
                label7.ForeColor = SystemColors.ControlText;
                label8.ForeColor = SystemColors.ControlText;
                label9.ForeColor = SystemColors.ControlText;
                label10.ForeColor = SystemColors.ControlText;
                label11.ForeColor = SystemColors.ControlText;
                label12.ForeColor = SystemColors.ControlText;
                label13.ForeColor = SystemColors.ControlText;

                button1.BackColor = SystemColors.Control;
                button2.BackColor = SystemColors.Control;
                button3.BackColor = SystemColors.Control;
                button4.BackColor = SystemColors.Control;
                button5.BackColor = SystemColors.Control;
                button6.BackColor = SystemColors.Control;
                button7.BackColor = SystemColors.Control;
                button8.BackColor = SystemColors.Control;
                button9.BackColor = SystemColors.Control;
                this.BackColor = SystemColors.Control;
            }
        }
    }

}
