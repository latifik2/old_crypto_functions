using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace md4
{
    class Program
    {
        

        static void Main(string[] args)
        {
            int option = 0;
            string message;

            while (true)
            {
                Console.WriteLine("\n# Choose an option =>");
                Console.WriteLine("# 1 - MD4");
                Console.WriteLine("# 2 - DES Encryption");
                Console.WriteLine("# 3 - DES Decryption");
                Console.WriteLine("# 4 - Exit\n");
                option = Convert.ToInt16(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        message = Console.ReadLine();
                        md4.MD4(message);
                        break;
                    case 2:
                        message = Console.ReadLine();
                        des.DES(message);
                        break;
                    case 3:
                        Console.WriteLine("# Module in development...");
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("# ERROR! NO SUCH OPTION!");
                        break;
                }
            }
            

            
            
        }
    }
}
