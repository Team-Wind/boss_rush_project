using Godot;
using System;

public partial class AttackState : State
{
	[Export] public Player Player;
	protected const float WalkSpeed = 150.0f;

	public override void Enter()
	{
		Player.SetAnimation("AttackTest");

		Player.IsAttacking = true;

		//if(Player.SwordCollider)
		Player.SwordOnBodyEntered(Player.BossRef);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void PhysicsUpdate(double delta)
	{
		var vel = Player.Velocity;
		var direction = Input.GetAxis("MoveLeft","MoveRight"); //verificação de direção
		vel.X = direction * WalkSpeed;
		Player.Velocity = vel;
		Player.MoveAndSlide();
		if (direction == 0)
        {
            StateMachine.ChangeState("IdleState"); //se não houver movimento, vota pro estado idle (tb nao tem input, por isso está aqui)
        }
	}

	public override void HandleInput(InputEvent @event)
	{
		if (Input.IsActionPressed("MoveLeft")||Input.IsActionPressed("MoveRight"))
		{
			StateMachine.ChangeState("WalkState");
		}

		if (Input.IsActionPressed("Jump"))
        {
            StateMachine.ChangeState("JumpState");
        }

        var dir_dash = Input.GetAxis("MoveLeft","MoveRight");
        if (Input.IsActionPressed("Dash") && dir_dash != 0 && !Player.Dashing && Player.DashTimer <= 0.0f)
        {
            StateMachine.ChangeState("DashState");
        }
		
	}

    public override void Exit()
	{
		Player.IsAttacking = false;
	}

}
