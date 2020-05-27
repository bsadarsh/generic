using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace CognitivieApi2
{
    class Program
    {
        static string imageFilePath = @"C:\Users\swathi\Pictures\S.jpg";
        byte[] byteData;
        static void Main()
        {
                MakeRequest();
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();
            }
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        static async Task MakeRequest()
        {
                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b543f0287dd4430a806ba22870fa6a14");

                // Request parameters
                queryString["language"] = "en";
            string requestParameters =
                   "visualFeatures=Categories,Description,Color";
            var uri = "https://northeurope.api.cognitive.microsoft.com/vision/v3.0/analyze?" + requestParameters;

                HttpResponseMessage response;

                // Request body
                byte[] byteData = GetImageAsByteArray(imageFilePath);//Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }
            string contentString = await response.Content.ReadAsStringAsync();


             string imageFilePath1 = @"C:\Users\swathi\Pictures\S.jpg";

            System.Drawing.Image img = System.Drawing.Image.FromFile(imageFilePath1);

            Console.WriteLine("\nResponse:\n\n{0}\n",
                    JToken.Parse(contentString).ToString());

        }
        }
    }

