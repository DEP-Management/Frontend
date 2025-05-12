using Blazored.LocalStorage;
using Blazored.Modal;
using DEP_Blazor_WASM;
using DEP_Blazor_WASM.Handlers;
using DEP_Blazor_WASM.Services;
using DEP_Blazor_WASM.Services.Excel;
using DEP_Blazor_WASM.Services.Interfaces;
using DEP_Blazor_WASM.Services.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register Blazored nuget services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();

// MudSnackbar
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3500;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

// Blazor Bootstrap
builder.Services.AddBlazorBootstrap();

// MudBlazor
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<AuthenticationHandler>();

// Configure HttpClient with the base address from configuration
builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{builder.Configuration["ServerUrl"]}/api/"))
                .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IExcelFromModule, ExcelFromModule>();
builder.Services.AddScoped<IExcelFromBoss, ExcelFromBoss>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileTagService, FileTagService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonCourseService, PersonCourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IObjectComparisonService, ObjectComparisonService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddSingleton<UserSessionService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());


await builder.Build().RunAsync();
