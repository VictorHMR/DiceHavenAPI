using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Utils.API
{
    public class Imgur
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly IConfiguration _configuration;
        private const string ImgurApiUrl = "https://api.imgur.com/3/upload";


        public Imgur(IConfiguration config)
        {
            _configuration = config;
            _httpClient = new HttpClient();
            _clientId = _configuration["ImgurClientID"];
        }

        public string uploadImageBase64(string base64Image)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/plain");
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);
            var response = httpclient.PostAsync("https://api.imgur.com/3/Image", new StringContent(base64Image)).Result;
            var stringcontent = response.Content.ReadAsStringAsync().Result;
            var imgurResponse = JObject.Parse(stringcontent);
            var link = imgurResponse["data"]["link"].Value<string>();
            return link;
        }
    }
}
