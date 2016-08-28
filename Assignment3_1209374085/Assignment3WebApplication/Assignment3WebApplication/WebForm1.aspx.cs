using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Threading.Tasks;


namespace Assignment3WebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String[] result = new string[10];
            String final_result ="";
            ServiceReference.ServiceClient web = new ServiceReference.ServiceClient();
            

            if (TextBox1.Text.ToString() != null && TextBox1.Text.ToString() != "" && TextBox1.Text.ToString() != " " )
            {
                result = web.top10WordsRegex(TextBox1.Text.ToString());
                TextBox2.TextMode = TextBoxMode.MultiLine;
                
                foreach (string str in result)
                {
                    final_result += str;
                    final_result += "\n";
                }
                TextBox2.Text = final_result;
                TextBox2.Text.Replace(Environment.NewLine, "<br />");
                
            }
            else
            {
                Response.Write("<script>alert('Please enter a value');</script>");
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text.ToString() != null && TextBox3.Text.ToString() != "" && TextBox3.Text.ToString() != " ")
            {
                
                ServiceReference.ServiceClient web = new ServiceReference.ServiceClient();
                string result = web.removeStopWords(TextBox3.Text.ToString());
                TextBox4.Text = result;
            }

            else
            {
                Response.Write("<script>alert('Please enter a value');</script>");
            }
        }
    }
}