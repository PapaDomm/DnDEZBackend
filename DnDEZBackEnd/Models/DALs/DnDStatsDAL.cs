using DnDEZBackEnd.Models;
using Newtonsoft.Json;

namespace DnDEZBackend.Models.DALs
{
    public class DnDStatsDAL
    {
        public static HttpClient dndClient = new HttpClient()
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/api/skills/")
        };

        public static async Task<DnDSkill>? getSkill(string skill)
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/classes";


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync($"{skill}");
            //HttpWebResponse respone = (HttpWebResponse)request.GetResponse();

            //Convert to JSON
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string JSON = reader.ReadToEnd();

            //Adjust
            //Convert to C#
            //Install Newtonsoft.Json
            DnDSkill? result = JsonConvert.DeserializeObject<DnDSkill>(JSON);

            if (result == null)
            {
                return result = new DnDSkill();
            }

            return result;
        }
    }
}
