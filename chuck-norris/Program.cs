using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuck_norris
{
    class Program
    {
        static void Main(string[] args)
        {
            char lastBit = '.';
            int countNumber = 0;
            string tmp, tmp7b;
            string fullBitMsg = string.Empty;
            string ccMessage = string.Empty;
            string message = Console.ReadLine();
            char[] charTab = message.ToCharArray();

            foreach (byte currentByte in getAsciiBytes(message))
            {
                tmp7b = Convert.ToString(currentByte, 2);
                if (tmp7b.Length < 7)
                { 
                    int miss = 7 - tmp7b.Length;
                    string s = tmp7b;
                    tmp7b = string.Empty;
                    for (int i = 0; i < miss; i++) { tmp7b += "0"; }
                    tmp7b += s;
                }
                fullBitMsg += tmp7b;
            }
            foreach (var bit in fullBitMsg)
            {
                Console.Error.WriteLine("Bit " + bit + " last : " + lastBit);
                if (lastBit == '.')
                {
                    lastBit = bit;
                }
                else if (bit != lastBit)
                {
                    if (!string.IsNullOrEmpty(ccMessage)) { ccMessage += " "; }

                    if (lastBit.ToString().Equals("0")) { ccMessage += "00 "; }
                    else { ccMessage += "0 "; }

                    tmp = string.Empty;
                    for (int i = 0; i < countNumber; i++) { tmp += "0"; }
                    ccMessage += tmp;

                    countNumber = 0;
                    lastBit = bit;
                }
                countNumber++;
            }
            if (lastBit != '.')
            { 
                if (!string.IsNullOrEmpty(ccMessage)) { ccMessage += " "; }

                if (lastBit.ToString().Equals("0")) { ccMessage += "00 "; }
                else { ccMessage += "0 "; }

                tmp = string.Empty;
                for (int i = 0; i < countNumber; i++) { tmp += "0"; }
                ccMessage += tmp;
            }
            Console.WriteLine(ccMessage);
        }
        public static byte[] GetAsciiBytesEvenParity(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);

            for (int ii = 0; ii < bytes.Length; ii++)
            {
                if (((0x6996 >> ((bytes[ii] ^ (bytes[ii] >> 4)) & 0xf)) & 1) != 0)
                {
                    bytes[ii] |= 0x80;
                }
            }

            return bytes;
        }
        public static byte[] getAsciiBytes(String input)
        {
            char[] c = input.ToCharArray();
            byte[] b = new byte[c.Length];
            for (int i = 0; i < c.Length; i++)
                b[i] = (byte)(c[i] & 0x007F);

            return b;
        }
        public static byte[] getAsciiBytesConvertHex(String input)
        {
            char[] c = input.ToCharArray();
            byte[] b = new byte[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                int intValue = (c[i] & 0x007F);
                int hexValue = int.Parse(intValue.ToString("X"));
                b[i] = (byte)hexValue;
            }
            return b;
        }
    }
}
