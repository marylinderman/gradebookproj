using System;
using System.Collections.Generic;
namespace GradeBook


{
    class Program
    {
        static void Main(string[] args)
        {

            var book = new Book("Joan's Grade Book");
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded -= OnGradeAdded;
            book.GradeAdded += OnGradeAdded;


            var done = false;





            while (!done)
            {
                Console.WriteLine("Add a grade or click q to quit.");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    done = true;
                    continue;

                    //could also use break and have true for the loop condition
                    //don't need done variable in this case.
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }

            var stats = book.GetStatistics();

            Console.WriteLine($"The highest grade is {stats.High}.");
            Console.WriteLine($"The lowest grade is {stats.Low}.");
            Console.WriteLine($"The average grade is {stats.Average}.");
            Console.WriteLine($"The letter grade is {stats.Letter}.");

            //Static method to handle the event raised by book when a grade is added.
            static void OnGradeAdded(object sender, EventArgs e)
            {
                Console.WriteLine("A grade was added.");
            }


        }
    }
}
