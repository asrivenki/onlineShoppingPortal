using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assign3Part2WebApp
{
    public partial class user_info : System.Web.UI.Page
    {
        String[] result = new String[10];
        List<string> stringlist = new List<string>();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            string user = (string)Session["user"];
            Label1.Text = user;
            string final_result = "";
            
            //ITEMS AVAILABLE
            
            service.WebserviceSoapClient item = new service.WebserviceSoapClient();
           // itemDisplay.ServiceClient i = new itemDisplay.ServiceClient();
           // result = i.itemdisplay();
            stringlist = item.itemdisplay();
            TextBox5.TextMode = TextBoxMode.MultiLine;

            foreach (string str in stringlist)
            {
                final_result += str;
                final_result += "\n";
            }
            TextBox5.Text = final_result;
            TextBox5.Text.Replace(Environment.NewLine, "<br />");

            //CART
            TextBox4.TextMode = TextBoxMode.MultiLine;
            final_result = "";
            stringlist = item.retrieveCart(user);

            if (stringlist != null)
            {
                foreach (string str in stringlist)
                {
                    final_result += str;
                    final_result += "\n";
                }
                TextBox4.Text = final_result;
                TextBox4.Text.Replace(Environment.NewLine, "<br />");
            }
            else
            {
                TextBox4.Text = "No items in Cart";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            service.WebserviceSoapClient item = new service.WebserviceSoapClient();
            string user = (string)Session["user"];

            String _item = TextBox2.Text.ToString();
            String _quantity = TextBox3.Text.ToString();
            String final_result;
            if (item.addtoCart(user, _item, _quantity) == true)
            {
                TextBox4.TextMode = TextBoxMode.MultiLine;
                final_result = "";
                stringlist = item.retrieveCart(user);

                if (stringlist != null)
                {
                    foreach (string str in stringlist)
                    {
                        final_result += str;
                        final_result += "\n";
                    }
                    TextBox4.Text = final_result;
                    TextBox4.Text.Replace(Environment.NewLine, "<br />");
                }
                else
                {
                    TextBox4.Text = "No items in Cart";
                }
            }
            else // Adding to cart not successful
            {
                Response.Write("<script> alert('Sorry item Not added to cart')</script>");
            }
        }
    }
}