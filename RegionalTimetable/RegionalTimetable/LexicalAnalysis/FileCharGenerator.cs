using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable.LexicalAnalysis
{
    class FileCharGenerator : ICharGenerator
    {
        StreamReader reader;
        char currentChar;

        public FileCharGenerator(string filename)
        {
            reader = new StreamReader(filename);
        }

        public char GetCurrent()
        {
            return currentChar;
        }

        public bool MoveNext()
        {
            if (!reader.EndOfStream)
            {
                currentChar = (char)reader.Read();
                return true;
            }
            else
            {
                currentChar = '\0';
                reader.Close();
                return false;
            }
        }
    }
}
