using Quotation.Persistence.DataBase;
using Quotation.Service.Query.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quotation.Models;
using AutoMapper;

namespace Quotation.Service.Query
{
    public interface IQuotationBuyQueryService
    {
        CurrencyBuyDto CurrencyBuyPost(CurrencyBuyDto currencyBuyDto);

        List<CurrencyBuyDto> CurrencyBuyGetSumByUser(int UserId, string currency);
    }
    public class QuotationBuyQueryService : IQuotationBuyQueryService
    {
        private readonly ApplicationDbContext _context;

        public QuotationBuyQueryService(ApplicationDbContext context)
        {
            _context = context;

        }

        public CurrencyBuyDto CurrencyBuyPost(CurrencyBuyDto currencyBuy)
        {
            try
            {
                var CurrencyBuy = new CurrencyBuy();
                CurrencyBuy.UserId = currencyBuy.UserId;
                CurrencyBuy.Abbreviation = currencyBuy.Abbreviation;
                CurrencyBuy.Amount = currencyBuy.Amount;
                CurrencyBuy.Bid = currencyBuy.Bid;
                CurrencyBuy.DateTime = DateTime.Now;


                _context.CurrencyBuy.Add(CurrencyBuy);
                _context.SaveChanges();

                int id = CurrencyBuy.Id;

                return currencyBuy;

            }
            catch (Exception e)
            {
                return new CurrencyBuyDto();
            }

        }

        public List<CurrencyBuyDto> CurrencyBuyGetSumByUser(int UserId, string currency)
        {
            try
            {
               var Currency = _context.CurrencyBuy.Where(m => m.UserId == UserId && m.Abbreviation == currency && m.DateTime.Month == DateTime.Now.Month);

                List<CurrencyBuyDto> cbListDto = new List<CurrencyBuyDto>();

     

                foreach (var CurrencyData in Currency)
                {
                    //Creo el usuario que vamos a añadir en ret
                    CurrencyBuyDto cbDto = new CurrencyBuyDto();

                    ////Meto las propiedades del dto recorrido en el usuario
                    cbDto.Abbreviation = CurrencyData.Abbreviation;
                    cbDto.Amount = CurrencyData.Amount;
                    cbDto.Bid = CurrencyData.Bid;
                    cbDto.DateTime = CurrencyData.DateTime;
                    cbDto.Id = CurrencyData.Id;
                    cbDto.UserId = CurrencyData.UserId;
                    cbDto.DateTime = DateTime.Now;

                    //Añado el usuario
                    cbListDto.Add(cbDto);
                }


                return cbListDto.ToList();

            }
            catch (Exception e)
            {
                return new List<CurrencyBuyDto>();
            }

        }
    }
}
