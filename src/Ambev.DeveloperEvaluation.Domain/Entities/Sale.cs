using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{   
    /// <summary>
    /// Identifier for the sale.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }
    
    /// <summary>
    /// Unique identifier for the customer.
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Name of the customer.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Total amount of the sale after applying discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }        
    
    /// <summary>
    /// Unique identifier for the branch.
    /// </summary>
    public Guid BranchId { get; set; }
    
    /// <summary>
    /// Name of the branch.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
    
    /// <summary>
    /// List of items included in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    public void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(item => item.Total);
    }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}