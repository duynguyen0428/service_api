using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Ultility
{
    public class HttpUltility
    {
        public static async Task<string>  PostRequest<K>(string uri,K request, string contentType)
        {
            string responseBody = string.Empty;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (HttpClient client = new HttpClient())
            {
                ///set timeout 5 mins
                client.Timeout = System.TimeSpan.FromMinutes(5);
                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, contentType);
                    HttpResponseMessage response = await client.PostAsync(uri, stringContent);
                    var stream = await response.Content.ReadAsStreamAsync();
                    StreamReader loResponseStream = new StreamReader(stream, Encoding.UTF8);
                    responseBody = await loResponseStream.ReadToEndAsync();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (HttpRequestException e)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    //TODO: Log exceptions

                    throw;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    throw;
                }
                return responseBody;
            }
        }

        public static async Task<string> GetRequest(string uri)
        {
            string responseBody = string.Empty;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (HttpClient client = new HttpClient())
            {
                ///set timeout 5 mins
                client.Timeout = System.TimeSpan.FromMinutes(5);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    var stream = await response.Content.ReadAsStreamAsync();
                    StreamReader loResponseStream = new StreamReader(stream, Encoding.UTF8);
                    responseBody = await loResponseStream.ReadToEndAsync();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (HttpRequestException e)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    //TODO: Log exceptions

                    throw;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    throw;
                }
                return responseBody;
            }
        }

    }
}
