using DotNetEnv;
using MicroondasMVC_Benner;
using MicroondasMVC_Benner.Models.API;
using MicroondasMVC_Benner.Repository.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load(Path.Combine("Config", ".env"));

string? encryptedConn = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(encryptedConn))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

string decryptedConn = CryptoHelper.Decrypt(encryptedConn);

// 3. Pass the RAW string directly to UseSqlServer
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(decryptedConn));

builder.Services.AddScoped<IAuthInterface, AuthService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Mude para true em Produção
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Microondas}/{action=Index}/{id?}");

app.Run();
