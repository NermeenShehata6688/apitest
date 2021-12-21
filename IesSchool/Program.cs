using IesSchool.Context.Models;
using IesSchool.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Context;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPersistenceServices(connString);
builder.Services.AddApplicationServices();
builder.Services.ServiceInjection();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Iespolice",
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});

/// <summary>
/// identity
/// </summary>
///

builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = false;
                //Password requirements
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 6;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<iesIdentityContext>()
             .AddDefaultTokenProviders();
//services.Configure<ApplicationDbContext>(o =>
//{
//  o.Database.Migrate();
//});

// Add JWT Authentication 
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })

    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;

        //cfg.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidIssuer = Configuration["JwtIssuer"],
        //    ValidAudience = Configuration["JwtIssuer"],
        //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
        //    ClockSkew = TimeSpan.Zero, // remove delay of token when expire
        //                ValidateIssuerSigningKey = true,
        //    ValidateLifetime = true,
        //};
    });



builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.Configure<IdentityOptions>(options =>
options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


























builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
    }); ;
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("Iespolice");

app.UseAuthorization();

app.MapControllers();

app.Run();
