using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using PrimeVaultApi.Db;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


var databaseName = new MySqlConnectionStringBuilder(connectionString).Database;


var masterConnectionString = new MySqlConnectionStringBuilder(connectionString)
{
    Database = ""
}.ConnectionString;

    
using (var connection = new MySqlConnection(masterConnectionString))
{
    connection.Open();

    using var cmd = connection.CreateCommand();
    cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
    cmd.ExecuteNonQuery();
}

builder.Services.AddDbContext<AppDbContext>(opcoes =>
{
    opcoes.UseMySQL(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:30000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen(opcoes =>
{
    opcoes.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PrimeVault",
        Version = "v1",
        Description = "An API for managing accounts"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opcoes =>
    {
        opcoes.SwaggerEndpoint("/swagger/v1/swagger.json", "PrimeVault API v1");
        opcoes.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "");

app.Run();
