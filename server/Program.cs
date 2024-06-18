using server.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowLocalOrigins");
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
