using System;
using System.Linq;

namespace Unindent
{
    class Program
    {
        private static void Main(string[] args)
        {
            foreach (var line in args)
            {
                Unindent(line);
            }
        }

        private static string Unindent(string input)
        {
            var lines = input.Split(Environment.NewLine);

            // Usually the first or second line is the one with indentation.
            // If it's any further, ignore it.
            var indent = 0;
            foreach (string line in lines.Take(2))
            {
                foreach (char c in line)
                {
                    if (char.IsWhiteSpace(c))
                        indent += GetWhitespaceLength(c);
                    else
                        break;
                }
                if (indent > 0)
                    break;
            }

            for (var i = 0; i < lines.Length; i++)
            {
                var offset = 0;
                var length = 0;
                foreach (char c in lines[i])
                {
                    if (char.IsWhiteSpace(c) && length < indent)
                    {
                        offset++;
                        length += GetWhitespaceLength(c);
                    }
                    else
                        break;
                }
                lines[i] = lines[i].Substring(offset);
            }
            Console.WriteLine($"Space: {indent}");
            return string.Join(Environment.NewLine, lines);
        }

        private static int GetWhitespaceLength(char c) =>
            c switch
            {
                ' '  => 1,
                '\t' => 4,
                _    => 1
            };
    }
}