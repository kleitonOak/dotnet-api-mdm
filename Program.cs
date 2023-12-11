using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var companySite = "https://myowncompany.com";
var responsibleSite = "Jose Kleiton";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "MDM Person API",
        Description = "Microservice responsible for manages data of Person",
        TermsOfService = new Uri(companySite),
        Contact = new OpenApiContact{
            Name = responsibleSite, 
            Url = new Uri(companySite+"/contact")
        },
        License = new OpenApiLicense{
            Name = responsibleSite,
            Url = new Uri(companySite+"/licence")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

//DATA BASE
var connectionString = builder.Configuration.GetConnectionString("PersonConnection");

builder.Services.AddDbContext<PersonContext>(opts => 
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// MAPPER
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

