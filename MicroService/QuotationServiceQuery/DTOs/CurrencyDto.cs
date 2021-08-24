using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotation.Service.Query.DTOs
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public bool HaveFormula { get; set; }
        public string Formula { get; set; }
        public decimal BuyLimit { get; set; }
        public decimal Bid { get; set; }

    }
    //public class CurrencyDtoList
    //{
    //    public List<CurrencyDto> CurrencyDto { get; set; }


    //}
}
