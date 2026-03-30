using Godot;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public partial class Player : CharacterBody2D
{
	[Export] private AnimatedSprite2D Animation;

	//assets que podemos colocar==============
	//[Export] public AudioStreamPlayer SfxWalk; 
	//[Export] public AudioStreamPlayer SfxDash;
	//[Export] public AudioStreamPlayer SfxJump;
	//========================================

	//variaveis do jogador
	[Export] public CollisionShape2D Hitbox;
	public bool CanDoubleJump = false;
	public bool WasOnFloor = true;
	public bool Invunerable = false;
	public bool Dashing = false;
	public int FacingDirection = 1;
	public bool IsDead = false;
	public float DashTimer = 0.0f;
    public float DashCooldown = 0.5f;

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

	//private void FlipPlayer()
	//{
	//	if (Velocity.X !=0){ Animation.FlipH = Velocity.X < 0; }
	//}
}
