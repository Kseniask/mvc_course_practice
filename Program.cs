using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Vidly.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
var connectionString = builder.Configuration.GetConnectionString("VidlyDbConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "285457030509514";
    options.AppSecret = "e9b0ce8a57d325264020346d50f594b6";
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddAutoMapper(typeof(Program));
var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    // Add more customization as needed
};
builder.Services.AddSingleton(jsonOptions);
// Add more customization as needed
// Configure camelCase in response


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
         name: "default",
         pattern: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = "" });
 });
app.Run();
