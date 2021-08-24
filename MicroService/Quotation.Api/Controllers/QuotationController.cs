using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quotation.Service.Query;
using Quotation.Service.Query.DTOs;
using System;
using System.Collections.Generic;
using System.Net;


namespace Quotation.Api.Controllers
{
    [ApiController]
    [Route("Quotation")]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationQueryService _QuotationQueryService;
        private readonly ILogger<QuotationController> _logger;

        public QuotationController(ILogger<QuotationController> logger, IQuotationQueryService QuotationQueryService)
        {
            _logger = logger;
            _QuotationQueryService = QuotationQueryService;
        }

        [HttpGet("{currency}")]
        public CurrencyDto GetCurrency(string currency)
        {
            try
            {

                CurrencyDto _ValidateCurrency = _QuotationQueryService.GetCurrency(currency);

                string url = "https://www.bancoprovincia.com.ar/Principal/Dolar";
                var json = new WebClient().DownloadString(url);
                // var m = JsonConvert.DeserializeObject(json);
                //CurrencyDto cDto = new CurrencyDto();

                string[] datajson = JsonConvert.DeserializeObject<string[]>(json);
                decimal DolarVal = Convert.ToDecimal(datajson[1].ToString());

                //var _cDto = _QuotationQueryService.GetCurrency(currency);

                if (currency == "DOLAR")
                {
                    // cDto.Name = currency;
                    _ValidateCurrency.Bid = DolarVal;
                }
                else if (currency == "REAL")
                {
                    var total = DolarVal / 4;
                    _ValidateCurrency.Bid = total;
                    //cDto.HaveFormula = currency;

                }
                //else
                //{ }
                //              _QuotationQueryService.GetCurrency(currency)
                return _ValidateCurrency;

            }
            catch { return new CurrencyDto(); }

        }

        public CurrencyDto ValidateCurrency(string currencySelected)
        {
            return _QuotationQueryService.GetCurrency(currencySelected);
        }
    }
}
