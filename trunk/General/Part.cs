using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public class Part
    {
        public string Name { get; set; }
        public string CatalogueNumber { get; set; }
        public List<Distributor> AvailableFrom { get; set; }
    }
}
