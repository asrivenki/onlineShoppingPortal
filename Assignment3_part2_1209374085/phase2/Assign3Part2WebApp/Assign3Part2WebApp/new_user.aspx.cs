using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Assign3Part2WebApp
{
    public partial class new_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = TextBox1.Text.ToString() + "," + TextBox2.Text.ToString() + "," + TextBox3.Text.ToString() + "," + TextBox4.Text.ToString() + "," + TextBox5.Text.ToString();
            service.WebserviceSoapClient ser = new service.WebserviceSoapClient();
            ser.registerUser(str);
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }
    }
}