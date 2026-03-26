using Godot;
using System;
using System.Collections.Generic;


//classe que vai controlar os estados do player, ela tem um estado atual e métodos para mudar de estado, atualizar o estado atual e lidar com input

public partial class StateMachine : Node
{
	
	[Export] public State InitialState; //estado inicial do player, será o estado IdleState
	public State CurrentState;
	public Dictionary<string, State> States = new(); //dicionário para armazenamento dos estados do player

	

}
