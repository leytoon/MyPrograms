using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CookingBook.Utilities;
namespace CookBookChoosenTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TextValidation()
        {
            List<string> integers = new List<string>() {"1", "2", "3", "99", "199", "1992" };

            List<string> price = new List<string>() 
            {"0","1", "2", "3", "99", "199", 
             "1.1", "2.11", "3.11", 
             "99.1", "99.11", "99.11",
             "199.6","199.76","199.76","1992",
             "1,1", "2,11", "3,11", 
             "99,1", "99,11", "99,11",
             "199,6","199,76","199,76","1992" };

            List<string> amount = new List<string>() 
            {"1", "2", "3", "99", "199", 
             "1.1", "2.11", "3.111", 
             "99.1", "99.11", "99.111",
             "199.6","199.76","199.766","1992",
             "1,1", "2,11", "3,111", 
             "99,1", "99,11", "99,111",
             "199,6","199,76","199,766" };

            foreach (var x in integers)
                Assert.IsTrue(TxtValidator.IsPersonsValid(x));

            foreach (var x in price)
                Assert.IsTrue(TxtValidator.IsPriceValid(x.Replace(".",",")));
            
            foreach (var x in amount)
                Assert.IsTrue(TxtValidator.IsAmountValid(x.Replace(".", ",")));


        }
    }
}
