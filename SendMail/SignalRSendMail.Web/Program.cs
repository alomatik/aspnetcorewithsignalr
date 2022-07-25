using RabbitMQ.Client;
using SignalRSendMail.Web.Hubs;
using SignalRSendMail.Web.Services.RabbitMQServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
builder.Services.AddSingleton<IConnectionFactory>(serviceProvider => new ConnectionFactory() { Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")) });
builder.Services.AddSingleton(typeof(RabbitMQPublisher));

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod()
                                                                                                       .AllowAnyHeader()
                                                                                                       .AllowCredentials()
                                                                                                       .SetIsOriginAllowed(x => true)));


builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<MessageHub>("/messageHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
