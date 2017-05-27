using System.Reflection;
using NUnitLite;

namespace OrdersCollector.EventStore.Tests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args);
        }
    }
}