using RegionalTimetableApp.LexicalAnalysis;
using RegionalTimetableApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            do
            {
                token = tokenGenerator.GetCurrent();
                // if hell
                if (token.Type == Token.TokenType.RouteNumber)
                {
                    parseRoute();
                }
                else if (regionalTimetable.Timetables.Count > 0 && token.Type == Token.TokenType.End)
                {
                    // legal exit
                    break;
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
            } while (token.Type != Token.TokenType.End);

            ParseResult result = new ParseResult(regionalTimetable, errors);

            return result; // return both AST and error list
        }

        private void parseRoute()
        {
            // parse the route
            Token token = tokenGenerator.GetCurrent();
            string routeNo = token.Lexeme;
            Timetable timetable = new Timetable(routeNo);
            tokenGenerator.MoveNext();

            // parse all the departures for the route
            do
            {
                // must have at least one departure
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
                    // legal exit
                    // don't move next, this index must be kept if there are more routes
                    break;
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
            } while (token.Type != Token.TokenType.End);

            regionalTimetable.Timetables.Add(timetable);
        }

        private void parseDeparture(Timetable timetable)
        {
            Token token = tokenGenerator.GetCurrent();
            string city = token.Lexeme;

            // look for time
            tokenGenerator.MoveNext();

            do
            {
                token = tokenGenerator.GetCurrent();
                if (token.Type == Token.TokenType.Time)
                {
                    string time = token.Lexeme;

                    Departure departure = new Departure(time, city);

                    timetable.Departures.Add(departure);

                    tokenGenerator.MoveNext();
                    break; // parsing success
                }
                else if (token.Type == Token.TokenType.Whitespace)
                {
                    //tokenGenerator.MoveNext();
                    tokenGenerator.MoveNext();
                }
                else
                {
                    string error = string.Format("Error on line {0}: Expected Time token, got {1} with text {2}.",
                        token.LineNo, token.Type, token.Lexeme);
                    errors.Add(error);
                    tokenGenerator.MoveNext();
                }
            } while (token.Type != Token.TokenType.End);
        }
    }
}
