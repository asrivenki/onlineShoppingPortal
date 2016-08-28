using System;
using System.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Web;

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

    public String Transform_XML_HTML(String xmlPath, string xslPath)
    {
        string htmlPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Persons.html");
        TextWriter txtWriter = null;
        TextReader txtReader = null;

        txtWriter = new StreamWriter(htmlPath);
        
        

        try
        {
            String html = "";
            
            XPathDocument xDoc = new XPathDocument(xmlPath);
            XslCompiledTransform xt = new XslCompiledTransform();
            xt.Load(xslPath);
            //  String str = "";
            xt.Transform(xDoc, null, txtWriter);

            if (txtWriter != null)
                txtWriter.Close();

            txtReader = new StreamReader(htmlPath);
            while ((txtReader.ReadLine()) != null)
            {
                html = html + txtReader.ReadLine();
            }

            if(txtReader != null)
                txtReader.Close();
            
            if (html != string.Empty)
            {
                return html;
            }
            else
                return "Error in generating the HTML.";
            
        }
        catch (Exception e)
        {
            if(txtReader != null)
                txtReader.Close();
            if(txtWriter != null)
                txtWriter.Close();
            return "Error." + e.Message;
        }

       


    }

    

    

    public string valid = "yes";
    public String validateSchema(String XMLpath, String XSDpath)
    {
        XmlReader reader = null;
        try
        {
            //String XMLpath = Path.Combine(HttpRuntime.AppDomainAppPath, "Persons_Wrong.xml");
            //String XSDpath = Path.Combine(HttpRuntime.AppDomainAppPath, "Persons.xsd");
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, XSDpath);
            // Define the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            // Create the XmlReader object.
            reader = XmlReader.Create(XMLpath, settings);
            // Parse the file. 
            while (reader.Read()) ;	// will call event handler if invalid
            reader.Close();
            Console.WriteLine("The XML file validation has completed");
        }
        catch (Exception e1)
        {
            valid = "Error "+ e1.Message;
            
        }

        if(reader != null)
            reader.Close();
        return valid;
    }

    public String validateSchema_Correct()
    {
        String XMLpath = Path.Combine(HttpRuntime.AppDomainAppPath, "Persons_Wrong.xml");
        String XSDpath = Path.Combine(HttpRuntime.AppDomainAppPath, "Persons.xsd");
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        schemaSet.Add(null, XSDpath);
        // Define the validation settings.
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.Schemas = schemaSet;
        settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
        // Create the XmlReader object.
        XmlReader reader = XmlReader.Create(XMLpath, settings);
        // Parse the file. 
        while (reader.Read()) ;	// will call event handler if invalid
        Console.WriteLine("The XML file validation has completed");
        return valid;
    }

    // Display any validation errors.
    private void ValidationCallBack(object sender, ValidationEventArgs e)
    {
        valid = e.Message;
        Console.WriteLine("Validation Error: {0}", e.Message);
       
    }
}
