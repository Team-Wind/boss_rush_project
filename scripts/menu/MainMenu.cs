using Godot;
using System;
using System.IO;

public partial class MainMenu : CanvasLayer
{

	[Export] private VBoxContainer buttonContainer;
	[Export] private Control Settings;
	
	[ExportGroup("Main Menu Buttons")]
	[Export] private Button playButton;
	[Export] private Button optionsButton;
	[Export] private Button quitButton;
	
	[ExportGroup("Settings Buttons")]
	[Export] private Button settingsBackButton;

	[Export] private string playScene = "res://scenes/test/world_test.tscn";

	public override void _Ready() 
	{
		DisableSettings();
		playButton.Text = "Start";
		optionsButton.Text = "Settings";
		quitButton.Text = "Quit";

		// Conectar os sinais
		playButton.Pressed += _on_play_button_button_pressed;
		optionsButton.Pressed += _on_settings_button_button_pressed;
		quitButton.Pressed += _on_quit_button_button_pressed;
		settingsBackButton.Pressed += _on_settings_back_button_pressed;
	}

	private void _on_play_button_button_pressed() 
	{
		GetTree().ChangeSceneToFile(playScene);
	}

	private void _on_settings_button_button_pressed() 
	{
		Settings.Visible = true;
		Settings.ProcessMode = ProcessModeEnum.Inherit;
		Settings.MouseFilter = Control.MouseFilterEnum.Stop;
	}

	private void _on_quit_button_button_pressed() 
	{
		GetTree().Quit();
	}

	private void _on_settings_back_button_pressed() 
	{
		DisableSettings();
	}

	private void DisableSettings() 
	{
		Settings.Visible = false;
		Settings.ProcessMode = ProcessModeEnum.Disabled;
		Settings.MouseFilter = Control.MouseFilterEnum.Ignore;
	}
}
