
namespace SuperSocket
{
    using System;
    using System.Collections.Generic;
    using SuperSocket.Compoment;
    using SuperSocket.Model;

    class Program
    {
        static void Main(string[] args)
        {
            foreach(var value in Enum.GetValues(typeof(ExcutiveRoleEnum)))
            {
                Console.WriteLine($"{(int)value}.{value}");
            }

            Console.Write($"Mode:");
            var modeResult = Console.ReadLine();
            while(!new List<string>() { "0", "1" }.Contains(modeResult))
            {
                Console.Write($"Mode:");
                modeResult = Console.ReadLine();
            }

            ProcessCompoment program = null;
            switch ((ExcutiveRoleEnum)int.Parse(modeResult))
            {
                case ExcutiveRoleEnum.Server:
                    program = new ServerProcessCompoment();
                    break;
                case ExcutiveRoleEnum.Client:
                    program = new ClientProcessCompoment();
                    break;
                default:
                    break;
            }

            if(program != null)
            {
                program.Start();
                program.Stop();
            }

            Console.Read();
        }
    }
}
