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

// IUnitOfWork arayüzünü gördüðu zaman UnitOfWorktan nesne oluþturacaðýný anlayacak ve burada nesnenin yaþam süresini belirlemiþ olduk.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IGenericRepository arayüzünü gördüðu zaman GenericRepository tan nesne oluþturacaðýný anlayacak ve burada nesnenin yaþam süresini belirlemiþ olduk.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Burada appsettings.json dosyasýnda belirlediðimiz connection adresini aliyoruz. Ve daha sonra appdbcontextimizin nerede oluðunu acýkca bildiriyoruz.
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
