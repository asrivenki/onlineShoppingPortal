using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Text.RegularExpressions;


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

    public String removeStopWords(String input)
    {
        int i = 0;

        String result = "";
        string regularExpression = @"(</?)[^>]+?(/?>)|[^a-zA-Z]";
        string parsedContentFromURL = Regex.Replace(input, regularExpression, " ").ToLower();
        // Console.WriteLine("\n Printing " + parsedContentFromURL + " dddd");
        input = parsedContentFromURL;
        String[] t = input.Split();
        String stopWords = "a about above after again against all am an and any are aren't as at be because been before being below between both but by can't cannot could couldn't did didn't do does doesn't doing don't down during each few for from further had hadn't has hasn't have haven't having he he'd he'll he's her here here's hers herself him himself is how how's i i'd i'll i'm i've if in into is isn't it it's its itself let's me more most mustn't my myself no nor not of off on once only or other ought our ours ourselves out over own same shan't she she'd she'll she's should shouldn't so some such than that that's the their  theirs them themselves then there there's these they they'd they'll they're they've this those through to too under until up very was wasn't we we'd we'll we're we've were weren't what what's when when's where where's which while who who's whom why why's with won't would wouldn't you you'd you'll you're you've your yours yourself";

        foreach (String s in t)
        {
            if (stopWords.Contains(s))
            {

            }
            else
            {
                result += s;
                result += " ";
            }


        }
        return result;
    }

    public string[] top10WordsRegex(string str)
    {
        String[] res = new String[10];
        int res_index = 0;
        int i = 0;
        WebContent.ServiceClient web = new WebContent.ServiceClient();
        String input = web.GetWebContent(str);
        Dictionary<String, Int32> dict = new Dictionary<string, int>();
        //string pattern = "<div.*?>(.*?)<\\/div>";
        string regularExpression = @"(</?)[^>]+?(/?>)|[^a-zA-Z]";
        string parsedContentFromURL = Regex.Replace(input, regularExpression, " ").ToLower();
        Console.WriteLine("\n Printing " + parsedContentFromURL + " dddd");
        String temp = parsedContentFromURL;
        /*
        string temp = "";
        //int i = 0;
        while (i < input.Length)
        {
            char c = input.ElementAt(i);
            Console.Write(c);
            if (c == '<')
            {

                while (i < input.Length && c != '>')
                {
                    i++;
                    c = input.ElementAt(i);
                }
                temp += ' ';
            }
            else
            {
                temp += c;
            }
            i++;
        }
        */
        char[] delimiterChars = { ' ', '\t' };
        string[] t = temp.Split(delimiterChars);
        Console.WriteLine("\n " + temp);
        /*
        string regularExpression = @"(</?)[^>]+?(/?>)|[^a-zA-Z]";
        string parsedContentFromURL = Regex.Replace(temp, regularExpression, " ").ToLower();
        Console.WriteLine(parsedContentFromURL);
        t = temp.Split(delimiterChars);
        */
        foreach (string s in t)
        {
            Console.WriteLine(s);
            if (dict.ContainsKey(s))
            {
                int j = dict[s];
                dict.Remove(s);
                dict.Add(s, j + 1);
            }
            else
            {
                dict.Add(s, 1);
            }
        }


        var myList = dict.ToList();

        myList.Sort((pair2, pair1) => pair1.Value.CompareTo(pair2.Value));
        int size = 0;

        foreach (var pair in myList)
        {
            if (size >= 10)
            {
                break;
            }
            if (pair.Key != "" && pair.Key != null)
            {
                res[res_index++] = pair.Key;
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
            size++;
        }

        return res;

    }


}
