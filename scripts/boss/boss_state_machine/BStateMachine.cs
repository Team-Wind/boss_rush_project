using Godot;
using System;
using System.Collections.Generic; 

public partial class BStateMachine : Node
{
	[Export] public BState InitialState;
	[Export] Label BossLabel;
    private BState CurrentState;
	private Dictionary<string, BState> BStates = new(); 

	public void Setup(Boss boss)
	{
		foreach(Node child in GetChildren())
		{
			if (child is BState state)
			{
				state.Setup(boss,this);
				BStates[state.Name.ToString().ToLower()] = state;
			}

		}

		CurrentState = InitialState;
        CurrentState?.Enter();
	}

	public override void _Process(double delta)
	{
		if (BossLabel != null) BossLabel.Text = CurrentState?.Name;
	}

	public void ChangeState(string newStateName)
	{	
		//cria a chave de armazenamento do estado
		string key = newStateName.ToLower();
		//se o estado lido nao estiver em states, finaliza a função
		if (!BStates.ContainsKey(key)) return;

		//sai do estado, troca o estado pela key lida e dps entra no novo estado
		CurrentState?.Exit();
		CurrentState = BStates[key];
		CurrentState.Enter();
	}

    public override void _PhysicsProcess(double delta)
    {
        CurrentState?.PhysicsUpdate(delta);
    }

}
