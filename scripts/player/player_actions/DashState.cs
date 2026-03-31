using Godot;
using System;

public partial class DashState : State
{
	[Export] public CharacterBody2D Character;
	[Export] public Player Player;
    public float DashSpeed = 700.0f;       
    public float DashDistance = 150.0f;    
    public float DashStartingPosition  = 0.0f;
    public float DashDirection = 0.0f;
    public float DashCooldown = 1.0f;
	public float CurrentDistance;

	public override void Enter()
    {
        var inputDir = Input.GetAxis("MoveLeft", "MoveRight");
		//Player.SfxDash.Play();
        if (inputDir != 0)
            DashDirection = Math.Sign(inputDir);
        else
            DashDirection = Player.FacingDirection != 0 ? Player.FacingDirection : 1;

        DashStartingPosition  = Character.GlobalPosition.X;
        CurrentDistance = 0.0f;
        Player.Dashing = true;
        
        Character.Velocity = new Vector2(DashDirection * DashSpeed, 0);
        Player.DashTimer = DashCooldown;
    }

	public override void PhysicsUpdate(double delta)
    {
        
        Character.Velocity = new Vector2(DashDirection * DashSpeed, 0);
        Character.MoveAndSlide();

        CurrentDistance = Math.Abs(Character.GlobalPosition.X - DashStartingPosition );

        
        if (CurrentDistance >= DashDistance || Character.IsOnWall())
        {
            Player.Dashing = false;
            StateMachine.ChangeState("FallState");
        }

        

    }

	public override void Exit()
    {
        Player.Dashing = false;
    }

	public override void HandleInput(InputEvent @event)
    {
		if (Input.IsActionJustPressed("Jump") && (Player.CanDoubleJump))
		{
			Player.CanDoubleJump = false;
			StateMachine.ChangeState("JumpState");
		}
    }
}