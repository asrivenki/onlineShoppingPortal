using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign1_1209374085_WebBrowser
{
    public partial class Form1 : Form
    {
        String result_string = "abc";
        public Form1()
        {
            InitializeComponent();
        }


        public void setString()
        {
            Font f = new Font("Forte", 20, FontStyle.Bold);
            var image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Brown);

            g.DrawString(generateRandomString(), f, Brushes.Chocolate, new Point(50, 25));
            pictureBox1.Image = image;
        }

        public String generateRandomString()
        {
            Random r1 = new Random();
            int i = 0;
            String result = "";
            while (i < 4)
            {
                Char c = (char)r1.Next(97, 122);
                result += c;
                i++;
            }
            result_string = result;
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String s = textBox2.Text.ToString();
            if (result_string.Equals(s) == true)
            {
                MessageBox.Show("Text Matches");
            }
            else
            {
                MessageBox.Show("Text does not Match");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.ToString() == null || textBox3.Text.ToString() == " " || textBox3.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a proper Symbol");
            }
            else
            {
                StockQuote.ServiceClient s1 = new StockQuote.ServiceClient();
                label4.Text = s1.getStockquote(textBox3.Text.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setString();
        }
    }
}
