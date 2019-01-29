using Kaomi.Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Kaomi.Client.Logic
{
    /// <summary>
    /// Makes Rest requests, or, Restquests.
    /// </summary>
    internal static class Restquest
    {
        internal static T Get<T>(IpAddress ip, int port, string query)
        {
            var web = new HttpClient();
            var request = web.GetAsync($"http://{ip.ToString()}:{port}/{query}");
            request.Wait();
            var response = request.Result;
            var content = response.Content.ReadAsStringAsync();
            content.Wait();
            var type = JsonConvert.DeserializeObject<T>(content.Result);

            return type;
        }
    }
}
