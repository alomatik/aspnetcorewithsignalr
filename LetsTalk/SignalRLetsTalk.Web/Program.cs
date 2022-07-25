using SignalRLetsTalk.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

//builder.Services.AddCors(corsOptions =>
//{
//    corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
//    {
//        corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(orign => true);
//    });
//});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseCors();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
