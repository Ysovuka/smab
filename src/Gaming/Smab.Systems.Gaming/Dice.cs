using System;
using System.Security.Cryptography;

namespace Smab.Systems.Gaming
{
    public class Dice
    {
        private RandomNumberGenerator rnd = RandomNumberGenerator.Create();
        private byte[] mbuffer = new byte[4];

        public double Roll(double max)
        {
            rnd.GetBytes(mbuffer);
            UInt32 rand = BitConverter.ToUInt32(mbuffer, 0);
            double dbl = rand / (1.0 + UInt32.MaxValue);
            return dbl * max;
        }

        public int Roll(int max)
        {
            return (int)Roll((double)max);
        }
    }
}
