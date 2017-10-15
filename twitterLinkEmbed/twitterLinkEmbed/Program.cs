using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace twitterLinkEmbed
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // initiate variables: line to capture each line of file and key as a search term to find the twitter link
            string line;
            string key = "https://twitter.com/";

            // delimter used to seperate each line into strings without spaces b/c each line is sandwhiched in between spaces
            char[] delimeters = {' '};            
            
            // set path 
            string fileName = "coding-challenge.html";
            string path = Path.Combine(Environment.CurrentDirectory, "", fileName);

            // using stringbuilder bc of append function to easily build final text object
            StringBuilder newFile = new StringBuilder();

            // open connection to file and loop through each line looking for twitter link
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                // if link is in the line, split the line into an array of words in the line, no spaces
                if (line.Contains(key))
                {
                    string[] words = line.Split(delimeters);
                   
                    // find which element of the words array is the link and send the link to function to get it replaced by the html GET response
                    foreach (string s in words)
                    {
                        int counter = 0;
                        if (s.Contains(key))
                        {
                            // make apicall: send url get html string
                             words[counter] = apiCall(s);
                        }
                        counter++;
                        

                    }

                    // put the spaces back in the line
                    line = string.Join(" ", words);


                }

                // add the new line to the stringbuilder object
                newFile.Append(line);
                
               
            }
            
            file.Close();

            System.Console.WriteLine(newFile);




        }

        // This method takes the twitter url and makes an api call using webclient and downloads the API resonse and then parses out the HTML value from the JSON
        // that html is then returned back to the filereader loop
        public static string apiCall(string url)
        {
            string html = "";

            using (WebClient wc = new WebClient())
            {
                try
                {
                    string responseJSON = wc.DownloadString("https://publish.twitter.com/oembed?url=" + url);
                    // jsonObject class file made with json2Csharp to easily make a c# class that can represent the JSON data
                    var JSON = JsonConvert.DeserializeObject<jsonObject>(responseJSON);
                    html = JSON.html;
                }
                catch (Exception e)
                {
                    // if the link doesn't work or returns an error, keep the original link the same and send it back to the file reader
                    html = url;
                    
                }
              
            }

            return html;

        }


    }
}
