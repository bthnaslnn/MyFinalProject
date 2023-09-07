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
//builder.Services.AddSingleton<IProductService, ProductManager>(); // webAPI üzerinde tanýmlanabilmeleri için yazýldý.
//builder.Services.AddSingleton<IProductDal, EfProductDal>();       // webAPI üzerinde tanýmlanabilmeleri için yazýldý.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autofac kurulum kodlarý
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
