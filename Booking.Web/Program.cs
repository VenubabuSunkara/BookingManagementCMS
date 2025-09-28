using Amazon.Runtime;
using Amazon.S3;
using Booking.Application.DTOs;
using Booking.Application.Hubs;
using Booking.Application.Interfaces;
using Booking.Application.Services;
using Booking.Infrastructure;
using Booking.Infrastructure.Data.Models;
using Booking.Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));
builder.Services.Configure<GoogleSettings>(
    builder.Configuration.GetSection("Google")
);
// If you want GoogleSettings to be directly injectable (not via IOptions), you can also do:
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<GoogleSettings>>().Value
);
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddResponseCaching();
builder.Services.AddOutputCache();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<CompanyUser>, PasswordHasher<CompanyUser>>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.SessionStore = builder.Services.BuildServiceProvider().GetRequiredService<ITicketStore>();
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});
builder.WebHost.ConfigureKestrel(o =>
{
    o.ConfigureEndpointDefaults(lo => lo.Protocols =
        Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1);
});
builder.Services.AddSignalR();
builder.Services.AddAuthorization();  // required
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ICouponCodeService, CouponCodeService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingDetailsService, BookingDetailsService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IPackageCategoryService, PackageCategoryService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IReviewCommentService, ReviewCommentService>();
builder.Services.AddScoped<IDriverVehicleService, DriverVehicleService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPackageMediaService, PackageMediaService>();
builder.Services.AddScoped<IPackageLocationService, PackageLocationService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<ISmtpEmailService, SmtpEmailService>();
builder.Services.AddScoped<ISendGridEmailService, SendGridEmailService>();
builder.Services.AddHttpClient<IGooglePlacesService, GooglePlacesService>();

builder.Services.AddSingleton<ICloudStorageService, AzureBlobStorageService>();
builder.Services.AddSingleton<FileReaderService>();
builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Configuration["SendGrid:ApiKey"];
});
builder.Services.AddTransient<SmtpEmailService>();
builder.Services.AddTransient<SendGridEmailService>();


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

// Add MiniProfiler services
// If using Entity Framework Core, add profiling for it as well (see the end)
builder.Services.AddMiniProfiler().AddEntityFramework();

// Runtime Compilation
var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();
app.UseMiniProfiler();
app.MapHub<NotificationHub>("/notificationHub");
using (var scope = app.Services.CreateScope())
{
    var ticketStore = scope.ServiceProvider.GetRequiredService<ITicketStore>();
    var options = app.Services.GetRequiredService<IOptionsMonitor<CookieAuthenticationOptions>>();
    options.Get(CookieAuthenticationDefaults.AuthenticationScheme).SessionStore = ticketStore;
}

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
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
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
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
