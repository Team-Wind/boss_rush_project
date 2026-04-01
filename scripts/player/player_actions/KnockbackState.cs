using Godot;
using System;

public partial class KnockbackState : State
{
	[Export] Player Player;

	public float KbHorizontalForce = 400f;
	public float KbVerticalForce = -200.0f;
	public float KnockbackDuration = 0.30f;
	public float KnockbackTimer = 0.0f;
	public int KnockbackDirection = 0;
	public Vector2 DamageSourcePosition;

	// Called when the node enters the scene tree for the first time.
	public override void Enter()
	{	
		KnockbackTimer = 0.0f;
		//calcula a direção do kb
		KnockbackDirection = (Player.GlobalPosition.X > DamageSourcePosition.X) ? 1 : -1;

		//para uma animação padrão, apenas calculamos a direção usando uma velocidade fixa em X (com um leve pulinho fixo em Y)
		Player.Velocity = new Vector2(KnockbackDirection * KbHorizontalForce, KbVerticalForce);
	}

	public override void PhysicsUpdate(double delta)
	{
		//incrementa o timer
		KnockbackTimer += (float) delta;

		//velocidade frame a frame
		var v = Player.Velocity;
		v.X = KnockbackDirection * KbHorizontalForce;
		v.Y += 10;

		Player.Velocity = v;
		Player.MoveAndSlide();

		if (KnockbackTimer >= KnockbackDuration)
		{	
			if (Player.IsOnFloor()) {StateMachine.ChangeState("IdleState");}
            
       	 	else {StateMachine.ChangeState("FallState");}        
		}

	}

    public override void Exit()
    {
		Player.IsKnocked = false;
    }

}
