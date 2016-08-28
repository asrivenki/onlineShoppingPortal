using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assign1_1209374085_WebApp
{
    public partial class WebFormConsumeWebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServiceReference2.WebService1SoapClient s1 = new ServiceReference2.WebService1SoapClient();
            try
            {
                int t = Int32.Parse(TextBox1.Text.ToString());
                Label1.Text = s1.c2f(t) + "";
            }
            catch (Exception e1)
            {
                Label1.Text = "Invalid";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ServiceReference2.WebService1SoapClient s1 = new ServiceReference2.WebService1SoapClient();
            try
            {
                int t = Int32.Parse(TextBox1.Text.ToString());
                Label2.Text = s1.f2c(t) + "";
            }
            catch (Exception e1)
            {
                Label2.Text = "Invalid";
            }
        }
    }
}