using Godot;
using System;

public partial class JumpState : State
{	
	[Export] Player Player;

	//variaveis de classe
	protected const float WalkSpeed = 150.0f;
	protected const float JumpSpeed = -300.0f;
	protected const float Gravity = 800.0f;

	public override void Enter()
	{
		//Player.AnimationPlayer("abuble");
		var vel = Player.Velocity;
		vel.Y = JumpSpeed;
		Player.Velocity = vel;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void PhysicsUpdate(double delta)
	{
		var vel = Player.Velocity;
		vel.Y += Gravity * (float)delta;

		if (vel.Y > 0)
		{
			StateMachine.ChangeState("FallState");
		}

		var direction = Input.GetAxis("MoveLeft","MoveRight");
		vel.X = direction * WalkSpeed;
		Player.Velocity = vel;
		Player.MoveAndSlide();

		if (Player.IsOnFloor())
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
		//verificação de double jump 
		if (Input.IsActionJustPressed("Jump") && (Player.CanDoubleJump))
		{
		  Player.CanDoubleJump = false;
		  StateMachine.ChangeState("JumpState");
		}

		//verificação de dash
		var dir_dash = Input.GetAxis("MoveLeft","MoveRight");
		if (Input.IsActionPressed("Dash") && dir_dash != 0 && !Player.Dashing && Player.DashTimer <= 0.0f)
		{
			StateMachine.ChangeState("DashState");
		}

	}

	 public override void Exit()
    {
    }
}
