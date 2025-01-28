using MongoDB.Driver;
using MongoDB.Bson;
using BloodBankAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// MongoDB Configuration
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = new MongoClient(builder.Configuration.GetConnectionString("MongoDb"));
    return client.GetDatabase("BloodBankDatabase"); // Specify your database name here
});

// Register Services for Dependency Injection
builder.Services.AddScoped<IBloodDonorService, BloodDonorService>();
builder.Services.AddScoped<IBloodInventoryService, BloodInventoryService>();

// Enable Swagger for API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
