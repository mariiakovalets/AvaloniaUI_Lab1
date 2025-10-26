using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using System.IO;

namespace TableManager.App.Controllers
{
    public class Parser
    {
        public static (bool isValid, string error) CheckSyntax(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return (false, "#ERROR");

            if (!expression.StartsWith("="))
                return (true, "");

            if (expression.Contains("  "))
                return (false, "#ERROR");

            try
            {
                var inputStream = new AntlrInputStream(expression);
                var lexer = new TableExpressionLexer(inputStream);
                
                lexer.RemoveErrorListeners();
                var lexerErrorListener = new SyntaxErrorListener();
                lexer.AddErrorListener(lexerErrorListener);
                
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new TableExpressionParser(tokenStream);
                
                parser.RemoveErrorListeners();
                var parserErrorListener = new SyntaxErrorListener();
                parser.AddErrorListener(parserErrorListener);
                
                var tree = parser.formula();
                
                if (lexerErrorListener.HasErrors || parserErrorListener.HasErrors)
                {
                    return (false, "#ERROR");
                }
                
                return (true, "");
            }
            catch (Exception)
            {
                return (false, "#ERROR");
            }
        }

        public static List<string> Parse(string expression)
        {
            var tokens = new List<string>();

            if (string.IsNullOrWhiteSpace(expression))
                return tokens;

            if (!expression.StartsWith("="))
            {
                tokens.Add(expression);
                return tokens;
            }

            string expr = expression.Substring(1).Trim();

            var regex = new Regex(@"(\d+\.?\d*|[A-Z]+\d+|inc|dec|mod|div|[+\-*/()=<>]|<=|>=|<>)");
            var matches = regex.Matches(expr);

            foreach (Match match in matches)
            {
                tokens.Add(match.Value);
            }

            return tokens;
        }

        public static List<string> GetDependencies(string expression)
        {
            var dependencies = new List<string>();

            if (!expression.StartsWith("="))
                return dependencies;

            var regex = new Regex(@"[A-Z]+\d+");
            var matches = regex.Matches(expression);

            foreach (Match match in matches)
            {
                if (!dependencies.Contains(match.Value))
                    dependencies.Add(match.Value);
            }

            return dependencies;
        }
    }

    public class SyntaxErrorListener : IAntlrErrorListener<IToken>, IAntlrErrorListener<int>
    {
        public bool HasErrors { get; private set; } = false;

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            HasErrors = true;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            HasErrors = true;
        }
    }
}