using Godot;
using System;

public partial class KnockbackState : State
{
	[Export] CharacterBody2D Character;
	[Export] Player Player;

	public float KnockbackForce = 400f;
	public Vector2 DamageSourcePosition;
	public float KnockbackTimer = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void Enter()
	{	
		KnockbackTimer = 0.0f;
		//calcula a direção do kb
		Vector2 kb_direction = (Player.GlobalPosition - DamageSourcePosition).Normalized();

		//a velocidade é indicada pela mult. da força pela direção
		var vel = kb_direction * KnockbackForce;
		vel.Y = -200f;
		Character.Velocity = vel;
	}

	public override void PhysicsUpdate(double delta)
	{
		var v = Character.Velocity;
		v.Y += 20;
		Character.Velocity = v;
		Character.MoveAndSlide();
		KnockbackTimer += (float)delta;

		if (KnockbackTimer >= 0.6)
		{	
			StateMachine.ChangeState("IdleState");
		}

	}

	public override void HandleInput(InputEvent @event)
	{
	}

    public override void Exit()
    {
		Player.IsKnocked = false;
    }

}
