using Application.DTOs;
using Application.DTOs.ProductDTO;
using Application.DTOsNS.UserType;
using Application.Mappers;
using Application.Repositories;
using Application.RepositoriesNS;
using Application.Services;
using Application.Services.Authintication;
using Application.Services.CartNS;
using Application.Services.Categoery;
using Application.Services.Payment;
using Application.Services.ProductServiceNS;
using Application.Services.UserInformation;
using Application.Services___Repositores.Mail;
using Application.Services___Repositores.NotficationNS;
using Application.Services___Repositores.OrderNs;
using Application.Services___Repositores.OrderService;
using Application.Services___Repositores.UserInformation;
using Application.Utilities;
using HardWhere.Application.Product.Validators;
using HardWherePresenter;
using infrastructure;
using infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
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
builder.Services.AddScoped<IProductService, Application.Services.ProductServiceNS.ProductService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<INotficationRepository, NotficationRepository>();
builder.Services.AddScoped<INotficationService, NotficationService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddScoped<IUserInformationServiceAddress, UserInfromationAddresses>();

builder.Services.AddScoped<IAddress, AddressRepository>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddScoped<IFileService, Application.DTOs.ProductDTO.FileService>();
builder.Services.AddTransient<IStringRandomGenarotor<SKUGenerator>, SKUGenerator>();
builder.Services.AddTransient<IStringRandomGenarotor<SlugGenerator>, SlugGenerator>();
builder.Services.AddSignalR();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"])
            )
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if ((path.StartsWithSegments("/api/public/notify")))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<HardwhereDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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
    auth.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("MyPolicy");
}
StripeConfiguration.ApiKey =
    "sk_test_51OXPeOCZ8fe8KVeaTMjg8Fvfd0rUmabgZRITBCCkGlv2psPpSaf4vFhC02uo8lmr8rglhjtHG5TsDRfD8lWxDlIu00Nq8rxuZK";
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseStaticFiles(
    new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")
        ),
        RequestPath = new PathString("/images")
    }
);
app.UseRouting();

app.MapHub<NotificationHub>("api/public/notify");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
