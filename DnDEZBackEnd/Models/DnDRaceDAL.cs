using Newtonsoft.Json;
using System.Net;

namespace DnDEZBackEnd.Models
{
    public class DnDRaceDAL
    {
        public static Race getRace(string race)
        {
            //Adjust
            //Setup
            string url = $"https://www.dnd5eapi.co/api/races/{race}";
            

            //Request
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse respone = (HttpWebResponse)request.GetResponse();

            //Convert to JSON
            StreamReader reader = new StreamReader(respone.GetResponseStream());
            string JSON = reader.ReadToEnd();

            //Adjust
            //Convert to C#
            //Install Newtonsoft.Json
            Race result = JsonConvert.DeserializeObject<Race>(JSON);

            return result;
        }
        public static DnDBasicObject getAllRaces()
        {
            //Adjust
            //Setup
            string url = $"https://www.dnd5eapi.co/api/races";


            //Request
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse respone = (HttpWebResponse)request.GetResponse();

            //Convert to JSON
            StreamReader reader = new StreamReader(respone.GetResponseStream());
            string JSON = reader.ReadToEnd();

            //Adjust
            //Convert to C#
            //Install Newtonsoft.Json
            DnDBasicObject result = JsonConvert.DeserializeObject<DnDBasicObject>(JSON);

            return result;
        }

    }
}
