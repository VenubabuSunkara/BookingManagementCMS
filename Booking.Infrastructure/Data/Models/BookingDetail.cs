using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Models;

public partial class BookingDetail
{
    [Key]
    public int Id { get; set; }

    [Column("BookingID")]
    public int BookingId { get; set; }

    [Column("RelativeID")]
    public int? RelativeId { get; set; }

    [StringLength(100)]
    public string? PassengerName { get; set; }

    public int? PassengerAge { get; set; }

    [StringLength(10)]
    public string? PassengerGender { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("BookingDetails")]
    public virtual BookingOrder Booking { get; set; } = null!;

    [ForeignKey("RelativeId")]
    [InverseProperty("BookingDetails")]
    public virtual CustomerRelative? Relative { get; set; }
}
