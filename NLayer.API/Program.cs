using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IUnitOfWork aray�z�n� g�rd��u zaman UnitOfWorktan nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IGenericRepository aray�z�n� g�rd��u zaman GenericRepository tan nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Burada appsettings.json dosyas�nda belirledi�imiz connection adresini aliyoruz. Ve daha sonra appdbcontextimizin nerede olu�unu ac�kca bildiriyoruz.
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
