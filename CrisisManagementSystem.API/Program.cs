using CrisisManagementSystem.API.Application.Configurations;
using CrisisManagementSystem.API.DataLayer;
using CrisisManagementSystem.API.Application.IRepository;
using CrisisManagementSystem.API.Application.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("CrisisManagementDatabaseConnection");
builder.Services.AddDbContext<CrisisManagementDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{   //builder coming from builder AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    options.AddPolicy("AllowAll", 
                      b => b.AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration ));

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository,UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//what request coming , how long it took ex-http request
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
