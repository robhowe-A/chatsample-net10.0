using System;
using ChatSample.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging(options => 
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);
builder.Services.AddW3CLogging(options =>
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.W3CLoggingFields.All);

builder.Services.AddSignalR();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Console
    .WriteLine(
        "{0}: {1}\n"+
        "{2}: {3}\n"+ 
        "|\t{4, -20}"+
        "|\t{5, -20}\t|",
        "ContentRoot Path", builder.Environment.ContentRootPath,
        "WebRootPath", builder.Environment.WebRootPath,
        "Application Name", "Environment Name");
Console
    .WriteLine(
        $"|\t{builder.Environment.ApplicationName, -20}"+
        $"|\t{builder.Environment.EnvironmentName, -20}\t|");

app.UseHttpLogging();
app.UseW3CLogging();

app.UseFileServer();

app.UseHttpsRedirection();
app.UseRouting();

app.MapHub<ChatHub>("/chat");


app.Run();