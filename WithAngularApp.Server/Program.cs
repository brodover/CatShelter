using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WithAngularApp.Server.Config;
using WithAngularApp.Server.Data;
using WithAngularApp.Server.Hubs;
using WithAngularApp.Server.Services;

DataClient.ParseData();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfig(builder.Configuration);

builder.Services.AddSingleton<DbService>();

// auth
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.Authority = $"{builder.Configuration["Auth0:Domain"]}";
	options.Audience = $"{builder.Configuration["Auth0:Audience"]}";
});

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(
		options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		Description = "JWT Authorization header using the Bearer scheme."
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
			},
			new string[] {}
		}
	});
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("hub");

app.MapFallbackToFile("/index.html");

app.Run();
