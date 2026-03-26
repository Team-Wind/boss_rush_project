using Godot;
using System;

public partial class FallState : State
{
	[Export] CharacterBody2D Character;
	[Export] Player Player;

	//variaveis de classe
	protected const float WalkSpeed = 150.0f;
	protected const float FallSpeed = -300.0f;
	protected const float Gravity = 800.0f;


	// Called when the node enters the scene tree for the first time.
	public override void Enter()
	{
		//Player.AnimationPlayer("aindasoipdna");

		var vel = Character.Velocity;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void PhysicsUpdate(double delta)
	{
	}

    public override void HandleInput(InputEvent @event)
    {
      
    }

    public override void Exit(){}

}
