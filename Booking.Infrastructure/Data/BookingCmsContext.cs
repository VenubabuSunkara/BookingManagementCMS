using System;
using System.Collections.Generic;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data;

public partial class BookingCmsContext : DbContext
{
    public BookingCmsContext()
    {
    }

    public BookingCmsContext(DbContextOptions<BookingCmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<BookingOrder> BookingOrders { get; set; }

    public virtual DbSet<CompanyUser> CompanyUsers { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CouponCode> CouponCodes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<CustomerRelative> CustomerRelatives { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverMediaMapping> DriverMediaMappings { get; set; }

    public virtual DbSet<DriverVehicle> DriverVehicles { get; set; }

    public virtual DbSet<DriverVehicleAvailability> DriverVehicleAvailabilities { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<PageContent> PageContents { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<RoleMenuPermission> RoleMenuPermissions { get; set; }

    public virtual DbSet<SeasonalPricing> SeasonalPricings { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

    public virtual DbSet<TourActivate> TourActivates { get; set; }

    public virtual DbSet<TourFeatureItemAttribute> TourFeatureItemAttributes { get; set; }

    public virtual DbSet<TourFeatureItemType> TourFeatureItemTypes { get; set; }

    public virtual DbSet<TourGuide> TourGuides { get; set; }

    public virtual DbSet<TourGuideAssignment> TourGuideAssignments { get; set; }

    public virtual DbSet<TourLocation> TourLocations { get; set; }

    public virtual DbSet<TourPackage> TourPackages { get; set; }

    public virtual DbSet<TourPackageCategory> TourPackageCategories { get; set; }

    public virtual DbSet<TourPackageItemAttributeValue> TourPackageItemAttributeValues { get; set; }

    public virtual DbSet<TourPackageItineraryDay> TourPackageItineraryDays { get; set; }

    public virtual DbSet<TourPackageMedium> TourPackageMedia { get; set; }

    public virtual DbSet<TourPackagePolicy> TourPackagePolicies { get; set; }

    public virtual DbSet<TourReview> TourReviews { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleFeature> VehicleFeatures { get; set; }

    public virtual DbSet<VehicleFeatureMapping> VehicleFeatureMappings { get; set; }

    public virtual DbSet<VehicleMediaMapping> VehicleMediaMappings { get; set; }

    public virtual DbSet<VehicleTypeMaster> VehicleTypeMasters { get; set; }

    public virtual DbSet<ViewAllBooking> ViewAllBookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AddressT__3214EC07801B9CC9");

            entity.ToTable("AddressType");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TypeName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__BookingD__8136D45AE0B2AABF");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExtraCharges)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Notes).HasMaxLength(250);

            entity.HasOne(d => d.BookingOrder).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingOrderId)
                .HasConstraintName("FK_BookingDetails_BookingOrders");
        });

        modelBuilder.Entity<BookingOrder>(entity =>
        {
            entity.HasKey(e => e.BookingOrderId).HasName("PK__BookingO__72C389194C67E26B");

            entity.HasIndex(e => e.BookingNumber, "UQ__BookingO__AAC320BF254E2A9C").IsUnique();

            entity.Property(e => e.ActualFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BookingNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DistanceInKm).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DropLocation).HasMaxLength(250);
            entity.Property(e => e.EstimatedFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExtraCharges).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Unpaid");
            entity.Property(e => e.PickupLocation).HasMaxLength(250);
            entity.Property(e => e.ScheduledDropTime).HasColumnType("datetime");
            entity.Property(e => e.ScheduledPickupTime).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TripType).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingOrders_Customers");

            entity.HasOne(d => d.Driver).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingOrders_Drivers");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingOrders_Vehicles");
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyU__3214EC07BA2349D2");

            entity.ToTable("CompanyUser");

            entity.HasIndex(e => e.UserId, "UQ_CompanyUser_AspNetUser").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithOne(p => p.CompanyUser)
                .HasForeignKey<CompanyUser>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUser_ASPNetUser");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.KeyName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.KeyValue)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ThreeLetterIsoCode).HasMaxLength(3);
            entity.Property(e => e.TwoLetterIsoCode).HasMaxLength(2);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CouponCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CouponCo__3214EC07B743B02F");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasDefaultValue("System");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DiscountType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MaximumDiscount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MinimumAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasDefaultValue("System");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsageCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC079D13EBE7");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Customer__85FB4E386A4DD130").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D105341E1C11EF").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLocked).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ReferralBonusGranted).HasDefaultValue(false);
            entity.Property(e => e.ReferralCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC071D03A197");

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CityName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CountryName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.LandMark)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StateName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AddressType).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerA__Addre__54EB90A0");
        });

        modelBuilder.Entity<CustomerRelative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07648BF12D");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRelatives)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerR__Custo__4885B9BB");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD24C12CC5B1");

            entity.ToTable("Driver");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ApproveDriver).HasDefaultValue(false);
            entity.Property(e => e.AvailabilityStatus).HasDefaultValue(true);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("isActive");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DriverMediaMapping>(entity =>
        {
            entity.ToTable("DriverMediaMapping");

            entity.Property(e => e.DriverMediaMappingId).HasColumnName("DriverMediaMappingID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverMediaMappings)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverMed__Drive__7FD5EEA5");

            entity.HasOne(d => d.Media).WithMany(p => p.DriverMediaMappings)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverMed__Media__00CA12DE");
        });

        modelBuilder.Entity<DriverVehicle>(entity =>
        {
            entity.HasKey(e => new { e.DriverId, e.VehicleId }).HasName("PK__DriverVe__C5C7786FC489CCAD");

            entity.ToTable("DriverVehicle");

            entity.HasIndex(e => e.VehicleId, "UQ__DriverVe__476B54B317EAFC0F").IsUnique();

            entity.HasIndex(e => e.DriverId, "UQ__DriverVe__F1B1CD25CE261544").IsUnique();

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithOne(p => p.DriverVehicle)
                .HasForeignKey<DriverVehicle>(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverVeh__Drive__55AAAAAF");

            entity.HasOne(d => d.Vehicle).WithOne(p => p.DriverVehicle)
                .HasForeignKey<DriverVehicle>(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverVeh__Vehic__569ECEE8");
        });

        modelBuilder.Entity<DriverVehicleAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__DriverVe__DA3979911F3D28DE");

            entity.ToTable("DriverVehicleAvailability");

            entity.HasIndex(e => new { e.VehicleId, e.DriverId, e.AvailableFrom, e.AvailableTo, e.SlotStart, e.SlotEnd }, "UQ_Availability").IsUnique();

            entity.Property(e => e.AvailabilityId).HasColumnName("AvailabilityID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverVehicleAvailabilities)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverVeh__Drive__5F3414E9");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.DriverVehicleAvailabilities)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverVeh__Vehic__60283922");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailSubject).HasMaxLength(400);
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SenderEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.MediaId);

            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.MediaName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MediaType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menus__C99ED230FF4D94F9");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.MenuUrl).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("FK__Menus__ParentMen__0F183235");
        });

        modelBuilder.Entity<PageContent>(entity =>
        {
            entity.ToTable("PageContent");

            entity.Property(e => e.CreateBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PageContent1).HasColumnName("PageContent");
            entity.Property(e => e.PageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Placeholder)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38665ABB3A");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMode).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Unpaid");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.BookingOrder).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_BookingOrders");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2FED2CC7A9");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PermissionName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Rating).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewCom__Drive__7152C524");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("PK__RoleMenu__F86287B69D103299");

            entity.HasIndex(e => new { e.RoleId, e.MenuId }, "UQ__RoleMenu__966323384C6E6140").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenus__MenuI__15C52FC4");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenus__RoleI__16B953FD");
        });

        modelBuilder.Entity<RoleMenuPermission>(entity =>
        {
            entity.HasKey(e => e.RoleMenuPermissionId).HasName("PK__RoleMenu__090D1B7B7C8DF978");

            entity.HasIndex(e => new { e.RoleId, e.MenuId, e.PermissionId }, "UQ__RoleMenu__B88C85C32454DA80").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenuPermissions)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenuP__MenuI__1D66518C");

            entity.HasOne(d => d.Permission).WithMany(p => p.RoleMenuPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenuP__Permi__25FB978D");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenuPermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenuP__RoleI__1F4E99FE");
        });

        modelBuilder.Entity<SeasonalPricing>(entity =>
        {
            entity.HasKey(e => e.PricingId).HasName("PK__Seasonal__EC306B729F382161");

            entity.ToTable("SeasonalPricing");

            entity.Property(e => e.PricingId).HasColumnName("PricingID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.Multiplier).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.SeasonalPricings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SeasonalP__Vehic__65E11278");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK__Site__B9DCB9039A19CCB6");

            entity.ToTable("Site");

            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Isactive).HasColumnName("ISActive");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.SiteName).HasMaxLength(100);
            entity.Property(e => e.TenantId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Taxis>(entity =>
        {
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TourActivate>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Act__727E838B3D4340D2");

            entity.ToTable("Tour_Activate");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Package).WithMany(p => p.TourActivates)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Acti__Packa__0B7CAB7B");
        });

        modelBuilder.Entity<TourFeatureItemAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Tour_Fea__C189298AF5931961");

            entity.ToTable("Tour_Feature_ItemAttribute");

            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ItemType).WithMany(p => p.TourFeatureItemAttributes)
                .HasForeignKey(d => d.ItemTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Feat__ItemT__7EACC042");
        });

        modelBuilder.Entity<TourFeatureItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId).HasName("PK__Tour_Fea__F51540DB96D20063");

            entity.ToTable("Tour_Feature_ItemType");

            entity.HasIndex(e => e.TypeName, "UQ__Tour_Fea__D4E7DFA87789EE7E").IsUnique();

            entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.TypeName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TourGuide>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Gui__727E838B41B23F38");

            entity.ToTable("Tour_Guides");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<TourGuideAssignment>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Gui__727E838B328C4121");

            entity.ToTable("Tour_GuideAssignments");

            entity.Property(e => e.AssignmentDate).HasDefaultValueSql("(CONVERT([date],sysutcdatetime()))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Guide).WithMany(p => p.TourGuideAssignments)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Guid__Guide__16EE5E27");

            entity.HasOne(d => d.Package).WithMany(p => p.TourGuideAssignments)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Guid__Packa__17E28260");
        });

        modelBuilder.Entity<TourLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Tour_Loc__E7FEA497770C97EE");

            entity.ToTable("Tour_Locations");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.FullAddress)
                .HasMaxLength(806)
                .HasComputedColumnSql("(concat(case when [Address] IS NOT NULL then [Address]+', ' else '' end,case when [City] IS NOT NULL then [City]+', ' else '' end,case when [State] IS NOT NULL then [State]+', ' else '' end,case when [Country] IS NOT NULL then [Country] else '' end))", true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.LocationHeadLine).HasMaxLength(500);
            entity.Property(e => e.LocationName).HasMaxLength(255);
            entity.Property(e => e.Longitude).HasMaxLength(50);
            entity.Property(e => e.PointImage).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.Package).WithMany(p => p.TourLocations)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Loca__Packa__4D1564AE");
        });

        modelBuilder.Entity<TourPackage>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Pac__727E838B5319DA62");

            entity.ToTable("Tour_Packages");

            entity.Property(e => e.BannerImage).HasMaxLength(500);
            entity.Property(e => e.BasePrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DurationDays)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageName).HasMaxLength(200);
            entity.Property(e => e.ShortDescription).HasMaxLength(4000);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.TourPackages)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tour_Packages_Tour_Category");
        });

        modelBuilder.Entity<TourPackageCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TourPackageCategory");

            entity.ToTable("Tour_PackageCategory");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TourPackageItemAttributeValue>(entity =>
        {
            entity.HasKey(e => new { e.PackageId, e.AttributeId }).HasName("PK__Tour_Pac__8E38A77427FB51E1");

            entity.ToTable("Tour_Package_ItemAttributeValue");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TourPackageItineraryDay>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Pac__727E838B12ABE673");

            entity.ToTable("Tour_Package_ItineraryDay");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Package).WithMany(p => p.TourPackageItineraryDays)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__04659998");
        });

        modelBuilder.Entity<TourPackageMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Tour_Pac__B2C2B5CF85E1AA28");

            entity.ToTable("Tour_PackageMedia");

            entity.Property(e => e.Caption).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MediaType).HasMaxLength(50);
            entity.Property(e => e.MediaUrl).HasMaxLength(500);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Package).WithMany(p => p.TourPackageMedia)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__7775B2CE");
        });

        modelBuilder.Entity<TourPackagePolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Tour_Pac__2E1339A48E657E0E");

            entity.ToTable("Tour_PackagePolicy");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PolicyType).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Package).WithMany(p => p.TourPackagePolicies)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__7D2E8C24");
        });

        modelBuilder.Entity<TourReview>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Rev__727E838B12E64736");

            entity.ToTable("Tour_Reviews");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Package).WithMany(p => p.TourReviews)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Revi__Packa__02E7657A");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicle");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.CarName).HasMaxLength(200);
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Fare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.InsurenceValidUntil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsurnceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Model)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PollucationCertificationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VehicleFeature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__VehicleF__82230A29E2F397F4");

            entity.ToTable("VehicleFeature");

            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeatureName).HasMaxLength(100);
            entity.Property(e => e.FeatureType).HasMaxLength(50);
            entity.Property(e => e.FeatureValue).HasMaxLength(100);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<VehicleFeatureMapping>(entity =>
        {
            entity.HasKey(e => new { e.VehicleId, e.FeatureId }).HasName("PK__VehicleF__CF4964109C8286F0");

            entity.ToTable("VehicleFeatureMapping");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Feature).WithMany(p => p.VehicleFeatureMappings)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleFe__Featu__65570293");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleFeatureMappings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleFe__Vehic__664B26CC");
        });

        modelBuilder.Entity<VehicleMediaMapping>(entity =>
        {
            entity.ToTable("VehicleMediaMapping");

            entity.Property(e => e.VehicleMediaMappingId).HasColumnName("VehicleMediaMappingID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Media).WithMany(p => p.VehicleMediaMappings)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleMe__Media__0682EC34");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleMediaMappings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleMe__Vehic__0777106D");
        });

        modelBuilder.Entity<VehicleTypeMaster>(entity =>
        {
            entity.HasKey(e => e.VehicleTypeId).HasName("PK__VehicleT__9F4496237AEF0410");

            entity.ToTable("VehicleTypeMaster");

            entity.HasIndex(e => e.TypeName, "UQ_TypeName").IsUnique();

            entity.Property(e => e.VehicleTypeId).HasColumnName("VehicleTypeID");
            entity.Property(e => e.AirConditioning).HasDefaultValue(true);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FuelType).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Transmission).HasMaxLength(50);
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<ViewAllBooking>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_AllBookings");

            entity.Property(e => e.ActualFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingNumber).HasMaxLength(50);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(102)
                .IsUnicode(false);
            entity.Property(e => e.DriverName).HasMaxLength(202);
            entity.Property(e => e.DropLocation).HasMaxLength(250);
            entity.Property(e => e.EstimatedFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModelName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PickupLocation).HasMaxLength(250);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
