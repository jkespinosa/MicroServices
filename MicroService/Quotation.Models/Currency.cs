using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotation.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public bool HaveFormula { get; set; }
        public string Formula { get; set; }
        public decimal BuyLimit { get; set; }

    }
}
