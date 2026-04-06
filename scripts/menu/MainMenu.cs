using Godot;
using System;
using System.IO;

public partial class MainMenu : CanvasLayer
{

	[Export] private VBoxContainer buttonContainer;
	
	[ExportGroup("Main Menu Buttons")]
	[Export] private Button playButton;
	[Export] private Button optionsButton;
	[Export] private Button quitButton;

	[Export] private string playScene = "res://scenes/test/world_test.tscn";

	public override void _Ready() 
	{
		buttonContainer.Size = new Vector2(400, 400);
		playButton.ExpandIcon = true;
		optionsButton.ExpandIcon = true;
		quitButton.ExpandIcon = true;
		playButton.Text = "Play";
		optionsButton.Text = "Options";
		quitButton.Text = "Quit";

		// Conectar os sinais
		playButton.Pressed += _on_play_button_button_pressed;
		optionsButton.Pressed += _on_options_button_button_pressed;
		quitButton.Pressed += _on_quit_button_button_pressed;
	}

	private void _on_play_button_button_pressed() 
	{
		GetTree().ChangeSceneToFile(playScene);
	}

	private void _on_options_button_button_pressed() 
	{
		GD.Print("Options button pressed");
	}

	private void _on_quit_button_button_pressed() 
	{
		GetTree().Quit();
	}
}
