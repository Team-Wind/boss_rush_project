using Godot;
using System;
using System.IO;

public partial class MainMenu : CanvasLayer
{

	[Export] private string PlayScene = "res://scenes/test/world_test.tscn";
	[Export] private AnimationPlayer AnimationPlayer;
	[Export] private Control Cursor;
	[Export] private Control Start;
	[Export] private Control Settings;
	[Export] private ColorRect FadeOut;
	
	[ExportGroup("Main Menu Buttons")]
	[Export] private Button StartButton;
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

	[ExportGroup("Start Options")]
	[Export] private Button StartBackButton;
	[Export] private Button NewGameButton;
	[Export] private Button ContinueButton;

	private bool NewGame;

	public override void _Ready() 
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		FadeOut.MouseFilter = ColorRect.MouseFilterEnum.Ignore;
		RefreshUI();

		DisableControl(Start);
		DisableControl(Settings);
		DisableControl(ControlsMenu);
		DisableControl(AudioMenu);
		DisableControl(LanguageMenu);

		// Conectar os sinais
		AnimationPlayer.AnimationFinished += _on_animation_finished;
		StartButton.Pressed += () => { if (Start.Visible == false) { AnimationPlayer.Play("StartMenu");} };
		SettingsButton.Pressed += () => { if (Settings.Visible == false) { AnimationPlayer.Play("Settings");} };
		QuitButton.Pressed += () => GetTree().Quit();
		SettingsBackButton.Pressed += () => AnimationPlayer.Play("SettingsBack");
		SettingsControlsButton.Pressed += () => AnimationPlayer.Play("ControlsMenu");
		SettingsAudioButton.Pressed += () => AnimationPlayer.Play("AudioMenu");
		SettingsLanguageButton.Pressed += () => AnimationPlayer.Play("LanguageMenu");
		ControlsMenu.Back += () => AnimationPlayer.Play("ControlsMenuBack");
		AudioMenu.Back += () => AnimationPlayer.Play("AudioMenuBack");
		LanguageMenu.Back += () => AnimationPlayer.Play("LanguageMenuBack");
		StartBackButton.Pressed += () => AnimationPlayer.Play("StartMenuBack");
		NewGameButton.Pressed += () => {AnimationPlayer.Play("Start"); NewGame = true;};
		ContinueButton.Pressed += () => {AnimationPlayer.Play("Start"); NewGame = false;};
	}

	public override void _Process(double delta)
	{
		Cursor.GlobalPosition = GetViewport().GetMousePosition();
	}

	private void _on_animation_finished(StringName animName)
	{
		if (animName == "Start")
			StartGame();
		else if (animName == "Settings")
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
		else if (animName == "StartMenu")
			EnableControl(Start);
		else if (animName == "StartMenuBack")
			DisableControl(Start);
	}

	private void StartGame(){//Demonstração
		if (NewGame)
		{
			GetTree().ChangeSceneToFile(PlayScene);
		}
		else
		{
			GetTree().ChangeSceneToFile(PlayScene);
		}
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
		StartButton.Text = Tr("MENU_START");
		SettingsButton.Text = Tr("MENU_SETTINGS");
		QuitButton.Text = Tr("MENU_QUIT");
		SettingsControlsButton.Text = Tr("MENU_CONTROLS");
		SettingsAudioButton.Text = Tr("MENU_AUDIO");
		SettingsLanguageButton.Text = Tr("MENU_LANGUAGE");
		NewGameButton.Text = Tr("START_NEW_GAME");
		ContinueButton.Text = Tr("START_CONTINUE");
	}
}
