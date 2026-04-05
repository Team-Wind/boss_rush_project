using Godot;
public partial class NeutroState : FSM
{
    [Export] public Boss1 boss;      
    [Export] private Player playerLocation;

    public override void Enter()
    {
        ///boss.AnimationPlayer("neutro");
        GD.Print("neutro");
    }

    
	public override void PhysicsUpdate(double delta)
	{
		//jogador parado, nao tem velocidade
		var vel = boss.Velocity;
		vel.X = 0;
		vel.Y = 0;


	    boss.Velocity = vel;
		boss.MoveAndSlide();
	}
    
    public override void ApplyStates(Player playerLocation)
    {
		//if(verificar a localiçaõ do player e mudar o estado)

		//if(verificar a vida e mudar o estágio)

		//if(se estiver no estágio 2 , atirar com colldown)

    }

 
}