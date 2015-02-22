using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using EnlightFountainControlLibrary.Models;

namespace EnlightFountainControlLibrary.Messages
{
    /// <summary>
    /// The MessageType is to differentiate between messages that require GET or POST HTTP headers.
    /// </summary>
    public enum MessageType
    {
        GET, POST
    }

    /// <summary>
    /// The Message abstract class is the base of all packets sent to the webserver.
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Returns the url of the message on the webserver. For example, if we were querying control, the
        /// URL would be: "/control/query".
        /// </summary>
        public abstract string Url
        {
            get;
        }

        /// <summary>
        /// Returns the HTTP type of the message (GET or POST).
        /// </summary>
        public virtual MessageType Type
        {
            get { return MessageType.GET; }
        }
    }
}
