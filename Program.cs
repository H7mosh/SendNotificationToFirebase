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
            ""to"": ""czrGRDYSSHe_ETdJ-2w9oX:APA91bHXcGk13w-2rzJBWP_RtCDFiJ2UjWl7PC2PE6JDrKf_TMX3e0tkaFGb5OcUgtqDfixBOqswi4gOv6YKxv8kQn9_ZDGZgVpO0lK3Uh2ptEMiBP8ZPsada25SyhouUl_LAEU47gFD"",
            ""notification"": {
                ""title"": """ + title + @""",
                ""body"": """ + body + @"""
            }
        }";

        string url = "https://fcm.googleapis.com/fcm/send";
        string authorizationHeader = "AAAAzo2uhFg:APA91bHF9c1x0Oisgihg2DssfqbKQZEaBnWmgGzJmfstel1cFyu3-NrQt0FmPgSio08xM990gpnBQdrzSXZrj4z0D5ZzfA985SzJ8vIQFlNTX2ho6IO--TIRljCbQcfBJHpSJZKtDrDh";

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
