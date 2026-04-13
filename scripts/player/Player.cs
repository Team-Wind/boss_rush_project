using Godot;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public partial class Player : CharacterBody2D
{
	//[Export] private AnimatedSprite2D Animation;
	[Export] public Boss BossRef;
	[Export] private AnimationPlayer Effects;
	[Export] private AnimationPlayer Animations;
	[Export] private Timer IframeDuration;
	[Export] private Sprite2D Sprite;
	[Export] public Area2D SwordArea;
	[Export] public CollisionShape2D SwordCollider;
	[Export] public CollisionShape2D Hitbox;
	//esse sinal serve para indicar que o player levou dano
	[Signal] public delegate void HitEventHandler(Vector2 sourcePosition);
	
	//assets de som que podemos colocar==============
	//[Export] public AudioStreamPlayer SfxWalk; 
	//[Export] public AudioStreamPlayer SfxDash;
	//[Export] public AudioStreamPlayer SfxJump;
	//========================================
	
	//variáveis do jogador
		//movimentação
		public bool CanDoubleJump = false;
		public bool WasOnFloor = true;
		public bool Dashing = false;
		public float DashCooldown = 0.5f;
		public float DashTimer = 0.0f;
		//status
		[Export] public int HitPoints = 10;
		public int CurrentHP;
		[Export] public int AttackDamage = 5;
		[Export] public int StaggerAmount = 1;
 		public bool IsKnocked = false;
		public bool IsInvulnerable = false;
		public bool IsAttacking = false;
		public int FacingDirection = 1;
		public bool IsDead = false;

		public bool bisInArea = false;


    public override void _Ready()
    {
        CurrentHP = HitPoints;
		SwordCollider.Disabled = true;
		SwordArea.Monitoring = true;
		SwordArea.BodyEntered += SwordOnBodyEntered;
    }


	public override void _PhysicsProcess(double delta)
	{
		if (WasOnFloor && !IsOnFloor())
		{
			CanDoubleJump = true; //garante que o double jump so reinicie quando o jogador encostar no chão de novo
		}

		WasOnFloor = IsOnFloor();

		FlipPlayer();

		if (Velocity.X != 0) { FacingDirection = Math.Sign(Velocity.X); }
			
		if (DashTimer > 0.0f) { DashTimer -= (float)delta; }

	}

	public void SetAnimation(StringName name)
	{
		Animations.Play(name);
	}

	public virtual void SwordOnBodyEntered(Node body)
	{
		GD.Print("Entrou algo: ", body.Name);

		if (body is Boss BossRef)
		{
			BossRef.TakeDamage(AttackDamage, StaggerAmount);
			GD.Print("DEU DANO");
		}
	}

	public async void TakeDamage(int amount, Vector2 sourcePosition)
	{	
		//finaliza a função caso o jogador estiver invulneravel;
		if (IsInvulnerable) return;

		FrameFreeze(0.05, 0.04);

		//logica de decremento de hp / emissão de sinal de hit
		CurrentHP -= amount;
		EmitSignal(SignalName.Hit, sourcePosition);
		GD.Print($"vida atual: {CurrentHP}");

		StartHurt();
	}

	public async void StartHurt()
	{
		//inicia a invulnerabilidade
		IsInvulnerable = true;

		Effects.Play("HurtBlink");

		//inicia o timer e espera o final
		IframeDuration.Start();
		await ToSignal(IframeDuration, Timer.SignalName.Timeout);
		
		//espera o timer acabar e toca a animação de reset (alpha do color rect = 0)
		Effects.Play("RESET");

		//finaliza a invulnerabilidade
		IsInvulnerable = false;
	}

	public async void FrameFreeze(double timeScale, double duration)
	{
		Engine.TimeScale = timeScale;
		await ToSignal(GetTree().CreateTimer(duration, false, true), SceneTreeTimer.SignalName.Timeout);

		Engine.TimeScale = 1.0;
	}

	public void FlipPlayer()
	{
		if (Velocity.X <= 0) Sprite.FlipH = false;
		else Sprite.FlipH = true;
	}
}
