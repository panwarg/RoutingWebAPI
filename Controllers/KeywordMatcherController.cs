using RoutingWebAPI.App_Start;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace RoutingWebAPI.Controllers
{
    public class KeywordMatcherController : ApiController
    {
        [HttpPost]
        public bool Post()
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
            reader.BaseStream.Position = 0;
            var document = reader.ReadToEnd();

            var isMatched = false;

            foreach (var key in ApplicationConfig.SourceKeyList)
            {
                var regex = new Regex(key);
                if (regex.IsMatch(document))
                {
                    isMatched = true;
                    break;
                }             
            }

            if (!isMatched) 
            {
                var httpClient = new HttpClient();
                var response = httpClient.PostAsync(ApplicationConfig.RedirectApiURL, new StringContent(document)).Result;
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK) 
                {
                    return true;
                }
                
            }

            return isMatched;
        }
    }
}