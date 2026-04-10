using Godot;
using System;

public abstract partial class BossIdle : BState
{
	[Export] Timer ChooseState;
	public override void Enter(){}
	public override void PhysicsUpdate(double delta){}
	public override void Exit() {}
    public override void Update(double delta) {}
}
