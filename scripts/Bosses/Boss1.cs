using Godot;
using System;
public partial class Boss1 : CharacterBody2D
{
    [Export] private AnimatedSprite2D Animation;
    [Export] private CollisionShape2D hitbox;
    [Export] private Timer Timer;
    [Signal] public delegate void HitEventHandler(double life); 


    public bool CanChangeState = false;
    public bool IsAttacking = false;
    public bool IsRunning = false; 
    public double EspecialCoolDown = 10;
    public int CurrentHp;

    public bool IsDead = false;

    public override void _PhysicsProcess(double delta)
    {
       // if() não sei oq usar aq 
    }
    //pegar as animações do boss
    public void AnimationPlayer(StringName name)
	{
		if(Animation.Animation == name)
			return;
		Animation.Play(name);
	}

    public void TakeDamage(int amount) // dano do player
    {
        CurrentHp -= amount;
        GD.Print($"vida do boss: {CurrentHp}");
    }


}