using System;

namespace LoadBalancer.Models
{
    public class Machine
    {
        public Machine(Uri url, string name, bool owned, int maxReq, int reqUsed = 0)
        {
            Url = url;
            Name = name;
            Owned = owned;
            MaxReq = maxReq;
            ReqUsed = reqUsed;
        }

        public Uri Url { get; set; }
        public string Name { get; set; }
        public bool Owned { get; set; }
        public int MaxReq { get; set; }
        public int ReqUsed { get; set; }

    }
}
