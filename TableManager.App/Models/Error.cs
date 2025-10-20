namespace TableManager.App.Models
{
    public class Error
    {
        public string Message { get; set; } = "";

        public Error(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }

        public static void ReportError(string message)
        {
            System.Console.WriteLine($"ERROR: {message}");
        }
    }
}