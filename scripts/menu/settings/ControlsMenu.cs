using Godot;
using Godot.Collections;
using System;

public partial class ControlsMenu : SettingsMenu
{
	[ExportGroup("Labels")]
	[Export] private Label MoveLeftLabel;
	[Export] private Label MoveRightLabel;
	[Export] private Label JumpLabel;
	[Export] private Label DashLabel;
	
	[ExportGroup("Action Buttons")]
	[Export] private Button MoveLeftButton;
	[Export] private Button MoveRightButton;
	[Export] private Button JumpButton;
	[Export] private Button DashButton;

	private string _waitingForAction = null;

	private Dictionary<string, Button> _actionButtons;

	public override void _Ready()
	{
		base._Ready();//Chama Ready do pai

		RefreshUI();

		_actionButtons = new Dictionary<string, Button>
		{
			{ "MoveLeft", MoveLeftButton },
			{ "MoveRight", MoveRightButton },
			{ "Jump", JumpButton },
			{ "Dash", DashButton }
		};

		// Conecta cada botão para escutar o remap
		foreach (var pair in _actionButtons)
		{
			string action = pair.Key;
			Button button = pair.Value;

			button.Pressed += () => StartListening(action);
		}

		LoadBindings();
		RefreshButtons();
	}

	private void StartListening(string action)
	{
		_waitingForAction = action;
		// Mostra que está esperando input
		_actionButtons[action].Text = "...";
	}
	
	public override void _Input(InputEvent @event)
	{
		if (_waitingForAction == null) return;

		if (@event is InputEventKey keyEvent && keyEvent.IsPressed() && !keyEvent.Echo)
		{
			Remap(_waitingForAction, keyEvent.Keycode);
			_waitingForAction = null;
			RefreshButtons();

			// Consume the event so it doesn't affect gameplay
			GetViewport().SetInputAsHandled();
		}
	}

	private void Remap(string action, Key newKey)
	{
		InputMap.ActionEraseEvents(action);

		var newEvent = new InputEventKey();
		newEvent.Keycode = newKey;
		InputMap.ActionAddEvent(action, newEvent);

		SaveBindings();
	}

	private void RefreshButtons()
	{
		foreach (var pair in _actionButtons)
		{
			var events = InputMap.ActionGetEvents(pair.Key);
			if (events.Count > 0 && events[0] is InputEventKey keyEvent)
				pair.Value.Text = keyEvent.Keycode.ToString();
		}
	}

	private void SaveBindings()
	{
		var config = new ConfigFile();

		foreach (var pair in _actionButtons)
		{
			var events = InputMap.ActionGetEvents(pair.Key);
			if (events.Count > 0 && events[0] is InputEventKey keyEvent)
				config.SetValue("bindings", pair.Key, (int)keyEvent.Keycode);
		}

		config.Save("user://bindings.cfg");
	}

	private void LoadBindings()
	{
		var config = new ConfigFile();
		if (config.Load("user://bindings.cfg") != Error.Ok) return;

		foreach (var pair in _actionButtons)
		{
			if (config.HasSectionKey("bindings", pair.Key))
			{
				Key savedKey = (Key)(int)config.GetValue("bindings", pair.Key);
				Remap(pair.Key, savedKey);
			}
		}
	}

	private void RefreshUI()
	{
		MoveLeftLabel.Text = Tr("CONTROLS_LEFT");
		MoveRightLabel.Text = Tr("CONTROLS_RIGHT");
		JumpLabel.Text = Tr("CONTROLS_JUMP");
		DashLabel.Text = Tr("CONTROLS_DASH");
	}
}
