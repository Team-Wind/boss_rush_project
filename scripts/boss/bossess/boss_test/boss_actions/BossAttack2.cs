using Godot;
using System;

public partial class BossAttack2 : BState
{
	public BStateMachine BStateMachine;

	// Called when the node enters the scene tree for the first time.

	public override void Enter()
	{
		Boss.AnimationPlayer.Play("boss_attack2");
	}
	public override void PhysicsUpdate(double delta){}
	public override void Exit() {}
    public override void Update(double delta) {}
}
