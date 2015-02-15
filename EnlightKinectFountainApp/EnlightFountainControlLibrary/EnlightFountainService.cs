using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using EnlightFountainControlLibrary.Messages;

namespace EnlightFountainControlLibrary
{
    public class EnlightFountainService
    {
        private static EnlightFountainService instance;

        private string apiKey;
        private string baseUrl;

        public static EnlightFountainService GetInstance(string baseUrl, string apikey)
        {
            if (instance == null)
                instance = new EnlightFountainService(baseUrl, apikey);

            return instance;
        }

        private EnlightFountainService(string baseUrl, string apiKey)
        {
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
        }

        public string SendMessage(Message message)
        {
            // TODO - SendMessage should return an object of sorts
            // containing the deserialized data
            Task<string> task = null;

            try
            {
                if (message.HttpHeader == HttpRequestType.GET)
                    task = SendGetMessage(message);
                else
                    task = SendPostMessage(message);
            }
            catch (HttpRequestException)
            {
                // TODO HANDLE
            }

            // wait for task complete
            task.Wait();

            return task.Result;
        }

        private async Task<string> SendGetMessage(Message message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(message.Url);

                // TODO: need to parse JSON
                // should at some point return a deserialized Task<object>
                return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<string> SendPostMessage(Message message)
        {
            //TODO need to inject API key
            // serializer stuff goes here


            // TODO: need to parse JSON
            // should at some point return a deserialized Task<object>
            throw new NotImplementedException();
        }
    }
}
