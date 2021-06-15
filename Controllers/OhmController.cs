using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shipcomtest.Controllers
{
   [Route("api/[controller]")]
   public class OhmController : Controller
   {
      private readonly ILogger<OhmController> logger;
      private readonly IOhmValueCalculator ohmCalculatorService;
      public OhmController(IOhmValueCalculator service, ILogger<OhmController> logger)
      {
         this.ohmCalculatorService = service;
         this.logger = logger;
      }
      /// <summary>
      /// Calculates the Ohm value of a resistor based on the band colors.
      /// </summary>
      /// <param name="bandAColor">The color of the first figure of component value band.</param>
      /// <param name="bandBColor">The color of the second significant figure band.</param>
      /// <param name="bandCColor">The color of the decimal multiplier band.</param>
      /// <param name="bandDColor">The color of the tolerance value band.</param>
      [HttpGet]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(500)]
      public ActionResult GetOhmResult(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
      {
         try
         {
            if (string.IsNullOrWhiteSpace(bandAColor))
               throw new ArgumentException("BandAColor parameter is invalid.");

            if (string.IsNullOrWhiteSpace(bandBColor))
               throw new ArgumentException("BandBColor parameter is invalid.");

            if (string.IsNullOrWhiteSpace(bandCColor))
               throw new ArgumentException("BandCColor parameter is invalid.");

            if (string.IsNullOrWhiteSpace(bandDColor))
               throw new ArgumentException("BandDColor parameter is invalid.");

            return new JsonResult(new { Result = ohmCalculatorService.CalculateOhmValue(bandAColor.ToLower(), bandBColor.ToLower(), bandCColor.ToLower(), bandDColor.ToLower()) });
         }
         catch (ArgumentException ex)
         {
            this.logger.LogError(ex.Message, ex.StackTrace);
            return BadRequest(ex);
         }
         catch (Exception ex)
         {
            this.logger.LogError(ex.Message, ex.StackTrace);
            return StatusCode(500, ex);
         }
      }

   }
}
