using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Helpers
{
    public static class CharNumericVerification
    {
        public static bool IsNumeric(this char inputChar)
        {
            switch (inputChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                case ',':
                    return true;
                default:
                    return false;
            }
        }
    }
}
