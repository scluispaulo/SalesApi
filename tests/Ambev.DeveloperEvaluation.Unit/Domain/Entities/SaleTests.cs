using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Given_ValidSaleItemData_When_Validated_Then_ShouldReturnValid()
        {
            var saleItem = SaleTestData.SaleItemFaker.Generate(1);

            Assert.True(saleItem[0].Validate().IsValid);
        }

        [Fact]
        public void Given_TwoSaleItens_When_Sum_Then_ReturnCorrectTotalAmount()
        {
            var sale = SaleTestData.SaleFaker.Generate(1);
            var saleItems = SaleTestData.SaleItemFaker.Generate(2);
            sale[0].Items.AddRange(saleItems);

            saleItems.ForEach(item => item.CalculateTotal());
            var expectedTotal = saleItems.Sum(item => item.Total);
            sale[0].CalculateTotalAmount();

            Assert.Equal(expectedTotal, sale[0].TotalAmount);
        }

        [Fact]
        public void Given_SaleItensOverflow_When_Validate_Then_ShouldReturnError()
        {
            var saleItem = SaleTestData.SaleItemFaker.Generate(1);
            saleItem[0].Quantity = 21;

            var result = saleItem[0].Validate();

            Assert.False(result.IsValid);
            Assert.Equal("Cannot sell more than 20 items of the same product.", result.Errors.First().Detail);
        }
    }
}