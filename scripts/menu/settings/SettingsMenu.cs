using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Export] private Button backButton;

	public override void _Ready() 
	{
		backButton.Pressed += _on_back_button_pressed;
	}

	private void _on_back_button_pressed() 
	{
		this.Visible = false;
		this.ProcessMode = ProcessModeEnum.Disabled;
		this.MouseFilter = Control.MouseFilterEnum.Ignore;
	}
}
