using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AOP --Autofac
//Autofac, Ninject, CastleWindsor, StructrureMap, LightInject, DryInject --> IoC Container
builder.Services.AddControllers();
//builder.Services.AddSingleton<IProductService, ProductManager>(); // webAPI �zerinde tan�mlanabilmeleri i�in yaz�ld�.
//builder.Services.AddSingleton<IProductDal, EfProductDal>();       // webAPI �zerinde tan�mlanabilmeleri i�in yaz�ld�.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autofac kurulum kodlar�
builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });
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
