using Godot;
using System;

public abstract partial class Boss : CharacterBody2D
{
	[Export] public Player PlayerRef;
	[Export] public Area2D ContactHitbox;
	[Export] public Area2D AttackHitbox;
	[Export] public CollisionShape2D AttackCollison;
	[Export] public Sprite2D Sprite2D;
	[Export] public AnimationPlayer AnimationPlayer;
	[Export] public BStateMachine BFSM;
	[Export] public int MaxHealth = 100;
	[Export] protected AnimatedSprite2D AnimSprite2D;
	[Export] public int DamageAmount = 1;
	[Export] public int StaggerCounter;
	public int CurrentStaggerCounter;
	public float DistanceToPlayer;
	public int CurrentHealth;
	protected Vector2 Direction;
    protected bool IsDead = false;
    public bool IsAttacking = false;

	[Signal] public delegate void BossDiedEventHandler();
	[Signal] public delegate void BossHitEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
		CurrentStaggerCounter = 0;
		InitializeBoss();
		if (ContactHitbox != null)
        {
            ContactHitbox.BodyEntered += OnBodyEntered;
        }
		AttackCollison.Disabled = true;
		AttackHitbox.Monitoring = true;

	}

	public abstract void InitializeBoss();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public virtual void TakeDamage(int amount, int StaggerAmount)
	{
		//finaliza a função caso o boss ja esteja morto
		if (IsDead) return;

		//decremento de vida
		CurrentHealth -= amount;
		CurrentStaggerCounter += StaggerAmount;
		EmitSignal(SignalName.BossHit, StaggerAmount);
		GD.Print($"Ataque desferido! {PlayerRef.AttackDamage} de dano no boss");
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

		if (Direction.X > 0)
		{
			Sprite2D.FlipH = true;
		}
		else
		{
			Sprite2D.FlipH = false;
		}
	}

	public virtual void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.TakeDamage(DamageAmount, GlobalPosition);
			GD.Print($"Colisão detectada com o Player! Enviando {DamageAmount} de dano.");
		}
	}
	public virtual void OnAttackEntered(Node2D body)
	{
		GD.Print("Entrou algo: ", body.Name);

		if(body is Player player)
		{
			player.TakeDamage(DamageAmount, GlobalPosition);
			GD.Print($"Ataque deferido ! Enviando {DamageAmount} de dano.");
		}
	}

	protected virtual void Die()
	{
		IsDead = true;
		EmitSignal(SignalName.BossDied);
		QueueFree();
	}

	public void SetAnimation(StringName name)
	{
		AnimSprite2D.Play(name);
	}
}
