using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataBookStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register the OData Services through Dependency Injection
builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
// Add service with In-Memory Databas
builder.Services.AddDbContext<BookStoreContext>(option => option.UseInMemoryDatabase("BookLists"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseODataBatching();
//TODO: Test middleware
app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Book>("Books");
    builder.EntitySet<Press>("Presses");
    return builder.GetEdmModel();
}