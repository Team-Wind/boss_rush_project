using Godot;
using System;

public partial class BossAttack1 : BState
{	
	public override void Enter()
	{
		Boss.AnimationPlayer.Play("boss_attack1");
	}
	public override void PhysicsUpdate(double delta){}
	public override void Exit() {}
    public override void Update(double delta) {}
}
