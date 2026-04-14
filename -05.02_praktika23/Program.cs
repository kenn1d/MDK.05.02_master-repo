using praktika22.Data.DataBase;
using praktika22.Data.Interfaces;
using praktika22.Data.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICategorys, DBCategory>();
builder.Services.AddTransient<IItems, DBItems>();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.Run();