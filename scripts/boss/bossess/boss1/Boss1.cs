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

    protected override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
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
