using MagicOnion;
using MagicOnion.Server;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMagicOnion();

var app = builder.Build();

app.MapMagicOnionService();

app.Run();
