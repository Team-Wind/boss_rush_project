using Godot;
using System;

public partial class WorldTest : Node2D
{
	[Export] Player Player;
	[Export] Boss1 Boss1;
	[Export] Label HPLabel;
	[Export] Label BossLabel;
 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (HPLabel != null) HPLabel.Text = "HP: " + Player.CurrentHP;
		if (BossLabel != null) BossLabel.Text = "Boss HP: " + Boss1.CurrentHealth + "/ Boss Stagger: " + Boss1.CurrentStaggerCounter;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (HPLabel != null) HPLabel.Text = "HP: " + Player.CurrentHP;
		if (BossLabel != null) BossLabel.Text = "Boss HP: " + Boss1.CurrentHealth;
	}
}
