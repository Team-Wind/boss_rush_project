using Godot;
using System;

public partial class WorldTest : Node2D
{
	[Export] Player Player;
	[Export] Label HPLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player.CurrentHP = Player.HitPoints;
		if (HPLabel != null) HPLabel.Text = "HP: " + Player.CurrentHP;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (HPLabel != null) HPLabel.Text = "HP: " + Player.CurrentHP;
	}
}
