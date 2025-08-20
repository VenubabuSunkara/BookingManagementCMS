using Amazon.Runtime;
using Amazon.S3;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Domain.DomainServices.DataTableLoader;
using Booking.Infrastructure;
using Booking.Infrastructure.Data.Models;
using Booking.Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SendGrid.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddResponseCaching();
builder.Services.AddOutputCache();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<CompanyUser>, PasswordHasher<CompanyUser>>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.LoginPath = "/Account/Index";
});
builder.Services.AddAuthorization();  // required
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ICouponCodeService, CouponCodeService>();
builder.Services.AddScoped<IDataTableService, DataTableService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingDetailsService, BookingDetailsService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Configuration["SendGrid:ApiKey"];
});
builder.Services.AddTransient<IEmailService, SendGridEmailService>();

builder.Services.AddSingleton<ICloudStorageService, AzureBlobStorageService>();
// or for AWS:
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new BasicAWSCredentials(
    builder.Configuration["AWS:AccessKey"],
    builder.Configuration["AWS:SecretKey"]
);
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddSingleton<ICloudStorageService, AwsS3StorageService>();
// or for GCP:
builder.Services.AddSingleton<ICloudStorageService, GoogleCloudStorageService>();


//builder.Services.AddResponseCompression(options => {
//    options.EnableForHttps = true;
//    options.Providers.Add<BrotliCompressionProvider>();
//    options.Providers.Add<GzipCompressionProvider>();
//});
//builder.Services.Configure<BrotliCompressionProviderOptions>(opts =>
//    opts.Level = CompressionLevel.Fastest);
//builder.Services.Configure<GzipCompressionProviderOptions>(opts =>
//    opts.Level = CompressionLevel.Optimal);

//builder.Services.AddAntiforgery(options => {
//    options.FormFieldName = "X-CSRF-TOKEN";
//    options.HeaderName = "X-CSRF-TOKEN";
//});

// Runtime Compilation
var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    // Add CSP, etc.
    await next();
});
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.UseResponseCaching();
app.UseOutputCache();
//app.UseResponseCompression();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
