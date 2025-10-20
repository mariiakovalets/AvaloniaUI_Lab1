using System.Collections.Generic;

namespace TableManager.App.Models
{
    public class Expression
    {
        public string Text { get; set; } = "";
        public List<string> Tokens { get; set; } = new List<string>();
        public List<string> Dependencies { get; set; } = new List<string>();

        public Expression(string text)
        {
            Text = text;
        }
    }
}