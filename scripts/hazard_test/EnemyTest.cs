using Godot;
using System;

public partial class EnemyTest : Area2D
{	
	//com o dano editavel no inspetor, podemos usar essa classe como base para criar outros tipos de danos e ataques
	[Export] public int DamageAmount;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.TakeDamage(DamageAmount, GlobalPosition);
		}

		GD.Print($"Colisão detectada com o Player! Enviando {DamageAmount} de dano.");

	}
}
