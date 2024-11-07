using MovieXprt.Application;
using MovieXprt.Infrastructure.DataStore;
using MovieXprt.Infrastructure.TvMazeClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .ConfigureDataStore(builder.Configuration)
    .ConfigureTvMazeApi(builder.Configuration)
    .ConfigureApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();
