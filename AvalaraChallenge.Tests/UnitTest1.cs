using AvalaraChallenge.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AvalaraChallenge.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            TaxCalculator tc = new TaxCalculator(money:10.0M, countyname:"Forsyth County");
            var errorString = tc.ErrorCheck();
            var rh = tc.GetTax();

            Assert.True(errorString == "" && rh.IsError == false && rh.returnString == "0.07");

        }
        [Fact]
        public void Test2()
        {
            TaxCalculator tc = new TaxCalculator(money: 10.0M, countyname: "Forsyth");
            var errorString = tc.ErrorCheck();
            var rh = tc.GetTax();

            Assert.True(errorString == "" && rh.IsError == true && rh.returnString == "County Name Not Found, Please try again");
        }
        [Fact]
        public void Test3()
        {
            TaxCalculator tc = new TaxCalculator(money: 0.0M, countyname: "Forsyth County");
            var errorString = tc.ErrorCheck();
              

            Assert.True(errorString == "Money must be greater than 0");

        }

        [Fact]
        public void Test4()
        {
            TaxCalculator tc = new TaxCalculator(money: 10.0M, countyname: "");
            var errorString = tc.ErrorCheck();


            Assert.True(errorString == "County Name must not be empty");
        }

    }
}
