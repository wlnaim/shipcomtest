using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shipcomtest.Models;

namespace shipcomtest
{
   public class OhmValueCalculatorService:IOhmValueCalculator
   {
      const decimal default_tolerance = 0.2m;
      private readonly List<ColorCode> _colorCodes;
      private readonly Dictionary<string, ColorCode> _colorCodeDict;

      public OhmValueCalculatorService(ShipcomDbContext context)
      {
         _colorCodes = new List<ColorCode>();
         _colorCodeDict = new Dictionary<string, ColorCode>();

         var blackColor = new ColorCode { color = "Black"};
         blackColor.Fill(context);
         _colorCodeDict.Add(blackColor.color.ToLower(), blackColor);
         _colorCodes.Add(blackColor);
         
         var brownColor = new ColorCode { color = "Brown"};
         brownColor.Fill(context);
         _colorCodeDict.Add(brownColor.color.ToLower(), brownColor);
         _colorCodes.Add(brownColor);

         var redColor = new ColorCode { color = "Red"};
         redColor.Fill(context);
         _colorCodeDict.Add(redColor.color.ToLower(), redColor);
         _colorCodes.Add(redColor);

         var orangeColor = new ColorCode { color = "Orange"};
         orangeColor.Fill(context);
         _colorCodeDict.Add(orangeColor.color.ToLower(), orangeColor);
         _colorCodes.Add(orangeColor);

         var yelllowColor = new ColorCode { color = "Yellow" };
         yelllowColor.Fill(context);
         _colorCodeDict.Add(yelllowColor.color.ToLower(), yelllowColor);
         _colorCodes.Add(yelllowColor);

         var greenColor = new ColorCode { color = "Green" };
         greenColor.Fill(context);
         _colorCodeDict.Add(greenColor.color.ToLower(), greenColor);
         _colorCodes.Add(greenColor);

         var blueColor = new ColorCode { color = "Blue" };
         blueColor.Fill(context);
         _colorCodeDict.Add(blueColor.color.ToLower(), blueColor);
         _colorCodes.Add(blueColor);

         var violetColor = new ColorCode { color = "Violet"};
         violetColor.Fill(context);
         _colorCodeDict.Add(violetColor.color.ToLower(), violetColor);
         _colorCodes.Add(violetColor);

         var greyColor = new ColorCode { color = "Grey" };
         greyColor.Fill(context);
         _colorCodeDict.Add(greyColor.color.ToLower(), greyColor);
         _colorCodes.Add(greyColor);

         var whiteColor = new ColorCode { color = "White" };
         whiteColor.Fill(context);
         _colorCodeDict.Add(whiteColor.color.ToLower(), whiteColor);
         _colorCodes.Add(whiteColor);

         var goldColor = new ColorCode { color = "Gold"};
         goldColor.Fill(context);
         _colorCodeDict.Add(goldColor.color.ToLower(), goldColor);
         _colorCodes.Add(goldColor);

         var silverColor = new ColorCode { color = "Silver" };
         silverColor.Fill(context);
         _colorCodeDict.Add(silverColor.color.ToLower(), silverColor);
         _colorCodes.Add(silverColor);
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

         return System.Convert.ToInt32( Math.Round(abcValue * (1 + tolerate)));
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
