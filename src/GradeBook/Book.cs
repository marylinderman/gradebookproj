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
            //returns object that writes to the file
            //writer variable writes to end of file.
            using (
            var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }

        }

        public override Statistics GetStatistics()
        {

            var result = new Statistics();

            using (
            var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;

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

            for (var i = 0; i < grades.Count; i++)
            {
                result.Add(grades[i]);
            }
            return result;
        }




        //FIELDS
        private List<double> grades;

        const string category = "science";

        public override event GradeAddedDelegate GradeAdded;
    }
}




