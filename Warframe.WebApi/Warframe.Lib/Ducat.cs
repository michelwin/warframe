using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warframe.Lib
{
    public class Ducat
    {
        public string PartName { get; set; }

        public List<string> DropLocations { get; set; }

        public int? BluePrintValue { get; set; }

        public int? CraftValue { get; set; }
    }
}
