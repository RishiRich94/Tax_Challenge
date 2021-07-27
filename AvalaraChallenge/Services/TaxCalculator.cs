using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvalaraChallenge.Services
{
    public class TaxCalculator
    {
        public decimal Money { get; set; }
        public string County_Name { get; set; }
        public TaxCalculator(decimal money, string countyname)
        {
            Money = money;
            County_Name = countyname;
        }

        public ReturnHandle GetTax()
        {
            try
            {
                using (StreamReader sr = new StreamReader("AppData/taxrates.json"))
                {
                    string json = sr.ReadToEnd();

                    dynamic array = JsonConvert.DeserializeObject(json);
                    foreach (var item in array)
                    {
                        if (item.CountyName == County_Name)
                        {
                            var taxRateString = item.TaxRate.Value.Remove(item.TaxRate.Value.Length - 1);//Removes Percent Symbol
                            var tax = Convert.ToDecimal(taxRateString) / 100; //calculates tax for item

                            ReturnHandle rh = new ReturnHandle { IsError = false, returnString = tax.ToString() };
                            return rh;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnHandle rh = new ReturnHandle { IsError = true, returnString = ex.Message };
                return rh;
            }
            ReturnHandle rhf = new ReturnHandle { IsError = true, returnString = "County Name Not Found, Please try again" };
            return rhf;
         }

        public string ErrorCheck()
        {
            if (Money <= 0)
            {
                return ("Money must be greater than 0");
            }
            if (string.IsNullOrEmpty(County_Name))
            {
                return ("County Name must not be empty");
            }
            return "";
        }
    }
}
