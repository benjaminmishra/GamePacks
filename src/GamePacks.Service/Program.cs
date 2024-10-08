using GamePacks.DataAccess;
using GamePacks.Service.Endpoints;
using GamePacks.Service.UseCases.Commands;
using GamePacks.Service.UseCases.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDataAccessServices(
    options => builder.Configuration.GetSection(GamePacksDatabaseOptions.ConfigSection).Bind(options));

builder.Services.AddScoped<CreatePackCommandHandler>();
builder.Services.AddScoped<GetPackByIdQueryHandler>();
builder.Services.AddScoped<GetAllPacksQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// add endpoints
app.RegisterV1Endpoints();

await app.RunAsync();