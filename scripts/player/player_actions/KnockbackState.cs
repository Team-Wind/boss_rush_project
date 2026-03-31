using Godot;
using System;

public partial class KnockbackState : State
{
	[Export] CharacterBody2D Character;
	[Export] Player Player;

	public float KnockbackDuration = 1.0f;

	// Called when the node enters the scene tree for the first time.
	public override void Enter()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void PhysicsUpdate(double delta)
	{
		

	}

	public override void HandleInput(InputEvent @event)
	{
	}

    public override void Exit()
    {
		Player.IsKnocked = false;
    }

}
