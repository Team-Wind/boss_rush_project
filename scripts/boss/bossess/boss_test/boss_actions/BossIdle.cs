using Godot;
using System;

public partial class BossIdle : BState
{	
	// Called when the node enters the scene tree for the first time.
	public override void Enter()
	{
		Boss.AnimationPlayer.Play("boss_idle");	

	}
	public override void PhysicsUpdate(double delta)
	{
		var vel = Boss.Velocity;
		vel.X = 0;
		vel.Y = 0;

		//verificação de contato com o solo (vem para o physicsupdate por nao depender de comando do jogador)
		if (!Boss.IsOnFloor())
		{
			BStateMachine.ChangeState("BossFall");
		}

		Boss.Velocity = vel;
		Boss.MoveAndSlide();
		Boss.FacePlayer();
	}
	public override void Exit() {}
    public override void Update(double delta) {}
}
