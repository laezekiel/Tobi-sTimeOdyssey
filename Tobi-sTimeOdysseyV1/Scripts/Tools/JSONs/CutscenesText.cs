using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools;
using Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs;
using Godot;

// Author: Louis Bouré
namespace Com.IronicEntertainment.TobisTimeOdyssey.Tools.JSONs
{

    public struct CutscenesText
    {
        private enum TypeCutscenes
        {
            Begining,
            End
        }

        public enum FieldCutscenes
        {
            Played,
            Speaker,
            Sentences
        }

        public enum Speaker
        {
            Position,
            Speak,
            Character
        }

        public CutscenesText(List<string> pLevel)
        {
            Godot.Collections.Dictionary lJson = Tobi_Data_JSON.LoadFileTXT(pLevel);
            Godot.Collections.Dictionary lBegining = lJson[TypeCutscenes.Begining.ToString()] as Godot.Collections.Dictionary;
            Godot.Collections.Dictionary lEnd = lJson[TypeCutscenes.End.ToString()] as Godot.Collections.Dictionary;
            Godot.Collections.Array lSpeaker;
            Godot.Collections.Array lSpeak;
            Godot.Collections.Array lSentences;

            List<Dictionary<Speaker, object>> lTempSpeaker;
            List<int> lTempSpeak;
            List<string> lTempSentences;

            if (lBegining.Count != 0)
            {
                lSpeaker = lBegining[FieldCutscenes.Speaker.ToString()] as Godot.Collections.Array;
                lSentences = lBegining[FieldCutscenes.Sentences.ToString()] as Godot.Collections.Array;
                lTempSpeaker = new List<Dictionary<Speaker, object>>();
                lTempSentences = new List<string>();

                foreach (Godot.Collections.Dictionary item in lSpeaker)
                {
                    lSpeak = item[Speaker.Speak.ToString()] as Godot.Collections.Array;
                    lTempSpeak = new List<int>();

                    foreach (var item2 in lSpeak) lTempSpeak.Add(item2.ToString().ToInt());

                    lTempSpeaker.Add(new Dictionary<Speaker, object>()
                    {
                        { Speaker.Position, item[Speaker.Position.ToString()].ToString() },
                        { Speaker.Speak, lTempSpeak },
                        { Speaker.Character, item[Speaker.Character.ToString()].ToString() }
                    } );
                }

                foreach (var item in lSentences) lTempSentences.Add(item.ToString());

                _begining = new Dictionary<FieldCutscenes, object>()
                {
                    { FieldCutscenes.Played, lBegining[FieldCutscenes.Played.ToString()] },
                    { FieldCutscenes.Speaker, lTempSpeaker },
                    { FieldCutscenes.Sentences, lTempSentences }
                }; 
            }
            else _begining = null;

            if (lEnd.Count != 0) 
            {
                lSpeaker = lEnd[FieldCutscenes.Speaker.ToString()] as Godot.Collections.Array;
                lSentences = lEnd[FieldCutscenes.Sentences.ToString()] as Godot.Collections.Array;
                lTempSpeaker = new List<Dictionary<Speaker, object>>();
                lTempSentences = new List<string>();

                foreach (Godot.Collections.Dictionary item in lSpeaker)
                {
                    lSpeak = item[Speaker.Speak.ToString()] as Godot.Collections.Array;
                    lTempSpeak = new List<int>();

                    foreach (var item2 in lSpeak) lTempSpeak.Add(item2.ToString().ToInt());

                    lTempSpeaker.Add(new Dictionary<Speaker, object>()
                    {
                        { Speaker.Position, item[Speaker.Position.ToString()].ToString() },
                        { Speaker.Speak, lTempSpeak },
                        { Speaker.Character, item[Speaker.Character.ToString()].ToString() }
                    });
                }

                foreach (var item in lSentences) lTempSentences.Add(item.ToString());

                _end = new Dictionary<FieldCutscenes, object>()
                {
                    { FieldCutscenes.Played, lEnd[FieldCutscenes.Played.ToString()] },
                    { FieldCutscenes.Speaker, lTempSpeaker },
                    { FieldCutscenes.Sentences, lTempSentences }
                };
            }
            else _end = null;
        }

        private Dictionary<FieldCutscenes, object>
            _begining,
            _end;

        public List<bool> HasPlayed 
        { 
            get 
            {
                List<bool> lTemp = new List<bool>();

                if (_begining != null) lTemp.Add(Convert.ToBoolean(_begining[FieldCutscenes.Played]));
                else lTemp.Add(true);

                if (_end != null) lTemp.Add(Convert.ToBoolean(_end[FieldCutscenes.Played]));
                else lTemp.Add(true);

                return lTemp;
            } 
        }
    
        public List<List<string>> Sentences_Character
        {
            get
            {
                int lIndex;
                List<List<string>> ltemp = new List<List<string>>();

                ltemp.Add(new List<string>());

                lIndex = Sentences[0].Count;

                for (int i = 0; i < lIndex; i++) ltemp[ltemp.Count - 1].Add("");

                for (int i = 0; i < lIndex; i++)
                {
                    foreach (Dictionary<Speaker, object> speaker in _begining[FieldCutscenes.Speaker] as List<Dictionary<Speaker, object>>)
                    {
                        if ((speaker[Speaker.Speak] as List<int>).Contains(i)) ltemp[ltemp.Count - 1][i] = speaker[Speaker.Character].ToString(); 
                    }
                }

                ltemp.Add(new List<string>());

                lIndex = Sentences[1].Count;

                for (int i = 0; i < lIndex; i++) ltemp[ltemp.Count - 1].Add("");

                for (int i = 0; i < lIndex; i++)
                {
                    foreach (Dictionary<Speaker, object> speaker in _end[FieldCutscenes.Speaker] as List<Dictionary<Speaker, object>>)
                    {
                        if ((speaker[Speaker.Speak] as List<int>).Contains(i)) ltemp[ltemp.Count - 1][i] = speaker[Speaker.Character].ToString();
                    }
                }

                return ltemp;
            }
        }
        
        public List<List<string>> Sentences_Position
        {
            get
            {
                int lIndex;
                List<List<string>> ltemp = new List<List<string>>();

                ltemp.Add(new List<string>());

                lIndex = Sentences[0].Count;

                for (int i = 0; i < lIndex; i++) ltemp[ltemp.Count - 1].Add("");

                for (int i = 0; i < lIndex; i++)
                {
                    foreach (Dictionary<Speaker, object> speaker in _begining[FieldCutscenes.Speaker] as List<Dictionary<Speaker, object>>)
                    {
                        if ((speaker[Speaker.Speak] as List<int>).Contains(i)) ltemp[ltemp.Count - 1][i] = speaker[Speaker.Position].ToString();
                    }
                }

                ltemp.Add(new List<string>());

                lIndex = Sentences[1].Count;

                for (int i = 0; i < lIndex; i++) ltemp[ltemp.Count - 1].Add("");

                for (int i = 0; i < lIndex; i++)
                {
                    foreach (Dictionary<Speaker, object> speaker in _end[FieldCutscenes.Speaker] as List<Dictionary<Speaker, object>>)
                    {
                        if ((speaker[Speaker.Speak] as List<int>).Contains(i)) ltemp[ltemp.Count - 1][i] = speaker[Speaker.Position].ToString();
                    }
                }

                return ltemp;
            }
        }

        public List<List<string>> Sentences
        {
            get
            {
                List<List<string>> ltemp = new List<List<string>>();
                ltemp.Add(new List<string>());
                foreach (string sentence in _begining[FieldCutscenes.Sentences] as List<string>) ltemp[ltemp.Count - 1].Add(sentence);
                ltemp.Add(new List<string>());
                foreach (string sentence in _end[FieldCutscenes.Sentences] as List<string>) ltemp[ltemp.Count - 1].Add(sentence);
                return ltemp;
            }
        }
    }
}
