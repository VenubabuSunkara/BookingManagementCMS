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

    public virtual DbSet<CmsCompany> CmsCompanies { get; set; }

    public virtual DbSet<CompanyUser> CompanyUsers { get; set; }

    public virtual DbSet<CompanyUserRoleMapping> CompanyUserRoleMappings { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CouponCode> CouponCodes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<CustomerRelative> CustomerRelatives { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverVehicleMapping> DriverVehicleMappings { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<PackagePolicy> PackagePolicies { get; set; }

    public virtual DbSet<PageContent> PageContents { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

    public virtual DbSet<TourActivate> TourActivates { get; set; }

    public virtual DbSet<TourDestination> TourDestinations { get; set; }

    public virtual DbSet<TourGuide> TourGuides { get; set; }

    public virtual DbSet<TourGuideAssignment> TourGuideAssignments { get; set; }

    public virtual DbSet<TourItemAttribute> TourItemAttributes { get; set; }

    public virtual DbSet<TourItemType> TourItemTypes { get; set; }

    public virtual DbSet<TourItineraryDay> TourItineraryDays { get; set; }

    public virtual DbSet<TourPackage> TourPackages { get; set; }

    public virtual DbSet<TourPackageCategory> TourPackageCategories { get; set; }

    public virtual DbSet<TourPackageItem> TourPackageItems { get; set; }

    public virtual DbSet<TourPackageItemAttributeValue> TourPackageItemAttributeValues { get; set; }

    public virtual DbSet<TourPackageMedium> TourPackageMedia { get; set; }

    public virtual DbSet<TourPackagePolicy> TourPackagePolicies { get; set; }

    public virtual DbSet<TourReview> TourReviews { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleMedium> VehicleMedia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1HQFJ50;Database=BookingCMS;Trusted_Connection=True;TrustServerCertificate=True;");

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
            entity.HasKey(e => e.Id).HasName("PK__BookingD__3214EC0736513067");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PassengerGender).HasMaxLength(10);
            entity.Property(e => e.PassengerName).HasMaxLength(100);
            entity.Property(e => e.RelativeId).HasColumnName("RelativeID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingDetails_BookingOrder");

            entity.HasOne(d => d.Relative).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.RelativeId)
                .HasConstraintName("FK_BookingDetails_UserRelatives");
        });

        modelBuilder.Entity<BookingOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingO__3214EC07D1F3E653");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TravelDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CouponCode).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.CouponCodeId)
                .HasConstraintName("FK_BookingOrders_CouponCodes");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingOrders_Customers");

            entity.HasOne(d => d.Driver).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK_BookingOrders_Drivers");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BookingOrders)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_BookingOrders_Vehicle");
        });

        modelBuilder.Entity<CmsCompany>(entity =>
        {
            entity.ToTable("CMS_Company");

            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyU__3214EC07A8955C18");

            entity.ToTable("CompanyUser");

            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHashText).IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompanyUserRoleMapping>(entity =>
        {
            entity.ToTable("CompanyUserRoleMapping");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Admin).WithMany(p => p.CompanyUserRoleMappings)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUserRoleMapping_AdminId");

            entity.HasOne(d => d.Role).WithMany(p => p.CompanyUserRoleMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUserRoleMapping_RoleId");
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
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3214EC0758E8E7B0");

            entity.Property(e => e.ApproveDriver).HasDefaultValue(false);
            entity.Property(e => e.AvailabilityStatus).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(64);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Photo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DriverVehicleMapping>(entity =>
        {
            entity.ToTable("DriverVehicleMapping");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverVehicleMappings)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK_DriverVehicleMapping_DriverId");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.DriverVehicleMappings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_DriverVehicleMapping_VehicleId");
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

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Modules__3214EC07F24F5AFF");

            entity.HasIndex(e => e.Name, "UQ__Modules__737584F696CCD388").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
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
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07293E2D2D");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMode).HasMaxLength(20);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.RemainingAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_BookingOrders");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07EA5AA9D5");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Permissions_Modules");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC072D104A2C");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3214EC0742CD588F");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Roles");
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

        modelBuilder.Entity<TourPackage>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Tour_Pac__727E838B57B541D7");

            entity.ToTable("Tour_Packages");

            entity.Property(e => e.BannerImage).HasMaxLength(500);
            entity.Property(e => e.BasePrice).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ItemGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PackageName).HasMaxLength(200);
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

            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Make)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VehicleMedium>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.MediaName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MediaType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleMedia)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleMedia_VehicleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
