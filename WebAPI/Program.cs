using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Context;
using WebAPI.Data;
using WebAPI.Helper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.FileProviders;
using WebAPI.Helper.Token;
using WebAPI.Models;
using WebAPI.Helper.SearchRole;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StudentTestDB");
builder.Services.AddDbContext<StudentDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUploadHelper, UploadHelper>();
builder.Services.AddScoped<ITokenHelper, TokenHelper>();
builder.Services.AddScoped<IUsertoRole, UsertoRole>();
builder.Services.AddScoped<IStudenService, StudentManager>();
builder.Services.AddIdentity<Users,IdentityRole>().AddEntityFrameworkStores<StudentDBContext>();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Title",
        Version = "V1",
        Description = "API Description"
    });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description="Authorization header using the Bearer scheme. Example \"Authorization: Bearer {token}\"",
        Name="Authorization",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Scheme="Bearer",
        Reference=new OpenApiReference
        {
            Type=ReferenceType.SecurityScheme,
            Id="Bearer"
        }
    };
    swagger.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securitySchema,Array.Empty<string>() }
    });
});


builder.Services.AddAuthentication(f =>
{
    f.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    f.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(k =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    k.SaveToken = true;
    k.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey= new SymmetricSecurityKey(Key),
        ClockSkew=TimeSpan.Zero
    };
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
    RequestPath="/Images"
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
