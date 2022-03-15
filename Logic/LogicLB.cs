using LoadBalancer.Models;
using System.Linq;

namespace LoadBalancer.Logic
{
    public class LogicLB
    {
        public static Machine getMachine()
        {
            IOrderedEnumerable<Machine> machines = Program.machines.OrderByDescending(x => x.Property).ThenBy(x => decimal.Divide(x.ReqUsed, x.MaxReq) * 100);

            return machines.First();
        }

    }
}
