using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool SendMessage(Message message)
        {
            //TODO
            // remember to write custom json serializer
            return false;
        }

    }
}
