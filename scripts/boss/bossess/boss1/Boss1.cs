using Godot;
using System;

public partial class Boss1 : Boss
{
    public override void _Ready()
    {
        base._Ready();
        CurrentHealth = MaxHealth;
        
    }

    public override void InitializeBoss()
    {
        CurrentHealth = MaxHealth;
    }

    public override void TakeDamage(int amount, int StaggerAmount)
    {
        base.TakeDamage(amount,StaggerAmount);
    }

    public override void FacePlayer()
    {
        base.FacePlayer();
    }

    public override void OnBodyEntered(Node2D body)
    {
        base.OnBodyEntered(body);
    }

    protected override void Die()
    {
        base.Die();
    }


}
