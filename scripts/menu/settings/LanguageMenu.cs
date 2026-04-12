using Godot;
using System;

public partial class LanguageMenu : SettingsMenu
{
	[Export] private Label LanguageLabel;
	[Export] private Button EnglishButton;
	[Export] private Button PortugueseButton;
	
	private LanguageManager LanguageManager;

	public override void _Ready()
	{
		base._Ready();

		RefreshUI();

		LanguageManager = GetNode<LanguageManager>("/root/LanguageManager");

		EnglishButton.Pressed += () => { LanguageManager.SetLanguage("en"); HighlightCurrentLanguage();};
		PortugueseButton.Pressed += () => { LanguageManager.SetLanguage("pt_BR"); HighlightCurrentLanguage();};

		HighlightCurrentLanguage();
	}

	private void HighlightCurrentLanguage()
	{
		string current = LanguageManager.GetCurrentLanguage();

		// Reset all
		EnglishButton.Text = "English";
		PortugueseButton.Text = "Português BR";

		// Highlight active
		switch (current)
		{
			case "en": EnglishButton.Text = "English<-"; break;
			case "pt_BR": PortugueseButton.Text = "Português BR<-"; break;
		}
	}

	private void RefreshUI()
	{
		LanguageLabel.Text = Tr("LANGUAGE_LABEL");
	}
}
