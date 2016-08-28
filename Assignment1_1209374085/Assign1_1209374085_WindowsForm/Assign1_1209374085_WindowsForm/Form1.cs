using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign1_1209374085_WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReference2.WebService1SoapClient c1 = new ServiceReference2.WebService1SoapClient();
            //HelloWorld.WebService1SoapClient c1 = new HelloWorld.WebService1SoapClient();
            int m1, m2;
            try
            {
                m1 = Int32.Parse(textBox1.Text.ToString());
                
                label2.Text = c1.c2f(m1) + "";

            }
            catch (FormatException e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceReference2.WebService1SoapClient c1 = new ServiceReference2.WebService1SoapClient();
            //HelloWorld.WebService1SoapClient c1 = new HelloWorld.WebService1SoapClient();
            int m1, m2;
            try
            {
                
                m2 = Int32.Parse(textBox2.Text.ToString());
                label4.Text = c1.f2c(m2) + "";

            }
            catch (FormatException e1)
            {
                Console.WriteLine(e1.Message);
            }
        }


    }
}
