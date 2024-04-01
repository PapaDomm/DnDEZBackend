using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace DnDEZBackEnd.Models
{
    public class DnDRaceDAL
    {
        public static HttpClient dndClient = new HttpClient()
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/api/races/")
        };

        public async static Task<Race>? getRace(string race)
        { 
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/races/{race}";

            


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync($"{race}");
            //HttpWebResponse respone = (HttpWebResponse)request.GetResponse();

            //Convert to JSON
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string JSON = reader.ReadToEnd();

            //Adjust
            //Convert to C#
            //Install Newtonsoft.Json
            Race? result = JsonConvert.DeserializeObject<Race>(JSON);

            if(result == null)
            {
                return result = new Race();
            }

            return result;
        }
        public static async Task<DnDBasicObject>? getAllRaces()
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/races";


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

            if(result == null)
            {
                return result = new DnDBasicObject();
            }
            return result;
        }

    }
}
