using Godot;

public partial class AudioMenu : SettingsMenu
{
	[ExportGroup("Labels")]
	[Export] private Label MasterLabel;
	[Export] private Label MusicLabel;
	[Export] private Label SFXLabel;
	
	[ExportGroup("Sliders")]
	[Export] private HSlider MasterSlider;
	[Export] private HSlider MusicSlider;
	[Export] private HSlider SFXSlider;

	public override void _Ready()
	{
		base._Ready();

		RefreshUI();

		// Set slider ranges
		MasterSlider.MinValue = 0.0f;
		MasterSlider.MaxValue = 1.0f;
		MasterSlider.Step = 0.01f;
		MusicSlider.MinValue = 0.0f;
		MusicSlider.MaxValue = 1.0f;
		MusicSlider.Step = 0.01f;
		SFXSlider.MinValue = 0.0f;
		SFXSlider.MaxValue = 1.0f;
		SFXSlider.Step = 0.01f;

		// Load saved values or default to 1.0
		LoadVolume();

		SetVolume("Master", (float)MasterSlider.Value);
		SetVolume("Music", (float)MusicSlider.Value);
		SetVolume("SFX", (float)SFXSlider.Value);

		// Connect sliders
		MasterSlider.ValueChanged += (value) => SetVolume("Master", (float)value);
		MusicSlider.ValueChanged += (value) => SetVolume("Music", (float)value);
		SFXSlider.ValueChanged += (value) => SetVolume("SFX", (float)value);
	}

	private void SetVolume(string busName, float value)
	{
		int busIndex = AudioServer.GetBusIndex(busName);

		// Mute when zero
		if (value == 0f)
		{
			AudioServer.SetBusMute(busIndex, true);
		}
		else
		{
			AudioServer.SetBusMute(busIndex, false);
			AudioServer.SetBusVolumeDb(busIndex, Mathf.LinearToDb(value));
		}

		SaveVolume();
	}

	private void SaveVolume()
	{
		var config = new ConfigFile();
		config.SetValue("audio", "master", MasterSlider.Value);
		config.SetValue("audio", "music", MusicSlider.Value);
		config.SetValue("audio", "sfx", SFXSlider.Value);
		config.Save("user://settings.cfg");
	}

	private void LoadVolume()
	{
		var config = new ConfigFile();

		if (config.Load("user://settings.cfg") != Error.Ok)
		{
			// No save file, default to 1.0
			MasterSlider.Value = 1.0f;
			MusicSlider.Value = 1.0f;
			SFXSlider.Value = 1.0f;
			return;
		}

		MasterSlider.Value = (double)config.GetValue("audio", "master", 1.0f);
		MusicSlider.Value = (double)config.GetValue("audio", "music", 1.0f);
		SFXSlider.Value = (double)config.GetValue("audio", "sfx", 1.0f);
	}

	private void RefreshUI()
	{
		MasterLabel.Text = Tr("AUDIO_MASTER");
		MusicLabel.Text = Tr("AUDIO_MUSIC");
		SFXLabel.Text = Tr("AUDIO_SFX");
	}
}
