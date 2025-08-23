// using Microsoft.OpenApi.Models; //Optional, but needed if you customize Swagger options.
using FootballPlayers.API;
using FootballPlayers.API.Endpoints;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()    // allow any frontend (for testing)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

// app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FootballPlayersAPI v1");
});

app.MapPlayersEndpoints();
app.MapTeamsEndpoints();
app.MapPositionsEndpoints();
app.Run();
