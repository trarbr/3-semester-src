using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    class Lexer
    {
        private ITokenizer tokenizer;

        public Lexer(ITokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }


        public List<Token> GetTokens()
        {
            List<Token> tokens = new List<Token>();
            tokenizer.MoveNext();

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
            string lexeme = "";
            char currentChar = tokenizer.GetCurrent();
            lexeme = currentChar.ToString();
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
                tokenizer.MoveNext();
                return new Token(Token.TokenType.Whitespace, lexeme);
            }
            else if (currentChar == '\0') // end of file
            {
                return new Token(Token.TokenType.End, "");
            }
            else
            {
                tokenizer.MoveNext();
                return new Token(Token.TokenType.Error, lexeme);
            }
        }

        private Token tokenizeTime(string lexeme)
        {
            tokenizer.MoveNext();
            char currentChar = tokenizer.GetCurrent();
            lexeme += currentChar;
            if (char.IsDigit(currentChar))
            {
                tokenizer.MoveNext();
                currentChar = tokenizer.GetCurrent();
                lexeme += currentChar;
                if (currentChar == ':')
                {
                    tokenizer.MoveNext();
                    currentChar = tokenizer.GetCurrent();
                    lexeme += currentChar;
                    if (char.IsDigit(currentChar))
                    {
                        tokenizer.MoveNext();
                        currentChar = tokenizer.GetCurrent();
                        lexeme += currentChar;

                        if (char.IsDigit(currentChar))
                        {
                            tokenizer.MoveNext();
                            return new Token(Token.TokenType.Time, lexeme);
                        }
                    }
                }
            }

            return new Token(Token.TokenType.Error, lexeme);
        }

        private Token tokenizeCity(string lexeme)
        {
            bool isChar;
            do
            {
                tokenizer.MoveNext();
                char currentChar = tokenizer.GetCurrent();
                isChar = char.IsLetter(currentChar);
                if (isChar)
                {
                    lexeme += currentChar;
                }
            }
            while (isChar);

            return new Token(Token.TokenType.City, lexeme);
        }

        private Token tokenizeRouteNumber(string lexeme)
        {
            tokenizer.MoveNext();
            char currentChar = tokenizer.GetCurrent();
            lexeme += currentChar;
            if (char.IsDigit(currentChar))
            {
                tokenizer.MoveNext();
                currentChar = tokenizer.GetCurrent();
                lexeme += currentChar;
                if (char.IsDigit(currentChar))
                {
                    tokenizer.MoveNext();
                    currentChar = tokenizer.GetCurrent();
                    if (char.IsDigit(currentChar))
                    {
                        tokenizer.MoveNext();
                        lexeme += currentChar;
                        return new Token(Token.TokenType.RouteNumber, lexeme);
                    }
                    else
                    {
                        return new Token(Token.TokenType.RouteNumber, lexeme);
                    }
                }
            }

            return new Token(Token.TokenType.Error, lexeme);
        }
    }
}
