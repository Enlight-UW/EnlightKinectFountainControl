using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace EnlightFountainControlLibrary.Models.Post
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ValveListControlModel : ControllerModel
    {
        [JsonProperty]
        private int bitmask;

        private BitArray valveSet;

        public ValveListControlModel(int controllerId, int bitmask = 0) : base(controllerId)
        {
            this.bitmask = bitmask;
            valveSet = new BitArray(bitmask);
        }

        public int Bitmask
        {
            get { return bitmask; }
        }

        public void SetValve(bool enable, int valveNumber)
        {
            valveSet.Set(valveNumber, enable);

            int[] temp = new int[1];
            valveSet.CopyTo(temp, 0);

            bitmask = temp[0];
        }
    }
}
