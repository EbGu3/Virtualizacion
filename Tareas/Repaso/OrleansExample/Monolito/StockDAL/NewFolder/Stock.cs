using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDAL.NewFolder
{
    internal class Stock
    {
        public string Id { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public bool Disponible { get; set; }
        public int Amount { get; set; }
    }
}
