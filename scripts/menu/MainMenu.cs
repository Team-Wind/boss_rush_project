using Godot;
using System;
using System.IO;

public partial class MainMenu : CanvasLayer
{

	[Export] private string PlayScene = "res://scenes/test/world_test.tscn";
	[Export] private AnimationPlayer AnimationPlayer;
	[Export] private Control Cursor;
	[Export] private Control Settings;
	
	[ExportGroup("Main Menu Buttons")]
	[Export] private Button PlayButton;
	[Export] private Button SettingsButton;
	[Export] private Button QuitButton;
	
	[ExportGroup("Settings Buttons")]
	[Export] private Button SettingsBackButton;
	[Export] private Button SettingsControlsButton;
	[Export] private Button SettingsAudioButton;
	[Export] private Button SettingsLanguageButton;

	[ExportGroup("Settings Options")]
	[Export] private SettingsMenu ControlsMenu;
	[Export] private SettingsMenu AudioMenu;
	[Export] private SettingsMenu LanguageMenu;

	public override void _Ready() 
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;

		RefreshUI();

		GD.Print("Locale: " + TranslationServer.GetLocale());
		GD.Print("MENU_PLAY: " + Tr("MENU_PLAY"));
		GD.Print("Loaded locales: ");
		foreach (var locale in TranslationServer.GetLoadedLocales())
			GD.Print(locale);

		DisableControl(Settings);
		DisableControl(ControlsMenu);
		DisableControl(AudioMenu);
		DisableControl(LanguageMenu);

		// Conectar os sinais
		AnimationPlayer.AnimationFinished += _on_animation_finished;
		PlayButton.Pressed += () => GetTree().ChangeSceneToFile(PlayScene);
		SettingsButton.Pressed += () => { if (Settings.Visible == false) { AnimationPlayer.Play("Settings");} };
		QuitButton.Pressed += () => GetTree().Quit();
		SettingsBackButton.Pressed += () => AnimationPlayer.Play("SettingsBack");
		SettingsControlsButton.Pressed += () => AnimationPlayer.Play("ControlsMenu");
		SettingsAudioButton.Pressed += () => AnimationPlayer.Play("AudioMenu");
		SettingsLanguageButton.Pressed += () => AnimationPlayer.Play("LanguageMenu");
		ControlsMenu.Back += () => AnimationPlayer.Play("ControlsMenuBack");
		AudioMenu.Back += () => AnimationPlayer.Play("AudioMenuBack");
		LanguageMenu.Back += () => AnimationPlayer.Play("LanguageMenuBack");
	}

	public override void _Process(double delta)
	{
		Cursor.GlobalPosition = GetViewport().GetMousePosition();
	}

	private void _on_animation_finished(StringName animName)
	{
		if (animName == "Settings")
			EnableControl(Settings);
		else if (animName == "SettingsBack")
			DisableControl(Settings);
		else if (animName == "ControlsMenu")
			EnableControl(ControlsMenu);
		else if (animName == "ControlsMenuBack")
			DisableControl(ControlsMenu);
		else if (animName == "AudioMenu")
			EnableControl(AudioMenu);
		else if (animName == "AudioMenuBack")
			DisableControl(AudioMenu);
		else if (animName == "LanguageMenu")
			EnableControl(LanguageMenu);
		else if (animName == "LanguageMenuBack")
			DisableControl(LanguageMenu);
	}

	private void EnableControl(Control control)
	{
		control.Visible = true;
		control.ProcessMode = ProcessModeEnum.Inherit;
		control.MouseFilter = Control.MouseFilterEnum.Stop;
	}
	private void DisableControl(Control control)
	{
		control.Visible = false;
		control.ProcessMode = ProcessModeEnum.Disabled;
		control.MouseFilter = Control.MouseFilterEnum.Ignore;
	}

	private void RefreshUI()
	{
		PlayButton.Text = Tr("MENU_PLAY");
		SettingsButton.Text = Tr("MENU_SETTINGS");
		QuitButton.Text = Tr("MENU_QUIT");
		SettingsControlsButton.Text = Tr("MENU_CONTROLS");
		SettingsAudioButton.Text = Tr("MENU_AUDIO");
		SettingsLanguageButton.Text = Tr("MENU_LANGUAGE");
	}
}
