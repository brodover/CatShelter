using WithAngularApp.Server.Config;
using WithAngularApp.Server.Data;
using WithAngularApp.Server.Hubs;
using WithAngularApp.Server.Services;

DataClient.ParseData();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(
		options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSignalR();

builder.Services.AddConfig(builder.Configuration);

builder.Services.AddSingleton<DbService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddAzureWebAppDiagnostics();
if (builder.Environment.IsDevelopment())
	builder.Logging.SetMinimumLevel(LogLevel.Debug);
else
	builder.Logging.SetMinimumLevel(LogLevel.Error);

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
