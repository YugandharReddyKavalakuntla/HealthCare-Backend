// using Ocelot.DependencyInjection;
// using Ocelot.Middleware;

// var builder = WebApplication.CreateBuilder(args);

// builder.Configuration
//     .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// // builder.Services.AddOcelot();
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAngular",
//         policy =>
//         {
//             policy
//                 .AllowAnyOrigin()
//                 .AllowAnyHeader()
//                 .AllowAnyMethod();
//         });
// });

// var app = builder.Build();

// // await app.UseOcelot();
// app.UseCors("AllowAngular");

// app.Run();


using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile(
        "ocelot.json",
        optional: false,
        reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("AllowAngular");

await app.UseOcelot();

app.Run();