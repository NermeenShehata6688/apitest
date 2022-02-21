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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using IesSchool.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPersistenceServices(connString);
builder.Services.AddApplicationServices();
builder.Services.ServiceInjection();
builder.Services.AddControllers();
//builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Iespolice",
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTUzMDQwQDMxMzkyZTM0MmUzMGlHSVBGS0VVekpkZ0xoTmFkUnp3WU5mOEFwaTd2M2tjNHo4cnB1NU5XUzQ9");

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

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


builder.Services.AddAuthentication(opt => {

    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})


.AddJwtBearer(options =>
{
    options.SecurityTokenValidators.Clear();

    options.TokenValidationParameters = new TokenValidationParameters
    {


        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler
    {
        // Disable the built-in JWT claims mapping feature.
        InboundClaimTypeMap = new Dictionary<string, string>()
    });
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();

//builder.Services.AddSwaggerGen();
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//app.UseMiddleware<BasicAuthenticationMiddleware>();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/secure"), appBuilder =>
{
    appBuilder.UseMiddleware<BasicAuthenticationMiddleware>();
});
app.Run();

