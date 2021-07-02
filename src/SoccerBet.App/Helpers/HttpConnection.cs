using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace SoccerBet.App.Helpers
{
    public class HttpConnection
    {
        private static JsonSerializerSettings referenceIgnore = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        };
        public static async Task<T> ExecuteRequest<T, Y>(string url, RestSharp.Method httpMethod, Y request) where T : class  where Y: class
        {
            var client = new RestClient(url);
            var httpRequest = new RestRequest(httpMethod);
            httpRequest.AddHeader("Content-type", "application/json;charset=UTF-8");
            httpRequest.AddJsonBody(JsonConvert.SerializeObject(request, referenceIgnore));

            try
            {
                IRestResponse httpResponse = await client.ExecuteAsync(httpRequest);
                var content = httpResponse.Content.ToString();
                var response = JsonConvert.DeserializeObject<T>(httpResponse.Content);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
