using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class CustomerAddress
{
    [Key]
    public int Id { get; set; }

    public int CustomersId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? AddressLine1 { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? AddressLine2 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CityName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? StateName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CountryName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? LandMark { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContactNo { get; set; }

    public int AddressTypeId { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public bool? IsDefault { get; set; }

    [ForeignKey("AddressTypeId")]
    [InverseProperty("CustomerAddresses")]
    public virtual AddressType AddressType { get; set; } = null!;

    [ForeignKey("CustomersId")]
    [InverseProperty("CustomerAddresses")]
    public virtual Customer Customers { get; set; } = null!;
}
