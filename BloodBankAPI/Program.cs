using MongoDB.Driver;
using BloodBankAPI.Models;
using BloodBankAPI.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    var client = new MongoClient(connectionString);
    return client.GetDatabase("BloodBankDatabase"); // Specify your database name here
});

    builder.Services.AddSingleton<IMongoCollection<Donor>>(sp =>
        sp.GetRequiredService<IMongoDatabase>().GetCollection<Donor>("Donors"));
    builder.Services.AddSingleton<IMongoCollection<Inventory>>(sp =>
        sp.GetRequiredService<IMongoDatabase>().GetCollection<Inventory>("Inventory"));
    builder.Services.AddSingleton<IMongoCollection<Donation>>(sp =>
        sp.GetRequiredService<IMongoDatabase>().GetCollection<Donation>("Donations"));
    builder.Services.AddSingleton<IMongoCollection<Request>>(sp =>
        sp.GetRequiredService<IMongoDatabase>().GetCollection<Request>("Requests"));
    builder.Services.AddSingleton<IMongoCollection<Recipient>>(sp =>
        sp.GetRequiredService<IMongoDatabase>().GetCollection<Recipient>("Recipients"));





builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IDonationService, DonationService>(); 
builder.Services.AddScoped<IBloodInventoryService, BloodInventoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IRecipientService, RecipientService>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();