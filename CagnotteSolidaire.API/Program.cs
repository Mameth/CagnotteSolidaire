using CagnotteSolidaire.Domain.Commands.Utilisateurs;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Infrastructure.Persistence;
using CagnotteSolidaire.Infrastructure.Repositories;
using CagnotteSolidaire.Domain.Services;
using CagnotteSolidaire.Infrastructure.Services;


using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//
// Controllers
//
builder.Services.AddControllers();

//
// Swagger (utile pour tests API)
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// DbContext EF Core
//
builder.Services.AddDbContext<CagnotteDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(">>> CONNECTION STRING = " + cs);

//
// Repositories (Domain -> Infrastructure)
//
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IAssociationRepository, AssociationRepository>();

//
// MediatR (Domain Commands / Queries)
//
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(InscrireParticipantCommand).Assembly)
);


builder.Services.AddHttpClient<IJoAssociationService, JoAssociationService>(client =>
{
    client.BaseAddress = new Uri(
        "https://journal-officiel-datadila.opendatasoft.com");
});

var app = builder.Build();

//
// Pipeline HTTP
//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
