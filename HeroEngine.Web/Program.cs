var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

string webRootDataFolder = Path.Combine(builder.Environment.ContentRootPath, "Data");
HeroEngine.Core.Data.PathConfig.DataFolderPath = webRootDataFolder;

HeroEngine.Core.Managers.HeroManager.Initialize();
HeroEngine.Core.Managers.AbilityManager.Initialize();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
