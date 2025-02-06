namespace PeltierController
{
    public partial class Form1 : Form
    {
        static bool coolingStatus;
        public Form1()
        {
            InitializeComponent();
            label2.Text = "Peltier Disabled";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coolingStatus = true;
        }
    }
}
