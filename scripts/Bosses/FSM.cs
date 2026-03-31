using Godot;


public abstract partial class FSM : Node2D
{
    private Node debug;
    private Node player;

    // private AnimationPlayer animationPlayer;


    public virtual void Enter(){}
    public virtual void Exit(){}

	public virtual void Update(double delta){}

	public virtual void PhysicsUpdate(double delta){
        
    }

	public virtual void HandleInput(InputEvent inputEvent)	{}
}