using Application.DTOs.ProductDTO;
using Application.DTOsNS.UserType;
using Application.Mappers;
using Application.Repositories;
using Application.Services;
using Application.Services.Authintication;
using Application.Services.CartNS;
using Application.Services.Categoery;
using Application.Services.Payment;
using Application.Services.ProductServiceNS;
using Application.Services.UserInformation;
using Application.Utilities;
using HardWhere.Application.Product.Validators;
using infrastructure;
using infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .Preserve;
    });
;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        }
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ProductValidator>();
builder.Services.AddScoped<IUserAuthicticateService, UserAuthinticationService>();
builder.Services.AddScoped<IUserAuthinticationRepoisitory, UserAuthiticationRepository>();
builder.Services.AddScoped<ITokenUtility, TokensUtilitiy>();
builder.Services.AddScoped<IStringUtility, StringUtility>();
builder.Services.AddScoped<IPaymentGateWayService, PaymentGateWayService>();
builder.Services.AddScoped<IPaymentRepository, PaymentGateWayRepository>();
builder.Services.AddScoped<IUserInformationRepository, UserInformationRepository>();
builder.Services.AddScoped<IUserInformationService<UserTypeDTO>, UserTypeInformationService>();
builder.Services.AddScoped<ICategoeryRepository, CateogeryRepository>();
builder.Services.AddScoped<ICategoeryService, CategoeryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddTransient<IStringRandomGenarotor<SKUGenerator>, SKUGenerator>();
builder.Services.AddTransient<IStringRandomGenarotor<SlugGenerator>, SlugGenerator>();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var hmac = new HMACSHA512(
            Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"])
        );

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(hmac.Key)
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                logger.LogError("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<HardwhereDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(UserAutoMaper).Assembly);
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy(
        "Bearer",
        new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            .RequireAuthenticatedUser()
            .Build()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("MyPolicy");
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(
    new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")
        ),
        RequestPath = new PathString("/images")
    }
);

app.MapControllers();

app.Run();
