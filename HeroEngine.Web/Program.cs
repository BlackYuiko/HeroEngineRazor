var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// --- INITIALIZE FILE PATHS AND LOAD DATA ---
// This guarantees the files are saved in "HeroEngine.Web/Data"
string webRootDataFolder = Path.Combine(builder.Environment.ContentRootPath, "Data");
HeroEngine.Core.Data.PathConfig.DataFolderPath = webRootDataFolder;

// Load heroes from JSON into memory on startup (Requirement 6.2)
HeroEngine.Core.Managers.HeroManager.Initialize();
HeroEngine.Core.Managers.AbilityManager.Initialize();

// -------------------------------------------

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
