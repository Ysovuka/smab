using System;
using System.Security.Cryptography;

namespace Smab.Systems.Gaming
{
    public class Dice
    {
        private RandomNumberGenerator rnd = RandomNumberGenerator.Create();
        private byte[] mbuffer = new byte[8];

        public double Roll(double times, double size, string @operator, double number)
        {
            if (!Validate(times, size, @operator, number)) throw new InvalidOperationException();

            double total = 0;
            for (int i = 1; i <= times; i++)
            {
                total += Solve(Roll(size), number, @operator);
            }
            
            if (total < 1) return 1;

            return total;
        }

        public double Roll(double max)
        {
            rnd.GetBytes(mbuffer);
            UInt64 rand = BitConverter.ToUInt64(mbuffer, 0);
            double results = rand / (1.0 + UInt64.MaxValue);
            return Math.Ceiling(results * max);
        }

        public int Roll(int max)
        {
            return (int)Roll((double)max);
        }

        private bool Validate(double times, double size, string @operator, double number)
        {
            if (times == 0) return false;
            if (size == 0) return false;
            if (@operator.Equals("*") && number == 0) return false;
            if (@operator.Equals("/") && number == 0) return false;
            if (Math.Pow(times, size) > double.MaxValue) return false;

            return true;
        }

        private double Solve(double left, double right, string @operator = "")
        {
            switch (@operator)
            {
                case "*":
                    return left * right;
                case "/":
                    return left / right;
                case "-":
                    return left - right;
                case "+":
                default:
                    return left + right;
            }
        }


    }
}
