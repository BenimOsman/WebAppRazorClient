using WebAppRazorClient;                                                                                    // Importing the client namespace for the web application
using WebAppRazorSandwitchClient;                                                                           // Importing the client namespace for Sandwich service

var builder = WebApplication.CreateBuilder(args);                                                           // Initialize the web application builder

// Add services to the container (Dependency Injection)
builder.Services.AddRazorPages();                                                                           // Adds support for Razor Pages (views and pages)
builder.Services.AddSession();                                                                              // Enable session management
builder.Services.AddHttpContextAccessor();                                                                  // Required to access the HttpContext in Razor Pages

// Register scoped services
builder.Services.AddScoped<SandwichService>();                                                              // Registers the SandwichService with a scoped lifetime
builder.Services.AddScoped<AuthService>();                                                                  // Registers the AuthService with a scoped lifetime

// Configure HttpClients for external API calls
builder.Services.AddHttpClient("SandwichAPI", client =>
{
    // Setting the base address for Sandwich API
    client.BaseAddress = new Uri("https://localhost:7281/");                                                
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,                     // Disable SSL validation for testing (insecure)
    UseCookies = true                                                                                       // Enables automatic cookie handling for the API client
});

// Configure HttpClient for Authentication API
builder.Services.AddHttpClient<AuthService>(client =>
{
    // Setting the base address for Auth API
    client.BaseAddress = new Uri("https://localhost:7256/api");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{ 
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,                     // Disable SSL validation for testing (insecure)
    UseCookies = true                                                                                       // Enables automatic cookie handling for the API client
});

var app = builder.Build();                                                                                  // Builds the application pipeline

// Configure middleware (Error handling and HTTPS redirection)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");                                                                      // Shows a generic error page in production environment
    app.UseHsts();                                                                                          // Enforces HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection();                                                                                  // Redirects HTTP requests to HTTPS
app.UseStaticFiles();                                                                                       // Serves static files (e.g., images, CSS, JS)
app.UseRouting();                                                                                           // Adds routing capabilities (mapping endpoints to controllers or pages)

// Enabes Sessions
app.UseSession(); 

// Adds the Authorization
app.UseAuthorization();                                                                                     

// Redirecting root (/) path to the Login page 
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")                                                                    // Check if the request is for the root path
    {
        context.Response.Redirect("/Account/Login");                                                    // Redirect to Login page
        return;  
    }

    await next();                                                                                       // Continue with the next middleware if not the root path
});

app.MapRazorPages();                                                                                    // Maps Razor Pages to their routes

app.Run();                                                                                              // Run the web application


// Instance is requested once per request or operation.