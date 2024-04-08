using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
//            throw new InvalidOperationException("Sorry your Connection is not found"));

//});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (connectionString == null)
    {
        throw new InvalidOperationException("Sorry your Connection is not found");
    }

    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Server")); // Specify migrations assembly
});

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));

  


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
