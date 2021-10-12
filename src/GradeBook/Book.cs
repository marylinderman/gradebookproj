using System;
using System.Collections.Generic;
using System.IO;

//The namespace is used to group the code for the GradeBook together.
namespace GradeBook
{
    //A delegate is a type that describes a method with a specifc
    //parameter list and return type. Its a type variable that
    //can hold a reference to a method. Used in conjunction with an event.
    public delegate void GradeAddedDelegate(object sender, EventArgs args);


    //An abstract class has at least one property or method that isn't implemented.
    //It is intended for use as a base class and can't be instantiated.
    //The Book class is derived from the NamedObject class,
    //and it implements the IBook interface.
    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();

    }
    //An interface defines set of related properties and methods.
    //It only provides the signature for them but no implementations.
    //A class implementing the interaface defines the code for each method or property.
    //Demonstrates the concept of polymorphism in OOP, that is interface based polymorphism.
    public interface IBook
    {

        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    //The DiskBook class is derived from the Book class.
    //It inherits from the NamedObject class and must implement the IBook interface. 
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        //This method overrides the AddGrade() method in the 
        //abstract Book class.
        public override void AddGrade(double grade)
        {
            //Returns object that writes to the file.
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
        //The following methods illustrate an overloaded method.
        //Both versions of the method have the same name but take 
        //different parameters. They could also have been designed to take a 
        //different number as well as different types of parameters.
        //Note the return type isn't part of the method signature.
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
        //This method uses a switch statement. It takes a letter grade
        //and then uses the switch statement to determine the numeric value 
        //for the grade.
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

            //Uses for loop to iterate through the grades list.
            for (var i = 0; i < grades.Count; i++)
            {
                //Calls the Add method on the Statistics class
                //to update the statistics values.
                result.Add(grades[i]);
            }
            return result;
        }





        //A List of numbers of type doubles. It uses the C# generics like List<T>.
        //A list is dynamically sizeable, which is an advantage over an array that requires
        //a size specified when it is declared.
        private List<double> grades;
        //An example of a constant.
        const string category = "science";

        //An event is used to notify other objects when a specifc action occurs in an application.
        //An event is handled by that you add a method to an event that you want invoked.
        //When you raise an event, you invoke the delegate for it.
        public override event GradeAddedDelegate GradeAdded;
    }
}




