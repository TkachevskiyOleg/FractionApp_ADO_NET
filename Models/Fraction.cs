using System;

namespace _24._04._2024_Lab.Models
{
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction() { }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be zero.");

            Numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        public Fraction Add(Fraction other)
        {
            int num = this.Numerator * other.Denominator + other.Numerator * this.Denominator;
            int den = this.Denominator * other.Denominator;
            return new Fraction(num, den);
        }

        public Fraction Subtract(Fraction other)
        {
            int num = this.Numerator * other.Denominator - other.Numerator * this.Denominator;
            int den = this.Denominator * other.Denominator;
            return new Fraction(num, den);
        }

        public Fraction Multiply(Fraction other)
        {
            return new Fraction(this.Numerator * other.Numerator, this.Denominator * other.Denominator);
        }

        public Fraction Divide(Fraction other)
        {
            if (other.Numerator == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return new Fraction(this.Numerator * other.Denominator, this.Denominator * other.Numerator);
        }

        public void Simplify()
        {
            int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
            Numerator /= gcd;
            Denominator /= gcd;

            if (Denominator < 0)
            {
                Denominator *= -1;
                Numerator *= -1;
            }
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public double ToDecimal() => (double)Numerator / Denominator;
    }
}
