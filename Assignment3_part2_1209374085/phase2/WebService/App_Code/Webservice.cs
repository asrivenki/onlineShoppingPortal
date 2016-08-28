using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

/// <summary>
/// Summary description for Webservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Webservice : System.Web.Services.WebService {

    public Webservice () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
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

    [WebMethod]
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

    [WebMethod]
    public String encrypt(String str)
    {
        string encrypt = "";

        encryptDecrypt.ServiceClient ob1 = new encryptDecrypt.ServiceClient();
        encrypt = ob1.Encrypt(str);

        return encrypt;
    }

    [WebMethod]
    public string decrypt(String decrypt)
    {
        encryptDecrypt.ServiceClient ob1 = new encryptDecrypt.ServiceClient();
        string username = ob1.Decrypt(decrypt);
        return username;
    }


        [WebMethod]
        public Boolean addtoCart(String userId, String item, string quantity)
        {
            try
            {
                if (File.Exists( Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt")) )
                {
                    string[] lines = File.ReadAllLines(Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt") ); 
                    int lineNumber = 1;
                    String replace_str = "";
                    Boolean replace_flag = false;
                    using (StreamReader sr = new StreamReader( Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt")) )
                    {
                        String str = "";
                        while ((str = sr.ReadLine()) != null)
                        {
                            char[] t1 = { ',' };
                            string[] t = str.Split(t1);
                            if (t[0].Equals(item))
                            {
                                replace_str = item + "," + (Int32.Parse(t[1]) + Int32.Parse(quantity) );
                                replace_flag = true;
                                break;
                            }
                            lineNumber++;
                        }

                    }

                    if (replace_flag == true)
                    {
                        using (StreamWriter writer = new StreamWriter(Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt")) )
                        {
                            for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
                            {
                                if (currentLine == lineNumber)
                                {
                                    writer.WriteLine(replace_str);
                                }
                                else
                                {
                                    writer.WriteLine(lines[currentLine - 1]);
                                }
                            }
                        }
                    } // End of Flag true

                    else
                    {
                        using (StreamWriter sw = File.AppendText(Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt")) )
                        {
                            sw.WriteLine(item + "," + quantity);
                        }

                    }

                } // End of File_Exists

                else   // File does not exists
                {
                    using (StreamWriter sw = File.AppendText(Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt")) )
                    {
                        sw.WriteLine(item + "," + quantity);
                    }
                }

                return true;
            }

            catch (Exception e1)
            {
                Console.WriteLine(e1.ToString());
                return false;
            }

        }

     [WebMethod]
     public String[] retrieveCart(String userId)
     {
         String[] result = null;
         if (File.Exists(Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt")) )
         {
             result = File.ReadAllLines(Path.Combine(HttpRuntime.AppDomainAppPath,"shopping_cart/" + userId + "_cart.txt") ); 
         }
         return result;
     }

    [WebMethod]
    public void registerUser(String str)
    {
        using (StreamWriter writer = File.AppendText(Path.Combine(HttpRuntime.AppDomainAppPath,"Login.txt")) )
        {
            //string str = "";
            writer.WriteLine(str);
        }
    }


        
    }

    

