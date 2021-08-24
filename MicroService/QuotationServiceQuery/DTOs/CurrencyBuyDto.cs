using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotation.Service.Query.DTOs
{
    public class CurrencyBuyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Abbreviation { get; set; }
        public decimal Amount { get; set; }
        public decimal Bid { get; set; }
        public DateTime? DateTime { get; set; }
        public string Message { get; set; }


    }


}
