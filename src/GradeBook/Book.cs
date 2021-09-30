using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();

    }

    public interface IBook
    {

        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            var writer = File.AppendText($"{Name}.txt");
            writer.WriteLine(grade);
        }

        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryBook : Book
    {

        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;

        }

        //METHODS

        public override void AddGrade(double grade)
        {

            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public void AddGrade(char letter)
        {

            switch (letter)
            { //variable that switch operates on
                case 'A':
                    AddGrade(90.0);
                    break; //break out of switch so don't fall through
                case 'B':
                    AddGrade(80.0);
                    break;
                case 'C':
                    AddGrade(70.0);
                    break;
                case 'D':
                    AddGrade(60.0);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public override Statistics GetStatistics()
        {

            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;


            for (var i = 0; i < grades.Count; i++)
            {

                result.High = Math.Max(grades[i], result.High);
                result.Low = Math.Min(grades[i], result.Low);
                result.Average += grades[i];
            }

            result.Average /= grades.Count;

            switch (result.Average)
            {
                case var d when d >= 90.0:  //runtime variable will get value in result.Average
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;

            }

            return result;

        }

        // SAMPLE LOOPS

        //WHILE LOOP
        // var index = 0;
        // while (index < grades.Count)
        // {

        //     result.High = Math.Max(grades[index], result.High);
        //     result.Low = Math.Min(grades[index], result.Low);
        //     result.Average += grades[index];
        //     index++;

        // }

        //DO WHILE LOOP
        // do
        // {

        //     result.High = Math.Max(grades[index], result.High);
        //     result.Low = Math.Min(grades[index], result.Low);
        //     result.Average += grades[index];
        //     index++;

        // } while (index < grades.Count);

        //FOREACH LOOP
        // foreach (var grade in grades)
        // {

        //     result.High = Math.Max(grade, result.High);
        //     result.Low = Math.Min(grade, result.Low);
        //     result.Average += grade;
        // }








        // public void ShowStatistics(){


        // Console.WriteLine($"The highest grade is {highGrade}.");
        // Console.WriteLine($"The lowest grade is {lowGrade}.");
        // Console.WriteLine($"The average grade is {totalGrade/grades.Count}.");

        // }


        //FIELDS/Properties
        private List<double> grades;



        const string category = "science";


        public override event GradeAddedDelegate GradeAdded;
    }
}




