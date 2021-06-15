using System;
using System.Collections.Generic;

#nullable disable

namespace shipcomtest.Models
{
    public partial class ColorConfiguration
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int? SignificantDigits { get; set; }
        public int? Multiplier { get; set; }
        public decimal? Tolerance { get; set; }
    }
}
