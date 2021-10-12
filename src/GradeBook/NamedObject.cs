
namespace GradeBook
{

    // A base class used by the Book and other classes.
    public class NamedObject
    {
        // Constructor for the NamedObject class.
        public NamedObject(string name)
        {
            Name = name;
        }

        //Property on the NamedObject class.
        public string Name
        {
            get; set;
        }
    }
}