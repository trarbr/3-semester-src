using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable.LexicalAnalysis
{
    class DummyCharGenerator : ICharGenerator
    {
        private string allText;
        private int index;

        public DummyCharGenerator()
        {
            allText = "#44 odense 14:00\n" + '\n';
            index = -1;
        }

        public char GetCurrent()
        {
            if (index >= allText.Length)
            {
                return '\0';
            }
            else
            {
                return allText[index];
            }
        }

        public bool MoveNext()
        {
            index++;
            return (index > allText.Length);
        }
    }
}
