﻿using System.Collections.ObjectModel;
using ProductManager.Enity;
using Xunit;

namespace ProductManager.Entity.Tests
{
    public class PricePlanTests
    {
        [Fact]
        public void ShouldReturnNullIfThereAreNoPrices()
        {
            var plan = new PricePlan();
            var price = plan.GetPrice(200, 300);
            Assert.Same(null, price);
        }

        [Fact]
        public void ShouldReturnCorrectPriceWhenThereIsOnPriceUnitLowerLimit()
        {
            var plan = new PricePlan();
            plan.PriceUnits = new Collection<PriceUnit>();
            plan.PriceUnits.Add(new PriceUnit { Height = 200, Width = 350, Price = 5000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 210, Width = 360, Price = 7000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 220, Width = 370, Price = 8000 });
            var price = plan.GetPrice(200, 350);
            Assert.Equal(5000, price);
        }

        [Fact]
        public void ShouldReturnCorrectPriceWhenThereIsOnPriceUnit()
        {
            var plan = new PricePlan();
            plan.PriceUnits = new Collection<PriceUnit>();
            plan.PriceUnits.Add(new PriceUnit { Height = 200, Width = 350, Price = 5000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 210, Width = 360, Price = 7000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 220, Width = 370, Price = 8000 });
            var price = plan.GetPrice(201, 351);
            Assert.Equal(5000, price);
        }

        [Fact]
        public void ShouldReturnCorrectPriceWhenThereIsOnPriceUnitUpperLimit()
        {
            var plan = new PricePlan();
            plan.PriceUnits = new Collection<PriceUnit>();
            plan.PriceUnits.Add(new PriceUnit { Height = 200, Width = 350, Price = 5000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 210, Width = 360, Price = 7000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 220, Width = 370, Price = 8000 });
            var price = plan.GetPrice(210, 360);
            Assert.Equal(7000, price);
        }

        [Fact]
        public void ShouldReturnCorrectPriceWhenThereAreTwoPriceUnits()
        {
            var plan = new PricePlan();
            plan.PriceUnits = new Collection<PriceUnit>();
            plan.PriceUnits.Add(new PriceUnit { Height = 200, Width = 350, Price = 5000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 210, Width = 360, Price = 7000 });
            plan.PriceUnits.Add(new PriceUnit { Height = 220, Width = 370, Price = 7000 });
            var price = plan.GetPrice(213, 365);
            Assert.Equal(7000, price);
        }
    }
}