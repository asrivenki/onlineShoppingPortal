using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assign3Part2WebApp
{
    public partial class Product : System.Web.UI.Page
    {
        List<String> itemstringlist;
        List<String> cartlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            string user;
            user = (string)Session["user"];
            user = "user1";

            Label1.Text = user;
            
            //ITEMS AVAILABLE

            service.WebserviceSoapClient item = new service.WebserviceSoapClient();
            // itemDisplay.ServiceClient i = new itemDisplay.ServiceClient();
            // result = i.itemdisplay();
            itemstringlist = item.itemdisplay();

            

            foreach (string str in itemstringlist)
            {
                
                 ListBox2.Items.Add(str);

            }

            //CART
            //Clearing Content
            ListBox1.Items.Clear();


            cartlist = item.retrieveCart(user);

            if (cartlist != null)
            {
                foreach (string str in cartlist)
                {
                    ListBox1.Items.Add(str);
                }
               
            }
            else
            {
                ListBox2.Items.Add("No Items in the cart");
            }

            //Default values in dropdown list
           
        }

        
        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            //Response.Write("<script> alert('" + ListBox2.SelectedItem.ToString() + "')</script>");
            
            String temp = ListBox2.SelectedItem.ToString();

            char[] t1 = { ',' };
            string[] t = temp.Split(t1);
            TextBox2.Text = t[0];

            int limit = Int32.Parse(t[1]);
            DropDownList2.Items.Clear();
            for (int i = 1; i <= limit; i++)
            {
                DropDownList2.Items.Add(i + "");
            }
            */
            
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            String temp = ListBox1.SelectedItem.ToString();

            char[] t1 = { ',' };
            string[] t = temp.Split(t1);
            TextBox1.Text = t[0];

            int limit = Int32.Parse(t[1]);
            DropDownList1.Items.Clear();
            for (int i = 1; i <= limit; i++)
            {
                DropDownList1.Items.Add(i + "");
            }*/
        }
        
        protected void Button2_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            Boolean var = false;
            service.WebserviceSoapClient client = new service.WebserviceSoapClient();
            string item = TextBox2.Text.ToString();
            string quantity = TextBox4.Text.ToString();
            String user = (string)Session["user"];
            user = "user1";

            if (item == "" || quantity == "")
            {
                Response.Write("<script> alert('Please enter a value')</script>");
            }
            else
            {
                foreach (string str in itemstringlist)
                {
                    char[] t1 = { ',' };
                    string[] t = str.Split(t1);
                    if (t[0].Equals(item) && Int32.Parse(t[1]) >= Int32.Parse(quantity) )
                    {
                        if (client.addtoCart(user, item, quantity) == true)
                        {
                            cartlist = client.retrieveCart(user);
                            if (cartlist != null)
                            {
                                foreach (string str1 in cartlist)
                                {
                                    ListBox1.Items.Add(str1);
                                }
                            }


                        }
                        else
                        {
                            Response.Write("<script> alert('Some problem in adding to cart')</script>");
                        }

                        var = true;
                        break;
                    }
                }

                if (var == false)
                {
                    Response.Write("<script> alert('Please enter proper value')</script>");
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}