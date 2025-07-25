using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class BookingManagementCmsContext : DbContext
{
    public BookingManagementCmsContext()
    {
    }

    public BookingManagementCmsContext(DbContextOptions<BookingManagementCmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<AdminRoleMapping> AdminRoleMappings { get; set; }

    public virtual DbSet<AdminUser> AdminUsers { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CouponCode> CouponCodes { get; set; }

    public virtual DbSet<DriverAndVehicle> DriverAndVehicles { get; set; }

    public virtual DbSet<DriverVehicleMedium> DriverVehicleMedia { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Logger> Loggers { get; set; }

    public virtual DbSet<PageContent> PageContents { get; set; }

    public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

    public virtual DbSet<Taxis> Taxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1HQFJ50;Database=BookingManagementCMS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AddressT__3214EC07CFAADDEF");

            entity.ToTable("AddressType");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TypeName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AdminRoleMapping>(entity =>
        {
            entity.ToTable("AdminRoleMapping");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comments)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminUse__3214EC072AA5F6F0");

            entity.ToTable("AdminUser");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
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

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ThreeLetterIsoCode).HasMaxLength(3);
            entity.Property(e => e.TwoLetterIsoCode).HasMaxLength(2);
        });

        modelBuilder.Entity<CouponCode>(entity =>
        {
            entity.Property(e => e.Code)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RangeMax).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RangeMin).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<DriverAndVehicle>(entity =>
        {
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DriverContact)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DriverEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DriverFirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DriverLastName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DriverLicenceNumber).HasMaxLength(50);
            entity.Property(e => e.IsApprove).HasColumnName("isApprove");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsernName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.VehicleType).WithMany(p => p.DriverAndVehicles)
                .HasForeignKey(d => d.VehicleTypeId)
                .HasConstraintName("FK_DriverAndVehicles_VehicleTypes");
        });

        modelBuilder.Entity<DriverVehicleMedium>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MediaName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.DriverVehicle).WithMany(p => p.DriverVehicleMedia)
                .HasForeignKey(d => d.DriverVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DriverVehicleMedia_DriverVehicle");
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

        modelBuilder.Entity<Logger>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK_BookingManagemnet_EventLog");

            entity.ToTable("Logger");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EventCode).HasMaxLength(100);
            entity.Property(e => e.EventMachineName).HasMaxLength(100);
            entity.Property(e => e.EventType).HasMaxLength(5);
            entity.Property(e => e.EventUrl).HasMaxLength(2000);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(100)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Source).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(250);
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
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PermissionGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC0735322E35");

            entity.ToTable("PermissionGroup");

            entity.HasIndex(e => e.GroupName, "UQ__Permissi__6EFCD4342D07171C").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(4000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ReviewComment1).HasColumnName("ReviewComment");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07EB7387AC");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<RolePermissionMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminPer__3214EC0726C1B9E2");

            entity.ToTable("RolePermissionMapping");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissionMappings)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdminPerm__Updat__66603565");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissionMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminPermissionMapping_Role");
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC0DB57BD1");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E381E29B68D").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346DA76940").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
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
            entity.Property(e => e.ReferredByUserId).HasColumnName("ReferredByUserID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAddr__3214EC072AEC4B81");

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.CityName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CountryName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StateName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AddressType).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__Addre__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__IsDef__5070F446");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleT__3214EC07C8C05CF6");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleType1)
                .HasMaxLength(100)
                .HasColumnName("VehicleType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
