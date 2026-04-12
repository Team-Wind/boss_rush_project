using Godot;

public partial class LanguageManager : Node
{
    private const string SavePath = "user://settings.cfg";
    private const string DefaultLocale = "en";

    public override void _Ready()
    {
        LoadLanguage();
    }

    public void SetLanguage(string locale)
    {
        TranslationServer.SetLocale(locale);
        SaveLanguage(locale);
        // Notify all menus to refresh
        GetTree().CallGroup("menus", "RefreshUI");
    }

    public string GetCurrentLanguage()
    {
        return TranslationServer.GetLocale();
    }

    private void SaveLanguage(string locale)
    {
        var config = new ConfigFile();
        config.Load(SavePath); // load existing settings first
        config.SetValue("language", "locale", locale);
        config.Save(SavePath);
    }

    private void LoadLanguage()
    {
        var config = new ConfigFile();
        if (config.Load(SavePath) != Error.Ok)
        {
            TranslationServer.SetLocale(DefaultLocale);
            return;
        }

        string locale = (string)config.GetValue("language", "locale", DefaultLocale);
        TranslationServer.SetLocale(locale);
    }
    
}