using Godot;

public partial class RunningState : FSM
{
    [Export] public Boss1 boss;      
    [Export] private Player playerLocation;

    

    public override void Enter()
    {
        
    }

    public override void PhysicsUpdate(double delta)
	{
		
	}

}