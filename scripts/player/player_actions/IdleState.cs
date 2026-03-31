using Godot;
using System;

public partial class IdleState : State
{	
	[Export] public CharacterBody2D Character;
	[Export] public Player Player;
	//[Export] public Player Player;

	//aqui, o enter funciona como a entrada ao estado, e alteraremos as animações por esse método
	public override void Enter()
	{
		//player.AnimationPlayer("spr_idle");
		//GD.Print("Estou Idle!");
	}

	//função de processamento e atualização da física do jogador + velocidade
	public override void PhysicsUpdate(double delta)
	{
		//jogador parado, nao tem velocidade
		var vel = Character.Velocity;
		vel.X = 0;
		vel.Y = 0;

		//verificação de contato com o solo (vem para o physicsupdate por nao depender de comando do jogador)
		if (!Character.IsOnFloor())
		{
			StateMachine.ChangeState("FallState");
		}

		Character.Velocity = vel;
		Character.MoveAndSlide();
	}

	//de acordo com o input, o estado mudará
    public override void HandleInput(InputEvent @event)
    {
		//andar para esquerda ou para a direita
        if (Input.IsActionPressed("MoveLeft")||Input.IsActionPressed("MoveRight"))
		{
			StateMachine.ChangeState("WalkState");
		}

		//pular
		if (Input.IsActionPressed("Jump"))
		{
			StateMachine.ChangeState("JumpState");
		}

		//dash 
		if (Input.IsActionPressed("Dash") && !Player.Dashing && Player.DashTimer <= 0.0f)
		{
			StateMachine.ChangeState("DashState");
		}

    }

	 public override void Exit()
    {
    }

}
