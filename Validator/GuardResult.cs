namespace Validator
{
    public class GuardResult
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public GuardResult(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
