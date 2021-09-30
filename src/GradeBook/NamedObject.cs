
namespace GradeBook
{

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        // A base class used by the Book and other classes.
        public string Name
        {
            get; set;
        }
    }
}