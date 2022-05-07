using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md4
{
    class md4
    {
        static private int addExtraBits(ref byte[] byteMessage)
        {
            const byte firstNotZeroBit = 0x80;
            int startingLength = byteMessage.Length * 8;


            Console.WriteLine("Current length is: " + byteMessage.Length);
            Array.Resize(ref byteMessage, byteMessage.Length + 1);
            byteMessage[byteMessage.Length - 1] = firstNotZeroBit;

            while (byteMessage.Length % 64 != 56)
            {
                Array.Resize(ref byteMessage, byteMessage.Length + 1);
                byteMessage[byteMessage.Length - 1] = 0x0;
            }

            Console.WriteLine("Extended length is: " + byteMessage.Length);


            return startingLength;
        }

        static private void addStartingLenght(UInt64 startingLength, ref byte[] byteMessage)
        {
            byte[] IntBytes = BitConverter.GetBytes(startingLength);
            Console.WriteLine("\nHEX: " + BitConverter.ToString(IntBytes));

            byte[] firstDWORD = new byte[4];
            byte[] secondDWORD = new byte[4];

            foreach (var item in IntBytes)
            {
                Console.WriteLine(Convert.ToString(item, 2));

            }

            for (int i = 0, j = 0; i < 8; i++)
            {

                if (i < 4) secondDWORD[i] = IntBytes[i];
                else
                {
                    firstDWORD[j++] = IntBytes[i];
                }
            }
            Console.WriteLine("\nFist word " + BitConverter.ToString(firstDWORD));
            Console.WriteLine("Second word: " + BitConverter.ToString(secondDWORD));

            for (int i = 0, j = 0; i < 8; i++)
            {
                Array.Resize(ref byteMessage, byteMessage.Length + 1);
                if (i < 4) byteMessage[byteMessage.Length - 1] = firstDWORD[i];
                else byteMessage[byteMessage.Length - 1] = secondDWORD[j++];
            }
        }

        static private void addStartingLengthStraightOrder(UInt64 startingLength, ref byte[] byteMessage)
        {
            byte[] IntBytes = BitConverter.GetBytes(startingLength);
            byte[] firstDWORD = new byte[4];
            byte[] secondDWORD = new byte[4];




            for (int i = 0, j = 0; i < 8; i++)
            {
                if (i < 4) firstDWORD[i] = IntBytes[i];
                else secondDWORD[j++] = IntBytes[i];
            }

            Console.WriteLine("\nFist word " + BitConverter.ToString(firstDWORD));
            Console.WriteLine("Second word: " + BitConverter.ToString(secondDWORD));

            for (int i = 0, j = 0; i < 8; i++)
            {
                Array.Resize(ref byteMessage, byteMessage.Length + 1);
                if (i < 4) byteMessage[byteMessage.Length - 1] = firstDWORD[i];
                else byteMessage[byteMessage.Length - 1] = secondDWORD[j++];
            }
        }

        static private UInt32 F(UInt32 X, UInt32 Y, UInt32 Z)
        {
            return (X & Y) | (~X & Z);
        }

        static private UInt32 G(UInt32 X, UInt32 Y, UInt32 Z)
        {
            return (X & Y) | (X & Z) | (Y & Z);
        }

        static private UInt32 H(UInt32 X, UInt32 Y, UInt32 Z)
        {
            return X ^ Y ^ Z;
        }

        static private UInt32 loopOffset(UInt32 dword, int offset)
        {
            return (dword << offset) | (dword >> (32 - offset));
        }

        static private UInt32[] blocks16Words(ref byte[] byteMessage)
        {

            int size = byteMessage.Length / 4;
            UInt32[] X = new UInt32[16];
            UInt32[] M = new UInt32[size];
            UInt32 A = 0x67452301;
            UInt32 B = 0xefcdab89;
            UInt32 C = 0x98badcfe;
            UInt32 D = 0x10325476;
            UInt32[] result = new UInt32[4];

            Console.WriteLine(byteMessage.Length / 4.0);

            for (int k = 0, l = 0; k < byteMessage.Length; k += 4, l++)
            {
                M[l] = BitConverter.ToUInt32(byteMessage, k);
            }


            for (int i = 0; i < M.Length; i += 16)
            {
                for (int j = 0; j < 16; j++)
                {
                    X[j] = M[i + j];
                }


                UInt32 AA = A, BB = B, CC = C, DD = D;

                //Round 1

                A = loopOffset((A + F(B, C, D) + X[0]), 3);
                D = loopOffset((D + F(A, B, C) + X[1]), 7);
                C = loopOffset((C + F(D, A, B) + X[2]), 11);
                B = loopOffset((B + F(C, D, A) + X[3]), 19);

                A = loopOffset((A + F(B, C, D) + X[4]), 3);
                D = loopOffset((D + F(A, B, C) + X[5]), 7);
                C = loopOffset((C + F(D, A, B) + X[6]), 11);
                B = loopOffset((B + F(C, D, A) + X[7]), 19);

                A = loopOffset((A + F(B, C, D) + X[8]), 3);
                D = loopOffset((D + F(A, B, C) + X[9]), 7);
                C = loopOffset((C + F(D, A, B) + X[10]), 11);
                B = loopOffset((B + F(C, D, A) + X[11]), 19);

                A = loopOffset((A + F(B, C, D) + X[12]), 3);
                D = loopOffset((D + F(A, B, C) + X[13]), 7);
                C = loopOffset((C + F(D, A, B) + X[14]), 11);
                B = loopOffset((B + F(C, D, A) + X[15]), 19);

                //Round 2

                A = loopOffset((A + G(B, C, D) + X[0] + 0x5A827999), 3);
                D = loopOffset((D + G(A, B, C) + X[4] + 0x5A827999), 5);
                C = loopOffset((C + G(D, A, B) + X[8] + 0x5A827999), 9);
                B = loopOffset((B + G(C, D, A) + X[12] + 0x5A827999), 13);

                A = loopOffset((A + G(B, C, D) + X[1] + 0x5A827999), 3);
                D = loopOffset((D + G(A, B, C) + X[5] + 0x5A827999), 5);
                C = loopOffset((C + G(D, A, B) + X[9] + 0x5A827999), 9);
                B = loopOffset((B + G(C, D, A) + X[13] + 0x5A827999), 13);

                A = loopOffset((A + G(B, C, D) + X[2] + 0x5A827999), 3);
                D = loopOffset((D + G(A, B, C) + X[6] + 0x5A827999), 5);
                C = loopOffset((C + G(D, A, B) + X[10] + 0x5A827999), 9);
                B = loopOffset((B + G(C, D, A) + X[14] + 0x5A827999), 13);

                A = loopOffset((A + G(B, C, D) + X[3] + 0x5A827999), 3);
                D = loopOffset((D + G(A, B, C) + X[7] + 0x5A827999), 5);
                C = loopOffset((C + G(D, A, B) + X[11] + 0x5A827999), 9);
                B = loopOffset((B + G(C, D, A) + X[15] + 0x5A827999), 13);

                //Round 3

                A = loopOffset((A + H(B, C, D) + X[0] + 0x6ED9EBA1), 3);
                D = loopOffset((D + H(A, B, C) + X[8] + 0x6ED9EBA1), 9);
                C = loopOffset((C + H(D, A, B) + X[4] + 0x6ED9EBA1), 11);
                B = loopOffset((B + H(C, D, A) + X[12] + 0x6ED9EBA1), 15);

                A = loopOffset((A + H(B, C, D) + X[2] + 0x6ED9EBA1), 3);
                D = loopOffset((D + H(A, B, C) + X[10] + 0x6ED9EBA1), 9);
                C = loopOffset((C + H(D, A, B) + X[6] + 0x6ED9EBA1), 11);
                B = loopOffset((B + H(C, D, A) + X[14] + 0x6ED9EBA1), 15);

                A = loopOffset((A + H(B, C, D) + X[1] + 0x6ED9EBA1), 3);
                D = loopOffset((D + H(A, B, C) + X[9] + 0x6ED9EBA1), 9);
                C = loopOffset((C + H(D, A, B) + X[5] + 0x6ED9EBA1), 11);
                B = loopOffset((B + H(C, D, A) + X[13] + 0x6ED9EBA1), 15);

                A = loopOffset((A + H(B, C, D) + X[3] + 0x6ED9EBA1), 3);
                D = loopOffset((D + H(A, B, C) + X[11] + 0x6ED9EBA1), 9);
                C = loopOffset((C + H(D, A, B) + X[7] + 0x6ED9EBA1), 11);
                B = loopOffset((B + H(C, D, A) + X[15] + 0x6ED9EBA1), 15);

                A += AA;
                B += BB;
                C += CC;
                D += DD;
            }

            result[0] = A;
            result[1] = B;
            result[2] = C;
            result[3] = D;

            return result;
        }

        static private UInt32 endiannessFix(UInt32 value)
        {
            return (((value & 0xff) << 24) | ((value & 0xff00) << 8) | ((value & 0xff0000) >> 8) | ((value & 0xff000000) >> 24));
        }

        static public void MD4(string message)
        {
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            UInt64 startingLength = Convert.ToUInt64(addExtraBits(ref byteMessage));
            Console.WriteLine("\nStarting length: " + startingLength);
            addStartingLengthStraightOrder(startingLength, ref byteMessage);
            

            UInt32[] hashedWords = blocks16Words(ref byteMessage);

            Console.WriteLine($"A: {hashedWords[0]}\nB: {hashedWords[1]}\nC: {hashedWords[2]}\nD: {hashedWords[3]}");
            Console.WriteLine("HASH: " + Convert.ToString(endiannessFix(hashedWords[0]), 16) + Convert.ToString(endiannessFix(hashedWords[1]), 16)
                + Convert.ToString(endiannessFix(hashedWords[2]), 16) + Convert.ToString(endiannessFix(hashedWords[3]), 16));
        }
    }
}
