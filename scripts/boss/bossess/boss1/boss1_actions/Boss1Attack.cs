using Godot;
using System;

public partial class Boss1Attack : BState
{
	[Export] public Boss Boss1;
	private const float WalkSpeed = 160.0f;

	public override void Enter()
	{
		Boss1.SetAnimation("AttackB1");
		Boss1.AttackCollison.Disabled = false;
		Boss1.IsAttacking = true;
	}
	public override void PhysicsUpdate(double delta){}
	public override void Exit()
	{
		Boss1.IsAttacking = false;
		Boss1.AttackCollison.Disabled = true;
	}
    public override void Update(double delta){}
}
