using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter the notification title: ");
        string title = Console.ReadLine();

        Console.Write("Enter the notification body: ");
        string body = Console.ReadLine();

        string requestBody = @"{
            ""to"": ""firebase device token"",
            ""notification"": {
                ""title"": """ + title + @""",
                ""body"": """ + body + @"""
            }
        }";

        string url = "https://fcm.googleapis.com/fcm/send";
        string authorizationHeader = "headerKey";

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "key=" + authorizationHeader);
            request.ContentType = "application/json";

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseContent = reader.ReadToEnd();

            Console.WriteLine("Response:");
            Console.WriteLine(responseContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}
