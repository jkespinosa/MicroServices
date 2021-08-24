using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quotation.Service.Query;
using Quotation.Service.Query.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Quotation.Api.Controllers
{
    [ApiController]
    [Route("Quotation1")]
    public class CurrencyBuyController: ControllerBase
    {
        private readonly IQuotationBuyQueryService _QuotationBuyQueryService;
        private readonly IQuotationQueryService _QuotationQueryService;


        public CurrencyBuyController(IQuotationBuyQueryService QuotationBuyQueryService, IQuotationQueryService QuotationQueryService)
        {
            _QuotationBuyQueryService = QuotationBuyQueryService;
            _QuotationQueryService = QuotationQueryService;

        }

        [HttpPost]
        public CurrencyBuyDto PostCurrencyBuy(CurrencyBuyDto currencyBuyDto)
        {
            CurrencyBuyDto _ValidateCurrency = new CurrencyBuyDto();

            try
            {
                /*Validacion de valor de dolar*/
                string url = "https://www.bancoprovincia.com.ar/Principal/" + currencyBuyDto.Abbreviation;
                var json = new WebClient().DownloadString(url);

                string[] datajson = JsonConvert.DeserializeObject<string[]>(json);
                decimal CurrencyVal = Convert.ToDecimal(datajson[1].ToString());

                // Seleccionamos los datos insertados para validar suma de compra por mes
                List<CurrencyBuyDto> _ValidateCurrencySum = _QuotationBuyQueryService.CurrencyBuyGetSumByUser(currencyBuyDto.UserId, currencyBuyDto.Abbreviation);

                //Validamos la suma total del mes
                var _ValidateCurrencySumSDSD = _ValidateCurrencySum.Sum(m => m.Amount);

                var validateLimit = _QuotationQueryService.GetCurrency(currencyBuyDto.Abbreviation);

                //validacion de la compra con valor actual de la moneda
                var Ammount = currencyBuyDto.Amount / CurrencyVal;

                if (_ValidateCurrencySumSDSD <= validateLimit.BuyLimit)
                {

                    //Modificamos nuevos valores
                    currencyBuyDto.Amount = Ammount;
                    currencyBuyDto.Bid = CurrencyVal;


                    _ValidateCurrency = _QuotationBuyQueryService.CurrencyBuyPost(currencyBuyDto);
                }
                else
                {
                    _ValidateCurrency.Message = "Se excede de valor permitido por mes";
                    
                }
                

                return _ValidateCurrency;
            }
            catch
            {
                return new CurrencyBuyDto();
            }
        }


    }

}
