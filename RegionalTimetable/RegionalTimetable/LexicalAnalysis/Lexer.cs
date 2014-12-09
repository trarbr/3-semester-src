using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegionalTimetable.LexicalAnalysis
{
    class Lexer
    {
        private ICharGenerator charGenerator;
        private int lineNo;

        public Lexer(ICharGenerator charGenerator)
        {
            this.charGenerator = charGenerator;
            lineNo = 0;
        }

        public List<Token> GetTokens()
        {
            List<Token> tokens = new List<Token>();
            charGenerator.MoveNext();

            Token token;
            do 
            {
                token = getNextToken();
                tokens.Add(token);
            } 
            while (token.Type != Token.TokenType.End);

            return tokens;
        } 

        private Token getNextToken()
        {
            char currentChar = charGenerator.GetCurrent();
            string lexeme = currentChar.ToString();
            if (currentChar == '#')
            {
                return tokenizeRouteNumber(lexeme);
            }
            else if (char.IsLetter(currentChar))
            {
                return tokenizeCity(lexeme);
            }
            else if (char.IsDigit(currentChar))
            {
                return tokenizeTime(lexeme);
            }
            else if (char.IsWhiteSpace(currentChar)) // whitespace
            {
                charGenerator.MoveNext();
                Token token = new Token(Token.TokenType.Whitespace, lexeme, lineNo);
                if (currentChar == '\n')
                {
                    lineNo++;
                }
                return token;
            }
            else if (currentChar == '\0') // end of file
            {
                return new Token(Token.TokenType.End, "", lineNo);
            }
            else
            {
                charGenerator.MoveNext();
                return new Token(Token.TokenType.Error, lexeme, lineNo);
            }
        }

        private Token tokenizeTime(string lexeme)
        {
            charGenerator.MoveNext();
            char currentChar = charGenerator.GetCurrent();
            lexeme += currentChar;
            if (char.IsDigit(currentChar))
            {
                charGenerator.MoveNext();
                currentChar = charGenerator.GetCurrent();
                lexeme += currentChar;
                if (currentChar == ':')
                {
                    charGenerator.MoveNext();
                    currentChar = charGenerator.GetCurrent();
                    lexeme += currentChar;
                    if (char.IsDigit(currentChar))
                    {
                        charGenerator.MoveNext();
                        currentChar = charGenerator.GetCurrent();
                        lexeme += currentChar;

                        if (char.IsDigit(currentChar))
                        {
                            charGenerator.MoveNext();
                            return new Token(Token.TokenType.Time, lexeme, lineNo);
                        }
                    }
                }
            }

            return new Token(Token.TokenType.Error, lexeme, lineNo);
        }

        private Token tokenizeCity(string lexeme)
        {
            bool isChar;
            do
            {
                charGenerator.MoveNext();
                char currentChar = charGenerator.GetCurrent();
                isChar = char.IsLetter(currentChar);
                if (isChar)
                {
                    lexeme += currentChar;
                }
            }
            while (isChar);

            return new Token(Token.TokenType.City, lexeme, lineNo);
        }

        private Token tokenizeRouteNumber(string lexeme)
        {
            charGenerator.MoveNext();
            char currentChar = charGenerator.GetCurrent();
            lexeme += currentChar;
            if (char.IsDigit(currentChar))
            {
                charGenerator.MoveNext();
                currentChar = charGenerator.GetCurrent();
                lexeme += currentChar;
                if (char.IsDigit(currentChar))
                {
                    charGenerator.MoveNext();
                    currentChar = charGenerator.GetCurrent();
                    if (char.IsDigit(currentChar))
                    {
                        charGenerator.MoveNext();
                        lexeme += currentChar;
                        return new Token(Token.TokenType.RouteNumber, lexeme, lineNo);
                    }
                    else
                    {
                        return new Token(Token.TokenType.RouteNumber, lexeme, lineNo);
                    }
                }
            }

            return new Token(Token.TokenType.Error, lexeme, lineNo);
        }
    }
}
