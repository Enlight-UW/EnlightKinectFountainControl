using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using EnlightFountainControlLibrary.Messages;
using EnlightFountainControlLibrary.Models;


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

        public T SendMessage<T>(Message message) where T : IEnlightModelSerializer<T>, new()
        {
            Task<string> task = null;

            try
            {
                if (message.Type == MessageType.GET)
                    task = SendGetMessage(message);
                else
                    task = SendPostMessage(message as PostMessage);
            }
            catch (HttpRequestException)
            {
                return default(T);
            }

            // wait for task complete
            task.Wait();

            // parse the json
            // if an exception is thrown in parsing,
            // we know we got the wrong object
            try
            {
                T model = new T();
                model = model.FromJson<T>(task.Result);

                return (model == null) ? default(T) : model;
            }

            catch
            {
                return default(T);
            }
        }

        private async Task<string> SendGetMessage(Message message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(message.Url);

                return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<string> SendPostMessage(PostMessage message)
        {
            // inject API key
            message.Model.ApiKey = apiKey;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(message.Model.ToJson(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(message.Url, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
