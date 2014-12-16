using RegionalTimetableApp.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Parsing
{
    class LexerTokenGenerator : ITokenGenerator
    {
        private List<Token> tokens;
        private int index;

        public LexerTokenGenerator(Lexer lexer)
        {
            tokens = lexer.GetTokens();
            index = -1;
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
