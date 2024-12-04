

namespace AdventOfCode
{
    internal abstract class EventTask
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

        public abstract string GetAnswer1();
        public abstract string GetAnswer2();

        protected abstract void ReadInput();

        protected string GetPathToInput()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();

            var myType = GetType();
            var n = myType.Namespace.Replace('.', '\\');
            path = Path.Combine(path, n);
            path = Path.Combine(path, "Input.txt");

            return path;
        }
    }
}
