using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnlightFountainControlLibrary.Models;

namespace EnlightFountainControlLibrary.Messages
{
    public abstract class PostMessage : Message
    {
        protected PostMessageModel model;

        public sealed override MessageType Type
        {
            get { return MessageType.POST; }
        }

        public PostMessageModel Model
        {
            get { return model; }
        }
    }
}
