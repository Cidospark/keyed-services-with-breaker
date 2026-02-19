using KeyedServicesWithBreaker.Models;
using KeyedServicesWithBreaker.Services;
using KeyedServicesWithBreaker.Services.Provider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<PaystackPaymentService>("paystack", c =>
{
    c.BaseAddress = new Uri("https://api.paystack.co");
});

builder.Services.AddHttpClient<FlutterwavePaymentService>("flutterwave", c =>
{
    c.BaseAddress = new Uri("https://api.flutterwave.com");
});

builder.Services.Configure<PaymentProviderOptions>(builder.Configuration.GetSection("PaymentProviders"));
builder.Services.AddKeyedScoped<IPaymentService>("paystack", (sp, key) =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var http = factory.CreateClient("paystack");
    return new PaystackPaymentService(http);
});

builder.Services.AddKeyedScoped<IPaymentService>("flutterwave", (sp, key) =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var http = factory.CreateClient("flutterwave");
    return new FlutterwavePaymentService(http);
});
builder.Services.AddScoped<PaymentProviderSelector>();
builder.Services.AddScoped<PaymentProcessorService>();
builder.Services.AddScoped<ProcessPaymentCommandHandler>();
builder.Services.AddTransient<IProviderPolicyRegistry, ProviderPolicyRegistry>();
builder.Services.AddScoped<Func<string, IPaymentService>>(
    sp => key => sp.GetRequiredKeyedService<IPaymentService>(key) // resolver delegate
);
builder.Services.AddSingleton<IPaymentExecutionEngine, PaymentExecutionEngine>();
builder.Services.AddSingleton<IProviderMetricsRegistry, ProviderMetricsRegistry>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
