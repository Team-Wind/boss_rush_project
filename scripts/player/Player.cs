using Godot;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public partial class Player : CharacterBody2D
{
	[Export] private AnimatedSprite2D Animation;
	[Export] public CollisionShape2D Hitbox;
	//esse sinal serve para indicar que o player levou dano
	[Signal] public delegate void HitEventHandler(Vector2 sourcePosition);
	
	//assets de som que podemos colocar==============
	//[Export] public AudioStreamPlayer SfxWalk; 
	//[Export] public AudioStreamPlayer SfxDash;
	//[Export] public AudioStreamPlayer SfxJump;
	//========================================

	
	//variaveis do jogador
		//movimentação
		public bool CanDoubleJump = false;
		public bool WasOnFloor = true;
		public bool Dashing = false;
		public float DashCooldown = 0.5f;
		public float DashTimer = 0.0f;
		public bool Invunerable = false;

		//status
		public int HitPoints = 10;
		public int CurrentHP;
		public bool IsKnocked = false;

		//random
		public int FacingDirection = 1;
		public bool IsDead = false;
		
	public override void _PhysicsProcess(double delta)
	{
		if (WasOnFloor && !IsOnFloor())
		{
			CanDoubleJump = true; //garante que o double jump so reinicie quando o jogador encostar no chão de novo
		}

		WasOnFloor = IsOnFloor();

		//FlipPlayer();

		if (Velocity.X != 0) { FacingDirection = Math.Sign(Velocity.X); }
			
		if (DashTimer > 0.0f) { DashTimer -= (float)delta; }

	}

	public void AnimationPlayer(StringName name)
	{
		if(Animation.Animation == name)
			return;
		Animation.Play(name);
	}

	public void TakeDamage(int amount, Vector2 sourcePosition)
	{
		CurrentHP -= amount;
		EmitSignal(SignalName.Hit, sourcePosition);
		GD.Print($"vida atual: {CurrentHP}");
	}

	//private void FlipPlayer()
	//{
	//	if (Velocity.X !=0){ Animation.FlipH = Velocity.X < 0; }
	//}
}
