
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var productos = new List<Productos>();


app.MapGet("/productos", () =>
{
    return productos;
});



app.MapGet("/productos/{id}", (int id) =>
{
    var producto = productos.FirstOrDefault(x => x.Id == id);
    return producto;
});



app.MapPost("/productos", (Productos Productos) =>
{
    productos.Add(Productos);
    return Results.Ok();
});

app.MapPut("/productos/{id}", (int id, Productos producto) =>
{
    var existingProductos = productos.FirstOrDefault(x => x.Id == id);
    if (existingProductos != null)
    {
        existingProductos.Name = producto.Name;
        existingProductos.Descripcion = producto.Descripcion;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/productos/{id}", (int id) =>
{
    var existingProductos = productos.FirstOrDefault(x => x.Id == id);
    if (existingProductos != null)
    {
        productos.Remove(existingProductos);
        return Results.NotFound();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

internal class Productos
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Descripcion { get; set; }
}