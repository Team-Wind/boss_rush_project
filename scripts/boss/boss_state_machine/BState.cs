using Godot;
using System;

public abstract partial class BState : Node
{
	protected Boss OwnerBoss;
	protected BStateMachine BFSM;

	// Called when the node enters the scene tree for the first time.
	public void Setup(Boss boss, BStateMachine fsm)
	{
		OwnerBoss = boss;
		BFSM = fsm;
	}

	public virtual void Enter(){}
	public virtual void PhysicsUpdate(double delta){}
	public virtual void Exit() {}
    public virtual void Update(double delta) {}
	
}
