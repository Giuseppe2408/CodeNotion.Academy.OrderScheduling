using System.Reflection;
using CodeNotion.Academy.OrderScheduling.Configurations;
using CodeNotion.Academy.OrderScheduling.Cqrs.Decorators;
using CodeNotion.Academy.OrderScheduling.Data;
using MediatR;
using Timer = CodeNotion.Academy.OrderScheduling.Cqrs.Decorators.Timer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//add dependency injection
builder.Services.AddTransient<Timer>();
builder.Services.AddDbContext<OrderDbContext>();
builder.Services.AddSwagger();
builder.Services.AddNSwag();

//add mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExecutionTimerDecorator<,>));
var app = builder.Build();

app.UseCors(b => b
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod());
// Configure the HTTP request pipeline.
app.UseApplicationSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();