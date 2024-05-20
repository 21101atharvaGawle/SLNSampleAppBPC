using SampleAppBPC.Middleware;
using SampleAppBPC.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
// Add services to the container.
builder.Services.AddScoped<ISampleService, SampleService>();


builder.Services.AddControllers();
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
app.UseGlobalExceptionHandlercs();

app.MapControllers();

app.Run();
