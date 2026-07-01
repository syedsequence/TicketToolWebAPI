using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Ticketing.Application;
using Ticketing.Application.AuthModel;
using Ticketing.Infrastructure;
using Ticketing.Infrastructure.DbContext;
using Ticketing.Infrastructure.FetchData;

var builder = WebApplication.CreateBuilder(args);

#region Service and Repository Registration

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService();

#endregion


#region DataBase Connection

builder.Services.AddDbContext<TicketingDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<TicketingUser, IdentityRole>(options =>
{


}).AddEntityFrameworkStores<TicketingDbContext>().AddDefaultTokenProviders();

#endregion


#region Add CORS

builder.Services.AddCors(options =>
{
	options.AddPolicy("TicketingAPIPolicy", corspolicy =>
	{
		corspolicy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

#endregion




builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region JWT Authentication

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		ValidateIssuer = true,
		ValidateAudience = true,
		ClockSkew = TimeSpan.Zero,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

#endregion

#region Swagger Authentication

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "FoodReceipe API version 1",
		Description = "Developed by sequencelogic",
		Version = "v1.0"
	});




	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		Description = @"Jwt Authorization header using the Bearer Scheme.
                        Enter 'Bearer' [space] and then your token in the input below.
                        Example:'Bearer 12345abcdef'",
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{

				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id ="Bearer"
				},
				Scheme ="Oauth2",
				Name="Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});

#endregion

#region Configuration for Seeding Data to Database

static async void UpdateDatabaseAsync(IHost host)
{
	using(var scope = host.Services.CreateScope())
	{
		var services = scope.ServiceProvider;

		try
		{
			var context = services.GetRequiredService<TicketingDbContext>();

			if(context.Database.IsSqlServer())
			{
				context.Database.Migrate();
			}

			await SeedData.SeedAllData(context);
		}
		catch(Exception ex)
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

			logger.LogError(ex, "An error occurred while migrating or seeding the database");
		}
	}
}

#endregion


var app = builder.Build();

#region Update Data and Roles

UpdateDatabaseAsync(app);

var serviceProvider = app.Services;

await SeedData.SeedRoles(serviceProvider);

#endregion

if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

}
app.UseCors("TicketingAPIPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
