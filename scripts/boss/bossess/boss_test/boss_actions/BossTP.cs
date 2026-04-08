using Godot;
using System;

public partial class BossTP : BState
{
	public BStateMachine BStateMachine;

	// Called when the node enters the scene tree for the first time.

	public override void Enter()
	{
		Boss.AnimationPlayer.Play("boss_tp");
	}
	public override void PhysicsUpdate(double delta){}
	public override void Exit() {}
    public override void Update(double delta) {}
}
