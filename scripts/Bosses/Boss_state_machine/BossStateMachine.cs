using Godot;
using System;
using System.Collections.Generic;

public partial class BossStateMachine : Node2D
{
	[Export] public FSM NormalState;
	[Export] public Label StateLabel;


	public FSM CurrentState;
	public Dictionary<string , FSM> States = new(); 



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	//Pegando as nodes filhas para processamento dos stados dos bosses 
	{
		foreach (Node child in GetChildren())
		{
			if (child is FSM st)
			{
				States[st.Name.ToString().ToLower()] = st;
				st.BossStateMachine = this;
				GD.Print($"Estado armazenado: {child.Name}, com sucesso!");
			}
			else
			{
				GD.Print($"Nó ignorado: {child.Name} (Não herdou de State ou não buildou)");
			}
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//EXPLICAÇÃO
	public override void _Process(double delta)
	{
		if(CurrentState != null)
		{
			CurrentState.Update(delta);
		}
	}
	//EXPLICAÇÃO
	public override void _PhysicsProcess(double delta)
	{
		if (CurrentState != null)
		{
			CurrentState.PhysicsUpdate(delta);
		}

		if (StateLabel != null) StateLabel.Text = CurrentState?.Name;
	}
	//EXPLICAÇÃO
	public void ChangeState (string NewStateName)
	{
		if (CurrentState != null)
		{
			CurrentState.Exit();
		}
		
		//variavel de acesso para um novo estado
		var key = NewStateName.ToLower();

		//verificação de erro 
		if (States.TryGetValue(key, out var newState))
		{
			CurrentState = newState;
		}
		else
		{
			GD.PushWarning($"State '{key}' não registrado."); //(debug)
		}

		//se o estado for válido, ele pode ser definido como novo estado e chamar a função de entrada
		if (CurrentState != null)
		{
			CurrentState.Enter();
		}
	}
}
