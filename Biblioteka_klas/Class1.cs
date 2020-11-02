using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Biblioteka_klas
{
    public class classLibrary
    {
        public static String Encryption(String input)
        {
            String output = null;
            char[] array = input.ToCharArray();
            System.Array.Reverse(array);
            foreach (char c in array)
            {
                output = output + c + "x";
            }
            return output;  
        }

        public static String Decryption(String input)
        {
            String output = "";
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i = i + 2)
            {
                output = output + array[i];
            }
            array = output.ToCharArray();
            System.Array.Reverse(array);
            output = new String(array);
            return output;
        }
    }
}
