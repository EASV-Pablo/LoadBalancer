using System;

namespace LoadBalancer.Models
{
    public class Machine
    {
        public Machine(Uri url, string name, bool property)
        {
            Url = url;
            Name = name;
            Property = property;
        }

        public Uri Url { get; set; }
        public string Name { get; set; }
        public bool Property { get; set; }

    }
}
