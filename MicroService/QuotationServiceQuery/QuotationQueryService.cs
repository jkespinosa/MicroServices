using Quotation.Persistence.DataBase;
using Quotation.Service.Query.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Quotation.Models;
using AutoMapper.QueryableExtensions;
using Service.Common.Mapping;
using System;

namespace Quotation.Service.Query
{
    public interface IQuotationQueryService
    {
        //List<CurrencyDto> GetCurrency(string currencySelected);
        CurrencyDto GetCurrency(string currencySelected);
        //string Validate();

    }
    public class QuotationQueryService : IQuotationQueryService
    {
        private readonly ApplicationDbContext _context;

        public QuotationQueryService(ApplicationDbContext context)
        {
            _context = context;
        }  

 
        public CurrencyDto GetCurrency(string currencySelected)
        {
            try {

                var Currency = _context.Currency.Where(m => m.Abbreviation == currencySelected.ToUpper()).FirstOrDefault();

                //if (Currency.Count() > 0)
                //{
                    var CurrencyList = Currency.MapTo<CurrencyDto>();

                    return CurrencyList;
                //}

            }
            catch(Exception e) {
                return new CurrencyDto();
            }

            //return new CurrencyDto();

        }

        public static string Validate()
        {
            return "Validando Unit Test";
        }

        //public CurrencyDto ValidateCurrency(string currencySelected)
        //{
        //    var Currency = _context.Currency.FirstOrDefault();

        //    var CurrencyList = Currency.MapTo<CurrencyDto>();

        //    return CurrencyList;
        //}

      
    }
}
