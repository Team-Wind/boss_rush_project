using Godot;
using System;

public abstract partial class BState : Node
{
	public BStateMachine BStateMachine;
	[Export] public Boss Boss;

	// Called when the node enters the scene tree for the first time.

	public virtual void Enter(){}
	public virtual void PhysicsUpdate(double delta){}
	public virtual void Exit() {}
    public virtual void Update(double delta) {}
	
}
