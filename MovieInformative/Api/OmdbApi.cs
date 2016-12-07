using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Api.Models;
using Newtonsoft.Json;


namespace Api
{
    public class OmdbApi
    {
        public OmdbModel GetByQuery(string name)
        {
            StringBuilder sb = new StringBuilder("http://www.omdbapi.com/?");
            sb.Append($"t={name}");
            sb.Append("&plot=short&r=json");
            
            using (var client = new WebClient())
            {
                var json = client.DownloadString(sb.ToString());
                var filmInfos = JsonConvert.DeserializeObject<OmdbModel>(json);

                return filmInfos;
            }
        }
    }
}
