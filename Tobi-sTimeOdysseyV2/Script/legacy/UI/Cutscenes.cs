using Godot;
using System;
using System.Collections.Generic;

namespace Com.IronicEntertainment.TobisTimeOdyssey.UI
{

	public class Cutscenes : Node2D
	{
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

		public void PlayCutscene()
		{
           
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