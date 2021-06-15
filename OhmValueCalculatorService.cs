using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shipcomtest.Models;

namespace shipcomtest
{
   public class OhmValueCalculatorService : IOhmValueCalculator
   {
      const decimal default_tolerance = 0.2m;
      private readonly List<ColorCode> _colorCodes;
      private readonly Dictionary<string, ColorCode> _colorCodeDict;

      public OhmValueCalculatorService(ShipcomDbContext context)
      {
         _colorCodes = new List<ColorCode>();
         _colorCodeDict = new Dictionary<string, ColorCode>();
         var colorsData = context.ColorConfigurations.ToList();

         if (colorsData.Count == 0)
            PopulateColorsData(context);

         foreach (var colorData in colorsData)
         {
            var color = new ColorCode
            {
               color = colorData.Color,
               multiplier = colorData.Multiplier.HasValue ? colorData.Multiplier.Value : (decimal?)null,
               significantDigits = colorData.SignificantDigits.HasValue ? colorData.SignificantDigits.Value : (decimal?)null,
               tolerance = colorData.Tolerance.HasValue ? colorData.Tolerance.Value : (decimal?)null
            };
            _colorCodeDict.Add(color.color.ToLower(), color);
            _colorCodes.Add(color);
         }
      }

      private void PopulateColorsData(ShipcomDbContext context)
      {
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 1, SignificantDigits = 0, Tolerance = (decimal?)null, Color = "Black" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 10, SignificantDigits = 1, Tolerance = 0.100M, Color = "Brown" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 100, SignificantDigits = 2, Tolerance = 0.200M, Color = "Red" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 1000, SignificantDigits = 3, Tolerance = (decimal?)null, Color = "Orange" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 10000, SignificantDigits = 4, Tolerance = (decimal?)null, Color = "Yellow" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 100000, SignificantDigits = 5, Tolerance = 0.005M, Color = "Green" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 1000000, SignificantDigits = 6, Tolerance = (decimal?)null, Color = "Blue" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = 10000000, SignificantDigits = 7, Tolerance = (decimal?)null, Color = "Violet" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = (int?)null, SignificantDigits = 8, Tolerance = (decimal?)null, Color = "Grey" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = (int?)null, SignificantDigits = 9, Tolerance = (decimal?)null, Color = "White" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = (int?)null, SignificantDigits = (int?)null, Tolerance = 0.050M, Color = "Gold" });
         context.ColorConfigurations.Add(new ColorConfiguration() { Multiplier = (int?)null, SignificantDigits = (int?)null, Tolerance = 0.100M, Color = "Silver" });
         context.SaveChanges();
      }

      public int CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
      {
         if (!_colorCodeDict.ContainsKey(bandAColor)) throw new ArgumentException("invalid parameters");
         var colorACode = _colorCodeDict[bandAColor];
         if (!_colorCodeDict.ContainsKey(bandBColor)) throw new ArgumentException("invalid parameters");
         var colorBCode = _colorCodeDict[bandBColor];
         if (!_colorCodeDict.ContainsKey(bandCColor)) throw new ArgumentException("invalid parameters");
         var colorCCode = _colorCodeDict[bandCColor];
         if (!string.IsNullOrEmpty(bandDColor) && !_colorCodeDict.ContainsKey(bandDColor)) throw new ArgumentException("invalid parameters");

         if (!colorACode.isSignificant || !colorBCode.isSignificant || !colorCCode.isMultiplier) throw new ArgumentException("invalid parameters");

         decimal abValue = colorACode.significantDigits.Value * 10 + colorBCode.significantDigits.Value;
         decimal abcValue = abValue * colorCCode.multiplier.Value;
         var tolerate = GetTolerate(bandDColor);

         return System.Convert.ToInt32(Math.Round(abcValue * (1 + tolerate)));
      }

      private decimal GetTolerate(string bandDColor)
      {
         if (string.IsNullOrEmpty(bandDColor))
         {
            return default_tolerance;
         }
         else if (_colorCodeDict[bandDColor].isTolerance)
         {
            return _colorCodeDict[bandDColor].tolerance.Value;
         }
         else
         {
            throw new ArgumentException("invalid parameters");
         }
      }
   }
}
