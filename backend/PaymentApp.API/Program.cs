using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Application.Services;
using PaymentApp.Infrastructure.Data;
using PaymentApp.Infrastructure.Repositories;
using PaymentApp.Infrastructure.Workers;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 🔧 Configure Services
// -----------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 💾 EF Core - SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🧩 Dependency Injection
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


builder.Services.AddScoped<IAutoConfirmService, AutoConfirmService>();
builder.Services.AddHostedService<AutoConfirmWorker>();

builder.Services.AddScoped<IReportsService, ReportsService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



// -----------------------------
// 🚀 Build the App
// -----------------------------
var app = builder.Build();

// -----------------------------
// 🧱 Middleware Pipeline
// -----------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();



//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//    });
//});
//app.UseCors("AllowAll");



app.Run();
