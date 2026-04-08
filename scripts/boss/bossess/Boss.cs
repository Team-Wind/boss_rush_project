using Godot;
using System;

public abstract partial class Boss : CharacterBody2D
{
	[Export] public Player PlayerRef;
	[Export] public AnimationPlayer AnimationPlayer;
	[Export] public BStateMachine BFSM;
	[Export] public int MaxHealth = 100;
	[Export] protected Sprite2D Sprite2D;
	protected int CurrentHealth;
    protected bool IsDead = false;
	protected Vector2 Direction;

	[Signal] public delegate void BossDiedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
		InitializeBoss();
	}

	public abstract void InitializeBoss();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public virtual void TakeDamage(int amount)
	{
		//finaliza a função caso o boss ja esteja morto
		if (IsDead) return;

		//decremento de vida
		CurrentHealth -= amount;
		//verificação de morte: se a vida for menor q zero, muda para o estado de morte
		if (CurrentHealth <= 0)
		{
			IsDead = true; 
			BFSM.ChangeState("BossDeath");
		}
	}

	public virtual void FacePlayer()
	{
		if (PlayerRef == null || Sprite2D == null) return;

		Direction = PlayerRef.GlobalPosition - GlobalPosition;

		if (Direction.X < 0)
		{
			Sprite2D.FlipH = true;
		}
		else
		{
			Sprite2D.FlipH = false;
		}
	}

	protected virtual void Die()
	{
		IsDead = true;
		EmitSignal(SignalName.BossDied);
		QueueFree();
	}
}
