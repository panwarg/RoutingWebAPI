using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace RoutingWebAPI.Controllers
{
    public class KeywordMatcherController : ApiController
    {
        // GET: api/KeywordMatcher/key1
        public bool Get(string id)
        {
            var sourceKeyList = ConfigurationManager.AppSettings["SourceKeyList"];
            if (string.IsNullOrEmpty(sourceKeyList)) 
            {
                return false;
            }

            var keys = sourceKeyList.Split(',');
            var isMatched = false;

            foreach (var key in keys)
            {
                if (key.Contains(id))
                {
                    isMatched = true;
                    break;
                }                    
            }

            if (!isMatched) 
            {
                var redirectApiURL = ConfigurationManager.AppSettings["RedirectApiURL"];
                
                if(!string.IsNullOrEmpty(redirectApiURL)) 
                {
                    var httpClient = new HttpClient();
                    var response = httpClient.GetAsync(redirectApiURL + $"/{id}").Result;
                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK) 
                    {
                        return true;
                    }
                }
            }

            return isMatched;
        }
    }
}