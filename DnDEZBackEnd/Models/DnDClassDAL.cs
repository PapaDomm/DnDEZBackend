using Newtonsoft.Json;
using System.Net;

namespace DnDEZBackEnd.Models
{
    public class DnDClassDAL
    {
        public static DnDBasicObject getAllClasses()
        {
            //Adjust
            //Setup
            string url = $"https://www.dnd5eapi.co/api/classes";


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
