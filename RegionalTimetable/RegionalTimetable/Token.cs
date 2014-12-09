using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    class Token
    {
        public enum TokenType { RouteNumber, City, Time, Error, Whitespace, End }
        public readonly TokenType Type;
        public readonly string Lexeme;

        public Token(TokenType token, string lexeme)
        {
            Type = token;
            Lexeme = lexeme;

            // convert string to tokentype:
            // Type = (TokenType) Enum.Parse(typeof(TokenType), "Other");
        }
    }
}
