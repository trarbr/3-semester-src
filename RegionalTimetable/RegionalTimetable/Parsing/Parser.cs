using RegionalTimetableApp.LexicalAnalysis;
using RegionalTimetableApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Parsing
{
    class Parser
    {
        /*
         * Error handling strategy:
         * If the correct token is found, try to create a Model object that reflects it - no matter which errors follow
        */
        private ITokenGenerator tokenGenerator;
        private RegionalTimetable regionalTimetable;
        private ParseResult parseResult;
        private List<string> errors;
        const string ERROR_FORMAT = "Error on line {0}: Expected {1} token, got {2} with text {3}.";

        public Parser(ITokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
            regionalTimetable = new RegionalTimetable();
            errors = new List<string>();
            parseResult = new ParseResult(regionalTimetable, errors);
        }

        public ParseResult Parse()
        {
            tokenGenerator.MoveNext();
            Token token = tokenGenerator.GetCurrent();
            if (token.Type == Token.TokenType.RouteNumber)
            {
                parseTimetable();
            }
            else if (token.Type == Token.TokenType.End)
            {
                string error = string.Format("Unexpected end error at line {0}", token.LineNo);
                errors.Add(error);
            }
            else
            {
                if (token.Type != Token.TokenType.Whitespace)
                {
                    string error = string.Format(ERROR_FORMAT, token.LineNo, Token.TokenType.RouteNumber, token.Type, token.Lexeme);
                    errors.Add(error);
                }
                Parse();
            }

            return parseResult;
        }

        private void parseTimetable()
        {
            Token token = tokenGenerator.GetCurrent();

            if (token.Type == Token.TokenType.RouteNumber)
            {
                string routeNumber = token.Lexeme;

                Timetable timetable = new Timetable(routeNumber);
                regionalTimetable.Timetables.Add(timetable);

                parseCity(timetable);
            }
            else if (token.Type == Token.TokenType.End)
            {
                if (regionalTimetable.Timetables.Count < 1)
                {
                    string error = string.Format("Unexpected end error at line {0}", token.LineNo);
                    errors.Add(error);
                }

                return;
            }
            else
            {
                if (token.Type != Token.TokenType.Whitespace)
                {
                    string error = string.Format(ERROR_FORMAT, token.LineNo, 
                        Token.TokenType.RouteNumber, token.Type, token.Lexeme);
                    errors.Add(error);
                }
                tokenGenerator.MoveNext();
                parseTimetable();
            }
        }

        private void parseCity(Timetable timetable)
        {
            tokenGenerator.MoveNext();
            Token token = tokenGenerator.GetCurrent();

            if (token.Type == Token.TokenType.City)
            {
                string city = token.Lexeme;

                parseTime(timetable, city);
            }
            else if (token.Type == Token.TokenType.RouteNumber && timetable.Departures.Count > 1)
            {
                parseTimetable();
            }
            else if (token.Type == Token.TokenType.End)
            {
                if (timetable.Departures.Count < 1)
                {
                    string error = string.Format("Unexpected end error at line {0}", token.LineNo);
                    errors.Add(error);
                }

                return;
            }
            else
            {
                if (token.Type != Token.TokenType.Whitespace)
                {
                    string error = string.Format(ERROR_FORMAT, token.LineNo, 
                        Token.TokenType.City, token.Type, token.Lexeme);
                    errors.Add(error);
                }
                parseCity(timetable);
            }
        }

        private void parseTime(Timetable timetable, string city)
        {
            tokenGenerator.MoveNext();
            Token token = tokenGenerator.GetCurrent();

            if (token.Type == Token.TokenType.Time)
            {
                string time = token.Lexeme;
                if (!validateTime(time))
                {
                    errors.Add(string.Format("Warning! Time at line {0} is bad!", token.LineNo));
                }

                Departure departure = new Departure(time, city);
                timetable.Departures.Add(departure);

                parseCity(timetable);
            }
            else if (token.Type == Token.TokenType.End)
            {
                string error = string.Format("Unexpected end error at line {0}", token.LineNo);
                errors.Add(error);

                return;
            }
            else
            {
                if (token.Type != Token.TokenType.Whitespace)
                {
                    string error = string.Format(ERROR_FORMAT, token.LineNo, 
                        Token.TokenType.Time, token.Type, token.Lexeme);
                    errors.Add(error);
                }
                parseTime(timetable, city);
            }
        }

        private bool validateTime(string time)
        {
            const string validTimePattern = @"([01][0-9]|2[0-3]):[0-5][0-9]";
            return Regex.IsMatch(time, validTimePattern);
        }
    }
}
