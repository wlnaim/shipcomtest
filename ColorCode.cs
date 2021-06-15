using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shipcomtest.Models;

namespace shipcomtest
{
   public class ColorCode
   {

      public string color { get; set; }
      public decimal? significantDigits { get; set; }
      public decimal? multiplier { get; set; }
      public decimal? tolerance { get; set; }
      public bool isSignificant
      {
         get { return significantDigits.HasValue; }
      }
      public bool isMultiplier
      {
         get { return multiplier.HasValue; }
      }
      public bool isTolerance
      {
         get { return tolerance.HasValue; }
      }

      public void Fill(ShipcomDbContext context)
      {
         if (context == null)
            throw new ArgumentNullException(nameof(context));

         var colorEntity = context.ColorConfigurations.Where(w => w.Color.ToLower() == this.color.ToLower())
            .FirstOrDefault(f => f.Id > 0);

         if (colorEntity == null)
            throw new ArgumentException("Invalid color code");

         if (colorEntity.Multiplier.HasValue)
            this.multiplier = colorEntity.Multiplier;

         if (colorEntity.SignificantDigits.HasValue)
            this.significantDigits = colorEntity.SignificantDigits;

         if (colorEntity.Tolerance.HasValue)
            this.tolerance = colorEntity.Tolerance;
      }
   }
}
