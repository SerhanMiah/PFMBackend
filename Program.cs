using Microsoft.EntityFrameworkCore;
using PFMBackend.Data;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration; 

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // It's useful to have this middleware in development to see detailed error messages
    app.UseSwagger();
    app.UseSwaggerUI();
}

// comment it out for now
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
