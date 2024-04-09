using DnDEZBackEnd.Models;
using Newtonsoft.Json;

namespace DnDEZBackend.Models.DALs
{
    public class DnDStatsDAL
    {
        public static HttpClient dndClient = new HttpClient()
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/api/")
        };

        public static async Task<DnDSkill>? getSkill(string skill)
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/classes";


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync($"skills/{skill}");
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

        public static async Task<DnDBasicObject>? getAlignments()
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/classes";


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync("alignments");
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

        public static async Task<DnDBasicObject>? getAllRules()
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/classes";


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync("rule-sections");
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

        public static async Task<DnDRule>? getRule(string rule)
        {
            //Adjust
            //Setup
            //string url = $"https://www.dnd5eapi.co/api/classes";


            //Request
            using HttpResponseMessage response = await dndClient.GetAsync($"rule-sections/{rule}");
            //HttpWebResponse respone = (HttpWebResponse)request.GetResponse();

            //Convert to JSON
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string JSON = reader.ReadToEnd();

            //Adjust
            //Convert to C#
            //Install Newtonsoft.Json
            DnDRule? result = JsonConvert.DeserializeObject<DnDRule>(JSON);

            if (result == null)
            {
                return result = new DnDRule();
            }

            return result;
        }
    }
}
