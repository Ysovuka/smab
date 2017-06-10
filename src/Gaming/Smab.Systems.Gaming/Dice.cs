using System;
using System.Security.Cryptography;

namespace Smab.Systems.Gaming
{
    public class Dice
    {
        private RandomNumberGenerator rnd = RandomNumberGenerator.Create();
        private byte[] mbuffer = new byte[8];

        public double Roll(double max)
        {
            rnd.GetBytes(mbuffer);
            UInt64 rand = BitConverter.ToUInt64(mbuffer, 0);
            double results = rand / (1.0 + UInt64.MaxValue);
            return results * max;
        }

        public int Roll(int max)
        {
            return (int)Roll((double)max);
        }
    }
}
