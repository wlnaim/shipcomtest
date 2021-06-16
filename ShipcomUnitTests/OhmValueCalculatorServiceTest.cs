using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shipcomtest;
using shipcomtest.Models;

namespace ShipcomUnitTests
{
   [TestClass]
   public class OhmValueCalculatorServiceTest
   {
      private OhmValueCalculatorService service;

      [TestInitialize]
      public void Setup()
      {
         var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
         var options = new DbContextOptionsBuilder<ShipcomDbContext>().UseSqlServer(config.GetConnectionString("ShipcomConnection"));
         service = new OhmValueCalculatorService(
            new ShipcomDbContext(options.Options));
      }

      [TestMethod]
      [ExpectedException(typeof( ArgumentException))]
      public void CalculateOhmValue_ValidateBandAArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("salmon", "", "", "");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CalculateOhmValue_ValidateBandBArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("black", "rose", "", "");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CalculateOhmValue_ValidateBandCArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("black", "brown", "fushia", "");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CalculateOhmValue_ValidateBandDArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("black", "brown", "orange", "pink");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CalculateOhmValue_ValidateBandASignificantArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("gold", "brown", "orange", "gold");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CalculateOhmValue_ValidateBandBSignificantArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("black", "gold", "orange", "gold");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void CalculateOhmValue_ValidateBandCMultiplierArgument_ThrowArgumentException()
      {
         service.CalculateOhmValue("black", "gold", "grey", "gold");
      }

      [TestMethod]
      public void CalculateOhmValue_ReturnOhmCalculation_Success()
      {
         var ohmResult = service.CalculateOhmValue("black", "yellow", "orange", "gold");
         Assert.IsTrue(ohmResult > 0);
      }
   }
}
