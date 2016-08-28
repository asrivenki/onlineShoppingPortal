using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.ServiceClient ser = new ServiceReference1.ServiceClient();
            string xmlPath = TextBox1.Text.ToString();
            string xslPath = TextBox2.Text.ToString();
            TextBox3.Text = ser.Transform_XML_HTML(xmlPath, xslPath);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ServiceReference1.ServiceClient ser = new ServiceReference1.ServiceClient();
            string xmlPath = TextBox4.Text.ToString();
            string xsdPath = TextBox5.Text.ToString();
            
            string res = ser.validateSchema(xmlPath, xsdPath);
            if (res.Equals("yes"))
            {
                TextBox6.Text = "No error";
            }
            else
                TextBox6.Text = res;

        }

    }
}