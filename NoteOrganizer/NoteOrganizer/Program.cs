using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NoteOrganizer.Core.DTO;
using NoteOrganizer.Core.Interface;
using NoteOrganizer.Core.Services;
using NoteOrganizer.Core.Utilities;
using NoteOrganizer.DataAccess;
using NoteOrganizer.DataAccess.Repository;
using NoteOrganizer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration config = builder.Configuration; 
builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddSwaggerConfiguration();
builder.Services.AddTransient<IValidator<UserDto>, UserObjectValidator>();
builder.Services.AddDbContext<NoteOrganizerDbContext>(options => options.UseSqlServer(config.GetConnectionString
    ("DefaultConnection")));
//builder.Services.AddAuthenticationExtension(config);
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthenticationExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
