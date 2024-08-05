using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureApiWithAuthentication.Authorization;
using SecureApiWithAuthentication.Data;
using SecureApiWithAuthentication.filters;
using SecureApiWithAuthentication.Logging;
using SecureApiWithAuthentication.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<permissionBasedAuthorization>();
       // options.Filters.Add<FiltersAction>();
       });
/*var configuration = builder.Configuration.GetSection("Attatchments").Get<AttachmentServices>();
builder.Services.AddSingleton(configuration);*/

// ways to configure

/// prefered to be singlton because we does not have to change it 
/*var attchments = new AttachmentServices();
builder.Configuration.GetSection("Attatchments").Bind(attchments);
builder.Services.AddSingleton(attchments);*/

///third way when we inject it we have three interfaces to inject it 
///first ioptions<AttachmentServices> singlton by default 
///second way ioptionssnapshot<AttachmentServices> is scoped by default 
///third way i optionsmonitoring<AttachmentServices> 

/*builder.Services.Configure<AttachmentServices>(builder.Configuration.GetSection("Attatchments"));*/



builder.Services.AddSingleton<IloggerServices, LogginServies>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var jwttoken = builder.Configuration.GetSection("jwt").Get<JwtOtptions>();

builder.Services.AddSingleton(jwttoken);

builder.Services.AddDbContext<AppDbcontext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options=>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwttoken.Issuer,
            ValidateAudience = true,
            ValidAudience = jwttoken.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwttoken.signinKey)),
        };
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ProfilingMiddleware>();
app.UseMiddleware<RateLimitMiddelware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
