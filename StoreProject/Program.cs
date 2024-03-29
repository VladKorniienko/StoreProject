using StoreProject.DAL;
using StoreProject.BLL;
using StoreProject.ExceptionHandler;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//so-called "composition root"
builder.Services.ConfigureDAL(builder.Configuration);
builder.Services.ConfigureBLL();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
