using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models
{
    /// <summary>
    /// This interface defines two common methods to all Enlight data models.
    /// </summary>
    /// <typeparam name="T">The type T to return when deserializing the raw JSON.</typeparam>
    public interface IEnlightModelSerializer<T>
    {
        /// <summary>
        /// The FromJson method attempts to deserialize the JSON given.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into</typeparam>
        /// <param name="json">The JSON to deserialize</param>
        /// <exception cref="JsonSerializationException">Thrown when strict deserialization fails.</exception>
        /// <returns>A deserialized object</returns>
        T FromJson<T>(string json);

        /// <summary>
        /// Serializes the class into JSON.
        /// </summary>
        /// <returns>a JSON string containing the instance's properties</returns>
        string ToJson();
    }

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseModel : IEnlightModelSerializer<BaseModel>
    {
        protected static JsonSerializerSettings settings;

        static BaseModel()
        {
            settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;
        }

        public virtual BaseModel FromJson<BaseModel>(string json)
        {
            throw new NotImplementedException("Cannot deserialize into the abstract class EnlightBaseModel!");
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
