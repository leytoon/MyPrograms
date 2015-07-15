using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace CookingBook.Utilities
{
    static public class TxtValidator
    {
        static public bool IsPriceValid(string txt)
        {
            Regex price = new Regex("^[0-9]{0,4},{0,1}[0-9]{1,2}$");
            
             return price.IsMatch(txt);
        }

        static public bool IsAmountValid(string txt) 
        {
            Regex amount = new Regex("^[0-9]{0,4},{0,1}[0-9]{1,3}$");

            return amount.IsMatch(txt);
        }
        static public bool IsPersonsValid(string txt)
        {
            Regex amount = new Regex("[1-9]{1,3}");

            return amount.IsMatch(txt);
        }
        static public bool IsIntegerValid(string txt)
        {
            Regex amount = new Regex("[0-9]{1,3}");

            return amount.IsMatch(txt);
        }

    }
}
