using AATAPI.Data;
using AATAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//***IMPORTANT INSTRUCTION HERE - MUST CONFIGURE CONNECTION BEFORE RUNNING MIGRATIONS
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AATConnection"))
);

// Register your repository implementation.
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IRegistrationsRepository, RegistrationsRepository>();
builder.Services.AddHttpContextAccessor(); // Already added as a scoped service here.
// In Startup.cs or Program.cs



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7099") // Update to use the secure origin of your Blazor WebAssembly app
    .AllowAnyMethod()
    .AllowAnyHeader() // Allow any headers, including ContentType
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
