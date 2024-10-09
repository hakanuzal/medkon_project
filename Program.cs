using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MedkonTestProject.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB bağlantısı ayarı
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var mongoSettings = builder.Configuration.GetSection("MongoDB");
    var connectionString = mongoSettings.GetValue<string>("ConnectionString");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException("MongoDb bağlantı dizesi boş olamaz.");
    }

    return new MongoClient(connectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase("MedkonTest");
    return database;
});

// MVC denetleyicileri ve görünüm desteği ekleyin
builder.Services.AddControllersWithViews();

// Servislerin eklenmesi
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILogService, LogService>();

var app = builder.Build();

// Middleware ayarları
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Hata sayfası
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Yetkilendirme middleware'ini ekleyin
app.UseAuthorization();

// Varsayılan rotayı ayarlayın
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
