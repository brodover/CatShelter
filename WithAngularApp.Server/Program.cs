using WithAngularApp.Server.Data;
using WithAngularApp.Server.Database;
using WithAngularApp.Server.Hubs;
using WithAngularApp.Server.Services;

DataClient.ParseData();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(
		options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSignalR();

builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection("BookStoreDatabase"));

builder.Services.AddSingleton<DbService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("hub");

app.MapFallbackToFile("/index.html");

app.Run();
