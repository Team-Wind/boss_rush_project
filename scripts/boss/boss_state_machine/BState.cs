using Godot;
using System;

public abstract partial class BState : Node
{
	public BStateMachine BStateMachine;
	[Export] public Boss Boss;
	

	public abstract void Enter();
	public abstract void PhysicsUpdate(double delta);
	public abstract void Exit();
    public abstract void Update(double delta);
	
}
