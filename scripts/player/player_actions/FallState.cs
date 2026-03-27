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

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void PhysicsUpdate(double delta)
	{
		var vel = Character.Velocity;
		vel.Y += Gravity * (float)delta;

		var direction = Input.GetAxis("MoveLeft","MoveRight");
		vel.X = direction * WalkSpeed;

		Character.Velocity = vel;
		Character.MoveAndSlide();

		if (Character.IsOnFloor())
		{
			if (direction == 0)
			{
				StateMachine.ChangeState("IdleState");
			}
			else
			{
				StateMachine.ChangeState("WalkState");
			}
		}

	}

    public override void HandleInput(InputEvent @event)
    {

		if (Input.IsActionJustPressed("Jump") && Player.CanDoubleJump)
		{
			Player.CanDoubleJump = false;
			StateMachine.ChangeState("JumpState");
		}

		var dir_dash = Input.GetAxis("MoveLeft","MoveRight");
		if (Input.IsActionJustPressed("Dash") && !Player.Dashing && Player.DashTimer <= 0.0f)
		{
			StateMachine.ChangeState("DashState");			
		}
      
    }

    public override void Exit(){}

}
