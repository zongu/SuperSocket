
namespace SuperSocket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SuperSocket.Compoment;

    class Program
    {
        static void Main(string[] args)
        {
            var process = new ServerProcessCompoment();
            process.Start();
            process.Stop();
        }
    }
}
