using RegionalTimetableApp.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Parsing
{
    class DummyTokenGenerator : ITokenGenerator
    {
        private List<Token> tokens;
        private int index;

        public DummyTokenGenerator()
        {
            index = -1;
            tokens = new List<Token>()
            {
                new Token(Token.TokenType.RouteNumber, "#123", 0),
                new Token(Token.TokenType.City, "odense", 1),
                new Token(Token.TokenType.Time, "23:30", 1),
                new Token(Token.TokenType.City, "middelfart", 1),
                new Token(Token.TokenType.Time, "00:15", 1),
                new Token(Token.TokenType.RouteNumber, "#234", 2),
                new Token(Token.TokenType.City, "odense", 3),
                new Token(Token.TokenType.Time, "23:30", 3),
                new Token(Token.TokenType.City, "middelfart", 3),
                new Token(Token.TokenType.Time, "00:15", 3),
                new Token(Token.TokenType.End, "\0", 3)
            };
        }

        public Token GetCurrent()
        {
            if (index >= tokens.Count)
            {
                return new Token(Token.TokenType.End, "\0", -1);
            }
            else
            {
                return tokens[index];
            }
        }

        public bool MoveNext()
        {
            index++;
            return (index >= tokens.Count);
        }
    }
}
