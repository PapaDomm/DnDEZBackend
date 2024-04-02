using DnDEZBackEnd.Models;
using Newtonsoft.Json;
using System.Net;

namespace DnDEZBackend.Models.DALs
{
    public class DnDClassDAL
    {
        public static HttpClient dndClient = new HttpClient()
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/api/classes/")
        };

        public static async Task<DnDBasicObject>? getAllClasses()
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/classes";


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync("");
            //HttpWebResponse respone = (HttpWebResponse)request.GetResponse();

            //Convert to JSON
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string JSON = reader.ReadToEnd();

            //Adjust
            //Convert to C#
            //Install Newtonsoft.Json
            DnDBasicObject? result = JsonConvert.DeserializeObject<DnDBasicObject>(JSON);

            if (result == null)
            {
                return result = new DnDBasicObject();
            }

            return result;
        }
    }
}
