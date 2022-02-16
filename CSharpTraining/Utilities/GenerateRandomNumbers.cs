using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTraining.Utilities
{
    public class GenerateRandomNumbers
    {
        private static Random RandomNumber = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[RandomNumber.Next(s.Length)]).ToArray());
        }
    }
}
