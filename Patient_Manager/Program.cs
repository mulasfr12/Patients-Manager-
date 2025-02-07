using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Patient_Manager.Data;
using Patient_Manager.IRepository;
using Patient_Manager.IRepository.Repositories;
using Patient_Manager.IRepository.Repositories.Patient_Manager.Repository;
using Patient_Manager.IRepository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Ensure Swagger is correctly registered
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patient Manager API", Version = "v1" });
});

// Register repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ICheckupRepository, CheckupRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IMedicalFileRepository, MedicalFileRepository>();


var app = builder.Build();

// ? Move Swagger OUTSIDE of the if block to ensure it's always enabled
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Manager API v1");
});

// ? Remove Swagger from here
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
