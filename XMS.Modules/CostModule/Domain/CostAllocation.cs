using XMS.Domain.Abstractions;
using XMS.Domain.Models;

namespace XMS.Modules.CostModule.Domain;

public class CostAllocation : BaseEntity, ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }



    public Guid? PaymentVoucherId { get; set; }
    public PaymentVoucherType PaymentVoucherType { get; set; }

    public string? Number { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string? BusinessOperation { get; set; }
    public string? PaymentPurpose { get; set; }

    public Guid? CostCategoryId { get; set; }
    public CostCategory? CostCategory { get; set; }

    public Guid? CostItemId { get; set; }
    public CostItem? CostItem { get; set; }

    public Guid? AuthorId { get; set; }
    public UserUt? Author { get; set; }

    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public Guid? LocationId { get; set; }
    public Location? Location { get; set; }

    public Guid? CityId { get; set; }
    public City? City { get; set; }

    public string? Comment { get; set; }

    public int PaymentDetailLineNumber { get; set; }

    public bool IsAllocated => DepartmentId is not null && LocationId is not null && CityId is not null;

    public string? Name
    {
        get
        {
            return PaymentVoucherType switch
            {
                PaymentVoucherType.Bank => "Банк",
                PaymentVoucherType.Cash => "Касса",
                _ => string.Empty
            };
        }
    }
}
