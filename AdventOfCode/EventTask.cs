

namespace AdventOfCode
{
    internal abstract class EventTask
    {
        public abstract string GetAnswer1();
        public abstract string GetAnswer2();

        protected abstract void ReadInput();

        protected string GetInputPath()
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
