using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable.LexicalAnalysis
{
    class Token
    {
        public enum TokenType { RouteNumber, City, Time, Error, Whitespace, End }
        public readonly TokenType Type;
        public readonly string Lexeme;
        public readonly int LineNo;

        public Token(TokenType token, string lexeme, int lineNo)
        {
            Type = token;
            Lexeme = lexeme;
            LineNo = lineNo;
        }
    }
}
