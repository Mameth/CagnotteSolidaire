using CagnotteSolidaire.Domain.Commands.Utilisateurs;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Infrastructure.Persistence;
using CagnotteSolidaire.Infrastructure.Repositories;
using CagnotteSolidaire.Domain.Services;
using CagnotteSolidaire.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CagnotteSolidaire API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entrez 'Bearer' [espace] et votre token. Exemple: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("http://localhost:5087", "https://localhost:5087")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<CagnotteDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(">>> CONNECTION STRING = " + cs);

builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IAssociationRepository, AssociationRepository>();
builder.Services.AddScoped<ICagnotteRepository, CagnotteRepository>();
builder.Services.AddScoped<IEmailService, ConsoleEmailService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = builder.Configuration["Jwt:Key"] ?? "CleDeSecoursPourDevLocalUniquement123456";

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(InscrireParticipantCommand).Assembly)
);

builder.Services.AddHttpClient<IJoAssociationService, JoAssociationService>(client =>
{
    client.BaseAddress = new Uri("https://journal-officiel-datadila.opendatasoft.com");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // On récupère ton DbContext
        var context = services.GetRequiredService<CagnotteSolidaire.Infrastructure.Persistence.CagnotteDbContext>();
        
        // 1. On supprime l'ancienne base qui bug (si elle existe)
        context.Database.EnsureDeleted();
        
        // 2. On recrée la base PROPRE avec les colonnes MotDePasse, Description, etc.
        context.Database.EnsureCreated();
        
        Console.WriteLine(">>> BASE DE DONNÉES RECRÉÉE AVEC SUCCÈS ! <<<");
    }
    catch (Exception ex)
    {
        Console.WriteLine(">>> ERREUR BDD : " + ex.Message);
    }
}

app.Run();