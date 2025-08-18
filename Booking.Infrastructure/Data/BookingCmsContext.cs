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

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageMedium> PackageMedia { get; set; }

    public virtual DbSet<PageContent> PageContents { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

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

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

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

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingD__3214EC0736513067");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingDetails_BookingOrder");

            entity.HasOne(d => d.Relative).WithMany(p => p.BookingDetails).HasConstraintName("FK_BookingDetails_UserRelatives");
        });

        modelBuilder.Entity<BookingOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingO__3214EC07D1F3E653");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CouponCode).WithMany(p => p.BookingOrders).HasConstraintName("FK_BookingOrders_CouponCodes");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingOrders_Customers");

            entity.HasOne(d => d.Driver).WithMany(p => p.BookingOrders).HasConstraintName("FK_BookingOrders_Drivers");

            entity.HasOne(d => d.Package).WithMany(p => p.BookingOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingOrders_Packages");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BookingOrders).HasConstraintName("FK_BookingOrders_Vehicle");
        });

        modelBuilder.Entity<CmsCompany>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyU__3214EC07A8955C18");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CompanyUserRoleMapping>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Admin).WithMany(p => p.CompanyUserRoleMappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUserRoleMapping_AdminId");

            entity.HasOne(d => d.Role).WithMany(p => p.CompanyUserRoleMappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUserRoleMapping_RoleId");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07A292D8D0");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ReferralBonusGranted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAddr__3214EC07828EDCD7");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AddressType).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__Addre__6754599E");

            entity.HasOne(d => d.Customers).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__Custo__68487DD7");
        });

        modelBuilder.Entity<CustomerRelative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRela__3214EC07501004B3");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRelatives)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRelat__Custo__6D0D32F4");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3214EC0758E8E7B0");

            entity.Property(e => e.ApproveDriver).HasDefaultValue(false);
            entity.Property(e => e.AvailabilityStatus).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<DriverVehicleMapping>(entity =>
        {
            entity.HasOne(d => d.Driver).WithMany(p => p.DriverVehicleMappings).HasConstraintName("FK_DriverVehicleMapping_DriverId");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.DriverVehicleMappings).HasConstraintName("FK_DriverVehicleMapping_VehicleId");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Modules__3214EC07F24F5AFF");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Packages__3214EC078F2C559E");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<PackageMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PackageM__3214EC072C36A961");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PackageMe__Updat__06CD04F7");
        });

        modelBuilder.Entity<PageContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PageContent\\");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07293E2D2D");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PaymentStatus).HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_BookingOrders");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07EA5AA9D5");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions).HasConstraintName("FK_Permissions_Modules");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC072D104A2C");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3214EC0742CD588F");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Roles");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<VehicleMedium>(entity =>
        {
            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleMedia_VehicleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}