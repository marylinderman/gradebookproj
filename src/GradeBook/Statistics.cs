using System;

namespace GradeBook
{
    public class Statistics
    {
        //FIELDS
        public double Average { get { return Sum / Count; } }
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:  //runtime variable will get value in result.Average
                        return 'A';

                    case var d when d >= 80.0:
                        return 'B';

                    case var d when d >= 70.0:
                        return 'C';

                    case var d when d >= 60.0:
                        return 'D';

                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Count;


        //Constructor for Statistics class.
        public Statistics()
        {
            Count = 0;
            Sum = 0.0;
            High = double.MinValue;
            Low = double.MaxValue;

        }


        public void Add(double number)
        {
            Sum += number;
            Count++;

            High = Math.Max(number, High);
            Low = Math.Min(number, Low);

        }


    }


}