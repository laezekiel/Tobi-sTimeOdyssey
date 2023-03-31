using Com.IronicEntertainment.TobisTimeOdyssey.Elements;
using Com.IronicEntertainment.TobisTimeOdyssey.Managers;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.UI
{

	public class Cutscenes : Node2D
	{
		public enum Type
		{
			begining,
			end
        }

		[Export]
		private NodePath
			leftPath,
			rightPath,
			textPath,
			textTimerPath,
			nextPath;

		private AnimatedSprite
			left,
			right;

		private Label
			text;

		private Timer
			textTimer;

		private Button
			next;

		private string
			speaker,
			sentence,
			position,
			_display = "";

		private int
			currentS = 0;

		private List<string>
			Sentences,
			Speakers,
			Positions;

		public bool
			set = false;

		private Type
			type;

		private string Display { get { return "\n" + speaker + ":\n" + _display; } }

		private void Init()
        {
			left = GetNode<AnimatedSprite>(leftPath);
			right = GetNode<AnimatedSprite>(rightPath);

			text = GetNode<Label>(textPath);

			textTimer = GetNode<Timer>(textTimerPath);

			next = GetNode<Button>(nextPath);

			next.Connect("pressed", this, nameof(OnTouch));

			textTimer.Connect("timeout", this, nameof(UpdateText));
        }

		public override void _Ready()
		{
			base._Ready();
			Init();

		}

		public void PlayCutscene(Type ptype = Type.begining)
		{
            if (!set)
			{
				switch (ptype)
				{
					case Type.begining:
						Sentences = GameManager.Level.Cutscenes.Sentences[0];
						Speakers = GameManager.Level.Cutscenes.Sentences_Character[0];
						Positions = GameManager.Level.Cutscenes.Sentences_Position[0];

						type = Type.begining;
						break;

					case Type.end:
						Sentences = GameManager.Level.Cutscenes.Sentences[1];
						Speakers = GameManager.Level.Cutscenes.Sentences_Character[1];
						Positions = GameManager.Level.Cutscenes.Sentences_Position[1];

						type = Type.end;
						break;
				}

				set = true;
			}

            UpdateStrings();

			if (position == nameof(left)) { left.Frames = Tobi_Data_JSON.GetSpeaker(speaker); left.Frame = 0; if (right.Frames != null) right.Frame = 1; }
			else if (position == nameof(right)) { right.Frames = Tobi_Data_JSON.GetSpeaker(speaker); right.Frame = 0; if (left.Frames != null) left.Frame = 1; }
			
			text.Text = Display;

			textTimer.Start();
		}

		public void OnTouch()
        {
			if (textTimer.TimeLeft != 0)
			{
				textTimer.Stop();

				_display += sentence;
				sentence = "";
				text.Text = Display;
			}
			else
			{
				currentS++;
				if (currentS < Sentences.Count) PlayCutscene();
				else EndCutscenes();
			}
		}

		public void EndCutscenes()
        {
            switch (type)
            {
                case Type.begining:
					TobiView.GetInstance().ShowButton();
					GameManager.GetInstance().SetGameModePlay();
					QueueFree();
					break;
                case Type.end:
					GameManager.GetInstance().WinEffect();
					QueueFree();
                    break;
            }
        }

		public void UpdateText()
        {
			_display += sentence[0];
			sentence = sentence.Remove(0, 1);

			text.Text = Display;

			if (sentence.Length > 0) textTimer.Start();
		}

		public void UpdateStrings()
        {
			_display = "";
			sentence = Sentences[currentS];
			speaker = Speakers[currentS];
			position = Positions[currentS];
		}
	}

}