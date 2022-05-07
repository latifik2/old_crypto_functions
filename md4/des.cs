using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md4
{
    class des
    {
        private static BitArray InitialPermutation(BitArray bitBlock)
        {
            int[] IP = {57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
                        61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7,
                        56, 48, 40, 32, 24, 16, 8, 0, 58, 50, 42, 34, 26, 18, 10, 2,
                        60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6};

            BitArray shuffledBlock = new BitArray(64);

            for (int i = 0; i < 64; i++)
            {
                shuffledBlock[i] = bitBlock[IP[i]];
            }

            return shuffledBlock;
        }
        private static void generateKEYS(string key)
        {
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            BitArray bitKey = new BitArray(byteKey);
            BitArray extendedKey = new BitArray(64);

            int[] mask = {0, 1, 2, 3, 4, 5, 6, 0, 7, 8, 9, 10, 11, 12, 13, 0,
                          14, 15, 16, 17, 18, 19, 20, 0, 21, 22, 23, 24, 25, 26, 27, 0,
                          28, 29, 30, 31, 32, 33, 34, 0, 35, 36, 37, 38, 39, 40, 41, 0,
                          42, 43, 44, 45, 46, 47, 48, 0, 49, 50, 51, 52, 53, 54, 55, 0,
                          56, 57, 58, 59, 60, 61, 62, 0};

            for (int i = 0, j = 0; i < 64; i++, j++)
            {
                if(i % 7 == 0)
                {
                    extendedKey[i] = false;
                }
                extendedKey[i] = bitKey[j];
            }
        }
        public static void DES(string message)
        {
            const int sizeOfBlock = 8;
            byte[] byteBlock = new byte[8];
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            Console.WriteLine("Message length is: " + byteMessage.Length);

            while(byteMessage.Length % sizeOfBlock != 0)
            {
                Array.Resize(ref byteMessage, byteMessage.Length + 1);
                byteMessage[byteMessage.Length - 1] = 0x0;
            }

            for (int i = 0; i < byteMessage.Length; i += 8) //split message for 64 bits blocks
            {
                for (int j = 0; j < sizeOfBlock; j++) //fill block with 64 bits (8 bytes)
                {
                    byteBlock[j] = byteMessage[i + j];
                }
                generateKEYS("secretK");

                BitArray bitBlock = new BitArray(byteBlock);
                BitArray shuffledBitBlock = InitialPermutation(bitBlock);
            }
        }
    }
}
