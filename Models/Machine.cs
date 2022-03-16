using System;

namespace LoadBalancer.Models
{
    public class Machine
    {
        public Machine(Uri url, string name, bool property, int maxReq, int reqUsed = 0)
        {
            Url = url;
            Name = name;
            Property = property;
            MaxReq = maxReq;
            ReqUsed = reqUsed;
        }

        public Uri Url { get; set; }
        public string Name { get; set; }
        public bool Property { get; set; }
        public int MaxReq { get; set; }
        public int ReqUsed { get; set; }

    }
}
