using Godot;
using System;

public partial class WalkState : State
{
	[Export] public CharacterBody2D Character; 
    [Export] public Player Player;
    protected const float WalkSpeed = 150.0f; //variavel local que determina a velocidade do andar

    public override void Enter()
    {
        //Player.AnimationPlayer("nome_aqui");
    }

    public override void PhysicsUpdate(double delta)
    {
        if (!Character.IsOnFloor())
        {
            StateMachine.ChangeState("FallState"); //fall state nao tem input, cai no physics update
        } 

        var vel = Character.Velocity;
        var direction = Input.GetAxis("MoveLeft","MoveRight"); //verificação de direção

        if (direction == 0)
        {
            StateMachine.ChangeState("IdleState"); //se não houver movimento, vota pro estado idle (tb nao tem input, por isso está aqui)
        }

        vel.X = direction * WalkSpeed;
        Character.Velocity = vel;
        Character.MoveAndSlide();
    }

    public override void HandleInput(InputEvent @event)
    {
        if (Input.IsActionPressed("Jump"))
        {
            StateMachine.ChangeState("JumpState");
        }

        var dir_dash = Input.GetAxis("MoveLeft","MoveRight");
        if (Input.IsActionPressed("Dash") && dir_dash != 0 && !Player.Dashing && Player.DashTimer <= 0.0f)
        {
            StateMachine.ChangeState("DashState");
        }
    }

     public override void Exit()
    {
    }


}
