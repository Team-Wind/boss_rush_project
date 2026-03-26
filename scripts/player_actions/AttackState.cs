using Godot;
using System;

public partial class AttackState : State
{
	[Export] public CharacterBody2D Character;
	[Export] public Player Player;
 	public override void Enter()
	{
		//Player.AnimationPlayer("insira nome do sprite aqui");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
