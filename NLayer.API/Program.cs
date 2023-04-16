using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;

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

// IService aray�z�n� g�rd��u zaman Service ten nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

// IProductRepository aray�z�n� g�rd��� zaman ProductRepository ten nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// IProductService aray�z�n� g�rd��� zaman ProductService ten nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped<IProductService, ProductService>();

// ICategoryRepository aray�z�n� g�rd��� zaman CategoryRepository ten nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// ICategoryService aray�z�n� g�rd��� zaman CategoryService ten nesne olu�turaca��n� anlayacak ve burada nesnenin ya�am s�resini belirlemi� olduk.
builder.Services.AddScoped<ICategoryService, CategoryService>();

// AutoMapper'� projemize dahil ettik.
builder.Services.AddAutoMapper(typeof(MapProfile));


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
