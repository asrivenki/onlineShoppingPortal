using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}

    public String[] itemdisplay()
    {
        String[] result = new String[10];
        int i = 0;
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            
            using (StreamReader sr = new StreamReader(Path.Combine(HttpRuntime.AppDomainAppPath,"items.txt")) )
            {
                String str = "";
                while ((str = sr.ReadLine()) != null)
                {
                    result[i] = str;
                    i++;
                }

            }
            return result;
        }
        catch (Exception e1)
        {
            return result;
        }
    }
    public Boolean validate_login(String uname, string password)
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(Path.Combine(HttpRuntime.AppDomainAppPath,"Login.txt")) )
            {
                String str = "";
                while ((str = sr.ReadLine()) != null)
                {
                    Char[] t1 = { ',' };
                    String[] t = str.Split(t1);
                    if (t[0].Equals(uname) && t[1].Equals(password))
                    {
                        return true;
                    }
                }
                return false;
            }

        }
        catch (Exception e1)
        {
            return false;
        }
    }

}
