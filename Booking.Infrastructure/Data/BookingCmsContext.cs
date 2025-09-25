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

    public virtual DbSet<DriverRating> DriverRatings { get; set; }

    public virtual DbSet<DriverVehicle> DriverVehicles { get; set; }

    public virtual DbSet<DriverVehicleAvailability> DriverVehicleAvailabilities { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<PackagePolicy> PackagePolicies { get; set; }

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

    public virtual DbSet<TourDestination> TourDestinations { get; set; }

    public virtual DbSet<TourGuide> TourGuides { get; set; }

    public virtual DbSet<TourGuideAssignment> TourGuideAssignments { get; set; }

    public virtual DbSet<TourItemAttribute> TourItemAttributes { get; set; }

    public virtual DbSet<TourItemType> TourItemTypes { get; set; }

    public virtual DbSet<TourItineraryDay> TourItineraryDays { get; set; }

    public virtual DbSet<TourLocation> TourLocations { get; set; }

    public virtual DbSet<TourPackage> TourPackages { get; set; }

    public virtual DbSet<TourPackageCategory> TourPackageCategories { get; set; }

    public virtual DbSet<TourPackageItem> TourPackageItems { get; set; }

    public virtual DbSet<TourPackageItemAttributeValue> TourPackageItemAttributeValues { get; set; }

    public virtual DbSet<TourPackageMedium> TourPackageMedia { get; set; }

    public virtual DbSet<TourPackagePolicy> TourPackagePolicies { get; set; }

    public virtual DbSet<TourReview> TourReviews { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleFeature> VehicleFeatures { get; set; }

    public virtual DbSet<VehicleFeatureMapping> VehicleFeatureMappings { get; set; }

    public virtual DbSet<VehicleMediaMapping> VehicleMediaMappings { get; set; }

    public virtual DbSet<VehicleRating> VehicleRatings { get; set; }

    public virtual DbSet<ViewAllBooking> ViewAllBookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AddressT__3214EC07E20FF0DB");

            entity.ToTable("AddressType");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TypeName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

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
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

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
            entity.HasKey(e => e.BookingDetailId).HasName("PK__BookingD__8136D45A32E6C295");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DistanceInKm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ExtraCharges)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.StopLocation).HasMaxLength(250);

            entity.HasOne(d => d.BookingOrder).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingOrderId)
                .HasConstraintName("FK_BookingDetails_BookingOrders");
        });

        modelBuilder.Entity<BookingOrder>(entity =>
        {
            entity.HasKey(e => e.BookingOrderId).HasName("PK__BookingO__72C389191003F68F");

            entity.HasIndex(e => e.BookingNumber, "UQ__BookingO__AAC320BF2C8D3609").IsUnique();

            entity.Property(e => e.ActualFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BookingNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DistanceInKm).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DropLocation).HasMaxLength(250);
            entity.Property(e => e.EstimatedFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExtraCharges).HasColumnType("decimal(18, 2)");
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
                .HasConstraintName("FK_BookingOrders_Drivers");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_BookingOrders_Vehicles");
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyU__3214EC07CD77A537");

            entity.ToTable("CompanyUser");

            entity.HasIndex(e => e.UserId, "UQ_CompanyUser_AspNetUser").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithOne(p => p.CompanyUser)
                .HasForeignKey<CompanyUser>(d => d.UserId)
                .HasConstraintName("FK_CompanyUser_ASPNetUser");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.KeyName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.KeyValue)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ThreeLetterIsoCode).HasMaxLength(3);
            entity.Property(e => e.TwoLetterIsoCode).HasMaxLength(2);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CouponCode>(entity =>
        {
            entity.Property(e => e.Code)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DiscountValue)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PriceRangeMax).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PriceRangeMin).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07A292D8D0");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Customer__85FB4E382CAFDA36").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053474500301").IsUnique();

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
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAddr__3214EC07828EDCD7");

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
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.LandMark)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StateName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AddressType).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__Addre__6754599E");

            entity.HasOne(d => d.Customers).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__Custo__68487DD7");
        });

        modelBuilder.Entity<CustomerRelative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRela__3214EC07501004B3");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRelatives)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRelat__Custo__6D0D32F4");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Drivers__F1B1CD2412374892");

            entity.ToTable("Driver");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ApproveDriver).HasDefaultValue(false);
            entity.Property(e => e.AvailabilityStatus).HasDefaultValue(true);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("isActive");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
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
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverMediaMappings)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__DriverMed__Drive__21D600EE");

            entity.HasOne(d => d.Media).WithMany(p => p.DriverMediaMappings)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK__DriverMed__Media__20E1DCB5");
        });

        modelBuilder.Entity<DriverRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__DriverRa__FCCDF85C63409CB2");

            entity.ToTable("DriverRating");

            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverRatings)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__DriverRat__Drive__77DFC722");

            entity.HasOne(d => d.Passenger).WithMany(p => p.DriverRatings)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__DriverRat__Passe__78D3EB5B");
        });

        modelBuilder.Entity<DriverVehicle>(entity =>
        {
            entity.HasKey(e => new { e.DriverId, e.VehicleId }).HasName("PK__DriverVe__C5C7786FE1B7386D");

            entity.ToTable("DriverVehicle");

            entity.HasIndex(e => e.VehicleId, "UQ__DriverVe__476B54B38B90903A").IsUnique();

            entity.HasIndex(e => e.DriverId, "UQ__DriverVe__F1B1CD252DBA8347").IsUnique();

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithOne(p => p.DriverVehicle)
                .HasForeignKey<DriverVehicle>(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverVeh__Drive__65C116E7");

            entity.HasOne(d => d.Vehicle).WithOne(p => p.DriverVehicle)
                .HasForeignKey<DriverVehicle>(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverVeh__Vehic__66B53B20");
        });

        modelBuilder.Entity<DriverVehicleAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__DriverVe__DA397991675936AC");

            entity.ToTable("DriverVehicleAvailability");

            entity.HasIndex(e => new { e.VehicleId, e.DriverId, e.AvailableDate, e.SlotStart, e.SlotEnd }, "UQ_Availability").IsUnique();

            entity.Property(e => e.AvailabilityId).HasColumnName("AvailabilityID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverVehicleAvailabilities)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__DriverVeh__Drive__08162EEB");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.DriverVehicleAvailabilities)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__DriverVeh__Vehic__090A5324");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EmailSubject).HasMaxLength(200);
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SenderEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA497B0C70266");

            entity.ToTable("Location");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.MediaId);

            entity.Property(e => e.MediaId).HasColumnName("MediaID");
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
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menus__C99ED230F723DAFD");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.MenuUrl).HasMaxLength(255);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("FK__Menus__ParentMen__6497E884");
        });

        modelBuilder.Entity<PackagePolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__PackageP__2E1339A46FD6602F");

            entity.ToTable("PackagePolicy");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.PolicyType).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Package).WithMany(p => p.PackagePolicies)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PackagePo__Packa__7EC1CEDB");
        });

        modelBuilder.Entity<PageContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PageContent\\");

            entity.ToTable("PageContent");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PageContent1).HasColumnName("PageContent");
            entity.Property(e => e.PageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Placeholder)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38EF28EBAF");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMode).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Success");

            entity.HasOne(d => d.BookingOrder).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_BookingOrders");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2FDD671F7B");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PermissionName).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("PK__RoleMenu__F86287B652E0062F");

            entity.HasIndex(e => new { e.RoleId, e.MenuId }, "UQ__RoleMenu__966323389E598D52").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenus__MenuI__77AABCF8");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenus__RoleI__76B698BF");
        });

        modelBuilder.Entity<RoleMenuPermission>(entity =>
        {
            entity.HasKey(e => e.RoleMenuPermissionId).HasName("PK__RoleMenu__090D1B7B5633EAAE");

            entity.HasIndex(e => new { e.RoleId, e.MenuId, e.PermissionId }, "UQ__RoleMenu__B88C85C374A6E595").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenuPermissions)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenuP__MenuI__7F4BDEC0");

            entity.HasOne(d => d.Permission).WithMany(p => p.RoleMenuPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenuP__Permi__004002F9");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenuPermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleMenuP__RoleI__7E57BA87");
        });

        modelBuilder.Entity<SeasonalPricing>(entity =>
        {
            entity.HasKey(e => e.PricingId).HasName("PK__Seasonal__EC306B72938CFE06");

            entity.ToTable("SeasonalPricing");

            entity.Property(e => e.PricingId).HasColumnName("PricingID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.Multiplier).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.SeasonalPricings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__SeasonalP__Vehic__0FB750B3");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK__Site__B9DCB903CED0C8B7");

            entity.ToTable("Site");

            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(false)
                .HasColumnName("ISActive");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.SiteName).HasMaxLength(100);
            entity.Property(e => e.TenantId).HasDefaultValueSql("(newid())");
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
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<TourActivate>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Act__727E838BFB4FD0A0");

            entity.ToTable("Tour_Activate");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FromDate).HasMaxLength(200);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.ToDate).HasMaxLength(200);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Package).WithMany(p => p.TourActivates)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Acti__Packa__3DE82FB7");
        });

        modelBuilder.Entity<TourDestination>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Des__727E838B7747B2DF");

            entity.ToTable("Tour_Destinations");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Location).WithMany(p => p.TourDestinations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Tour_Destinations_Tour_Location");

            entity.HasOne(d => d.Package).WithMany(p => p.TourDestinations)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Dest__Packa__382F5661");
        });

        modelBuilder.Entity<TourGuide>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Gui__727E838B66080752");

            entity.ToTable("Tour_Guides");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<TourGuideAssignment>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__TourGuid__727E838B91AF87D5");

            entity.ToTable("Tour_GuideAssignments");

            entity.Property(e => e.AssignmentDate).HasDefaultValueSql("(CONVERT([date],sysutcdatetime()))");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Guide).WithMany(p => p.TourGuideAssignments)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourGuide__Guide__5006DFF2");

            entity.HasOne(d => d.Package).WithMany(p => p.TourGuideAssignments)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourGuide__Packa__50FB042B");
        });

        modelBuilder.Entity<TourItemAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Tour_Ite__C189298A45C97758");

            entity.ToTable("Tour_ItemAttribute");

            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.ItemType).WithMany(p => p.TourItemAttributes)
                .HasForeignKey(d => d.ItemTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Item__ItemT__30592A6F");
        });

        modelBuilder.Entity<TourItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId).HasName("PK__Tour_Ite__F51540DBC17B80E5");

            entity.ToTable("Tour_ItemType");

            entity.HasIndex(e => e.TypeName, "UQ__Tour_Ite__D4E7DFA8C458F2DF").IsUnique();

            entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.TypeName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
        });

        modelBuilder.Entity<TourItineraryDay>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Iti__727E838BCC083096");

            entity.ToTable("Tour_ItineraryDay");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Package).WithMany(p => p.TourItineraryDays)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Itin__Packa__2AD55B43");
        });

        modelBuilder.Entity<TourLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__TourLoca__E7FEA49720B2F576");
            entity.ToTable("Tour_Locations");

            entity.ToTable(tb => tb.HasTrigger("TRG_TourLocations_UpdatedOn"));

            entity.HasIndex(e => e.City, "IX_TourLocations_City");

            entity.HasIndex(e => e.Country, "IX_TourLocations_Country");

            entity.HasIndex(e => e.IsActive, "IX_TourLocations_IsActive");

            entity.HasIndex(e => e.State, "IX_TourLocations_State");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.FullAddress)
                .HasMaxLength(806)
                .HasComputedColumnSql("(concat(case when [Address] IS NOT NULL then [Address]+', ' else '' end,case when [City] IS NOT NULL then [City]+', ' else '' end,case when [State] IS NOT NULL then [State]+', ' else '' end,case when [Country] IS NOT NULL then [Country] else '' end))", true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.LocationHeadLine).HasMaxLength(500);
            entity.Property(e => e.LocationName).HasMaxLength(255);
            entity.Property(e => e.Longitude).HasMaxLength(50);
            entity.Property(e => e.PointImage).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.ZipCode).HasMaxLength(20);
        });

        modelBuilder.Entity<TourPackage>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Pac__727E838B57B541D7");

            entity.ToTable("Tour_Packages");

            entity.Property(e => e.BannerImage).HasMaxLength(500);
            entity.Property(e => e.BasePrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageName).HasMaxLength(200);
            entity.Property(e => e.ShortDescription).HasMaxLength(4000);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Category).WithMany(p => p.TourPackages)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Tour_Packages_Tour_Category");
        });

        modelBuilder.Entity<TourPackageCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TourPackageCategory");

            entity.ToTable("Tour_PackageCategory");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<TourPackageItem>(entity =>
        {
            entity.HasKey(e => e.PackageItemId).HasName("PK__Tour_Pac__D45F719168D2774E");

            entity.ToTable("Tour_PackageItem");

            entity.Property(e => e.PackageItemId).HasColumnName("PackageItemID");
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemName).HasMaxLength(200);
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.ItemType).WithMany(p => p.TourPackageItems)
                .HasForeignKey(d => d.ItemTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__ItemT__24E777C3");

            entity.HasOne(d => d.Package).WithMany(p => p.TourPackageItems)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__23F3538A");
        });

        modelBuilder.Entity<TourPackageItemAttributeValue>(entity =>
        {
            entity.HasKey(e => new { e.PackageItemId, e.AttributeId }).HasName("PK__Tour_Pac__6847E3096E28C0A0");

            entity.ToTable("Tour_PackageItemAttributeValue");

            entity.Property(e => e.PackageItemId).HasColumnName("PackageItemID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Attribute).WithMany(p => p.TourPackageItemAttributeValues)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Attri__370627FE");

            entity.HasOne(d => d.PackageItem).WithMany(p => p.TourPackageItemAttributeValues)
                .HasForeignKey(d => d.PackageItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__361203C5");
        });

        modelBuilder.Entity<TourPackageMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Tour_Pac__B2C2B5CF8389C051");

            entity.ToTable("Tour_PackageMedia");

            entity.Property(e => e.Caption).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.MediaType).HasMaxLength(50);
            entity.Property(e => e.MediaUrl).HasMaxLength(500);
            entity.Property(e => e.SequenceOrder).HasDefaultValue(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Package).WithMany(p => p.TourPackageMedia)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__7167D3BD");
        });

        modelBuilder.Entity<TourPackagePolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Tour_Pac__2E1339A423D24903");

            entity.ToTable("Tour_PackagePolicy");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ItemOrder).HasDefaultValue(0);
            entity.Property(e => e.PolicyType).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Package).WithMany(p => p.TourPackagePolicies)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Pack__Packa__093F5D4E");
        });

        modelBuilder.Entity<TourReview>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Rev__727E838BF13989CA");

            entity.ToTable("Tour_Reviews");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Package).WithMany(p => p.TourReviews)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Revi__Packa__308E3499");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicle");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.BasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FuelType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.Make)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModelName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TaxRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VehicleFeature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__VehicleF__82230A294B4D26C3");

            entity.ToTable("VehicleFeature");

            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeatureName).HasMaxLength(100);
            entity.Property(e => e.FeatureType).HasMaxLength(50);
            entity.Property(e => e.FeatureValue).HasMaxLength(100);
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<VehicleFeatureMapping>(entity =>
        {
            entity.HasKey(e => new { e.VehicleId, e.FeatureId }).HasName("PK__VehicleF__CF496410182BC7F5");

            entity.ToTable("VehicleFeatureMapping");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Feature).WithMany(p => p.VehicleFeatureMappings)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleFe__Featu__7226EDCC");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleFeatureMappings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleFe__Vehic__7132C993");
        });

        modelBuilder.Entity<VehicleMediaMapping>(entity =>
        {
            entity.ToTable("VehicleMediaMapping");

            entity.Property(e => e.VehicleMediaMappingId).HasColumnName("VehicleMediaMappingID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Media).WithMany(p => p.VehicleMediaMappings)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK__VehicleMe__Media__1A34DF26");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleMediaMappings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehicleMe__Vehic__1B29035F");
        });

        modelBuilder.Entity<VehicleRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__VehicleR__FCCDF85CDA96E2F4");

            entity.ToTable("VehicleRating");

            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ItemGUID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Passenger).WithMany(p => p.VehicleRatings)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__VehicleRa__Passe__00750D23");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleRatings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehicleRa__Vehic__7F80E8EA");
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
