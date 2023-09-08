using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PinjamanOnline.Data;
using PinjamanOnline.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(
//	c =>
//	{
//		c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//		{
//			In = ParameterLocation.Header,
//			Description = "Entered Token",
//			Name = "Authorization",
//			Type = SecuritySchemeType.Http,
//			BearerFormat = "JWT",
//			Scheme = "bearer"
//		});
//		c.AddSecurityRequirement(new OpenApiSecurityRequirement
//		{
//			{
//				new OpenApiSecurityScheme
//				{
//					Reference = new OpenApiReference
//					{
//						Type = ReferenceType.SecurityScheme,
//						Id = "Bearer"
//					}
//				},
//				new string[]{}
//			}
//		});
//	});

//DbContext
builder.Services.AddDbContext<LoanDbContext>(
	o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<UserDbContext>(
	o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddHostedService<LoanExpirationCheck>();

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
