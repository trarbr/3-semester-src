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
        private List<string> errors;

        public Parser(ITokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
            regionalTimetable = new RegionalTimetable();
            errors = new List<string>();
        }

        public ParseResult Parse()
        {
            Token token;
            tokenGenerator.MoveNext();

            bool parsingSuccess = false;

            do
            {
                token = tokenGenerator.GetCurrent();
                if (token.Type == Token.TokenType.RouteNumber)
                {
                    parseTimetable();
                }
                else if (regionalTimetable.Timetables.Count > 0 && token.Type == Token.TokenType.End)
                {
                    parsingSuccess = true;
                }
                else if (token.Type == Token.TokenType.Whitespace)
                {
                    tokenGenerator.MoveNext();
                }
                else
                {
                    string error = string.Format("Error on line {0}: Expected RouteNumber token, got {1} with text {2}.",
                        token.LineNo, token.Type.ToString(), token.Lexeme);
                    errors.Add(error);
                    tokenGenerator.MoveNext();
                }
            } while (!parsingSuccess && token.Type != Token.TokenType.End);

            ParseResult result = new ParseResult(regionalTimetable, errors);

            return result; // return both AST and error list
        }

        private void parseTimetable()
        {
            // create the timetable
            Token token = tokenGenerator.GetCurrent();
            string routeNo = token.Lexeme;
            Timetable timetable = new Timetable(routeNo);

            // parse all the departures for the timetable
            tokenGenerator.MoveNext();
            bool parsingSuccess = false;
            do
            {
                token = tokenGenerator.GetCurrent();

                if (token.Type == Token.TokenType.City)
                {
                    // the success of this step depends on parseDeparture, so parseDeparture gets
                    // to call movenext
                    parseDeparture(timetable);
                }
                else if (timetable.Departures.Count > 0 
                    && (token.Type == Token.TokenType.RouteNumber || token.Type == Token.TokenType.End))
                {
                    // Don't call MoveNext, this index must be kept if we hit a RouteNumber, 
                    // so it can parse the next Timetable
                    parsingSuccess = true;
                }
                else if (token.Type == Token.TokenType.Whitespace)
                {
                    tokenGenerator.MoveNext();
                }
                else
                {
                    string error = string.Format("Error on line {0}: Expected City token, got {1} with text {2}.",
                        token.LineNo, token.Type, token.Lexeme);
                    errors.Add(error);
                    tokenGenerator.MoveNext();
                }
            } while (!parsingSuccess && token.Type != Token.TokenType.End);

            regionalTimetable.Timetables.Add(timetable);
        }

        private void parseDeparture(Timetable timetable)
        {
            Token token = tokenGenerator.GetCurrent();
            string city = token.Lexeme;

            // look for time
            tokenGenerator.MoveNext();
            bool parsingSuccess = false;

            do
            {
                token = tokenGenerator.GetCurrent();
                if (token.Type == Token.TokenType.Time)
                {
                    string time = token.Lexeme;
                    // add warning if the time is invalid
                    if (!validateTime(time))
                    {
                        string error = string.Format("Warning on line {0}: Departure time {1} is invalid",
                            token.LineNo, time);
                        errors.Add(error);
                    }

                    Departure departure = new Departure(time, city);
                    timetable.Departures.Add(departure);

                    parsingSuccess = true;
                    tokenGenerator.MoveNext();
                }
                else if (token.Type == Token.TokenType.Whitespace)
                {
                    tokenGenerator.MoveNext();
                }
                else
                {
                    string error = string.Format("Error on line {0}: Expected Time token, got {1} with text {2}.",
                        token.LineNo, token.Type, token.Lexeme);
                    errors.Add(error);
                    tokenGenerator.MoveNext();
                }
            } while (!parsingSuccess && token.Type != Token.TokenType.End);
        }

        private bool validateTime(string time)
        {
            const string validTimePattern = @"([01][0-9]|2[0-3]):[0-5][0-9]";
            return Regex.IsMatch(time, validTimePattern);
        }
    }
}
