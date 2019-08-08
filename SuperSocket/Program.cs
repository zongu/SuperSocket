
namespace SuperSocket
{
    using System;
    using System.Collections.Generic;
    using SuperSocket.Compoment;

    class Program
    {
        static void Main(string[] args)
        {
            foreach(var type in Enum.GetValues(typeof(ExcutiveRoleType)))
            {
                Console.WriteLine($"{(int)type}.{type}");
            }

            Console.Write("Mode:");
            var modeResult = Console.ReadLine();
            while (!new List<string>() { "0", "1" }.Contains(modeResult))
            {
                Console.Write($"Mode:");
                modeResult = Console.ReadLine();
            }

            IProcessCompoment process = null;
            switch ((ExcutiveRoleType)int.Parse(modeResult))
            {
                case ExcutiveRoleType.Server:
                    process = new ServerProcessCompoment();
                    break;
                case ExcutiveRoleType.Client:
                    process = new ClientProcessCompoment();
                    break;
                default:
                    break;
            }

            if (process != null)
            {
                process.Start();
                process.Stop();
            }

            Console.Read();
        }
    }
}
