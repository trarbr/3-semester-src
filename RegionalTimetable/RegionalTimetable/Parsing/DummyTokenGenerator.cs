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
                new Token(Token.TokenType.Time, "13:30", 1),
                new Token(Token.TokenType.End, "\0", 1)
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
