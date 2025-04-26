using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class SaleItem : BaseEntity
{   
    /// <summary>
    /// Unique identifier for the sale item.
    /// </summary>
    public Guid SaleId { get; set; }
    
    /// <summary>
    /// Reference to the sale associated with this item.
    /// </summary>
    public Sale Sale { get; set; } = new();

    /// <summary>
    /// Unique identifier for the product.
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Title of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;
    
    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    /// <remarks>Maximum quantity is 20.</remarks>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Discount applied to the sale item.
    /// </summary>
    public decimal Discount { get; set; }
    
    /// <summary>
    /// Total price for the sale item after applying the discount.
    /// </summary>
    public decimal Total { get; set; }
    
    /// <summary>
    /// Indicates whether the sale item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    public void CalculateTotal()
    {
        CalculateDiscount();
        Total = Math.Round((UnitPrice * Quantity) - Discount, 2);
    }

    public void CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            Discount = Math.Round(UnitPrice * Quantity * 0.20m, 2);
        
        if (Quantity >= 4)
            Discount = Math.Round(UnitPrice * Quantity * 0.10m, 2);
        
        Discount = 0m;
    }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
