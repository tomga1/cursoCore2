using cursoCore2API;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;
using FluentValidation;
using cursoCore2API.DTOs;
using cursoCore2API.Validators;
using cursoCore2API.Services;
using Microsoft.Extensions.DependencyInjection;
using cursoCore2API.Repository;
using cursoCore2API.Models;
using cursoCore2API.AutoMappers;
using cursoCore2API.Repository.IRepository;
using cursoCore2API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddTransient<UserRepository>();
//builder.Services.AddKeyedScoped<ICommonService<ProductoDto, ProductoInsertDto, ProductoUpdateDto>, ProductoService>("productoService");


//builder.Services.AddScoped<IRepository<Producto>, ProductoRepository>();
builder.Services.AddScoped<IproductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("cursoCore2API", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Aca pegar el token generado"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "cursoCore2API"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(p => p.AddPolicy("PoliticaCors", build =>
{
    build.WithOrigins("https://localhost:7243").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
    );


 
////Validators

//builder.Services.AddScoped<IValidator<ProductoInsertDto>, ProductoInsertValidator>();
//builder.Services.AddScoped<IValidator<ProductoUpdateDto>, ProductoUpdateValidator>();


//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Configuramos el Authentication




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Soporte para CORS
app.UseCors("PoliticaCors");

//Soporte para autenticacion
app.UseAuthentication
    (
        x => 
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true; 
        x.TokenValidationParameters = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)
    })



app.UseAuthorization();

app.MapControllers();

app.Run();
