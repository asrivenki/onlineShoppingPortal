using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using System.IO;
namespace Assign3Part2WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string user = TextBox1.Text.ToString();
            string pass = TextBox2.Text.ToString();

            if (user == "" || user == null || pass == "" || pass == null)
            {
                Response.Write("<script>alert('Please enter a value');</script>");
            }
            else
            {
                WebClient proxy = new WebClient();
                String serviceURL = string.Format("http://webstrar5.fulton.asu.edu/page3/Service.svc/validateLogin?user={0}&password={1}", user, pass);
                byte[] data = proxy.DownloadData(serviceURL);
                Stream _mem = new MemoryStream(data);
                var reader = new StreamReader(_mem);
                string result = reader.ReadToEnd();

                if (result.Contains("true"))
                {
                    Session["user"] = user;
                    Response.Write("<script>alert('You have entered valid username and password');</script>");
                    //Response.Redirect(Path.Combine(HttpRuntime.AppDomainAppPath,"Product.aspx"));

                }
                else
                {
                    Response.Write("<script>alert('Please enter valid user and password');</script>");
                }


                /*
                service.WebserviceSoapClient ser = new service.WebserviceSoapClient();

                if (ser.validate_login(user, pass) == true)
                {
                    Session["user"] = user; 
                    Response.Redirect("user_info.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Please enter valid user and password');</script>");
                }*/
            }
        }
    }
}