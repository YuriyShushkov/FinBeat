using FinBeat.Application.DTOs;
using FinBeat.Application.Services;
using FinBeat.Application.Validators;
using FinBeat.Domain;
using FinBeat.Domain.Interfaces;
using FinBeat.Infrastructure.Persistence;
using FinBeat.WebService.Api.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataRecordRepository, DataRecordRepository>();
builder.Services.AddScoped<IDataRecordService, DataRecordService>();

builder.Services.AddScoped<IApiLogRepository, ApiLogRepository>();
builder.Services.AddScoped<DataRecordService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("FinBeat.Infrastructure")));

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<DataRecordValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<ApiLoggingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});



app.Run();