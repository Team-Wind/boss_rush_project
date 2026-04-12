using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Signal] public delegate void BackEventHandler();
	[Export] private Button BackButton;

	public override void _Ready() 
	{
		BackButton.Pressed += () => EmitSignal(SignalName.Back);
	}
}
