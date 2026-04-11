using Godot;
using System;

public partial class BossIdle : BState
{
	[Export] Timer ChooseStateTimer;
	public override void Enter()
	{
		//cria o timer toda vez que entra no estado idle
		if (ChooseStateTimer != null)
		{
			//inicia o timer 
			ChooseStateTimer.Start();
			//se o timer estiver conectado corretamente, incrementa o timer até dar timeout
			if(ChooseStateTimer.IsConnected(Timer.SignalName.Timeout, Callable.From(OnTimeout)))
			{
				ChooseStateTimer.Timeout += OnTimeout;
			}
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		var vel = Boss.Velocity;
		vel.X = 0;
		vel.Y = 0;
		Boss.Velocity = vel;
		Boss.MoveAndSlide();
	}
	public override void Exit()
	{
		//finaliza o timer quando sai do estado
		ChooseStateTimer?.Stop();
	}
    public override void Update(double delta)
	{
	}

	private void OnTimeout()
	{
		//ao finalizar o timer (timeout), roda a função de escolher o estado
		DecideState();
	}

	//função de escolher o estado (por enquanto abstract mas provavelmente será virtual+implementada aqui)
	protected void DecideState()
	{
	
	}
}
