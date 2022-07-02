using UnityEngine;

namespace Game.UI.Score
{
    public class ScoreModel
    {
        private const string RecordKey = "scoreKey";
        private int _record;
        private int _score;

        public void Initialize()
        {
            _record = PlayerPrefs.GetInt(RecordKey, 0);
        }

        private void Save()
        {
            _record = Score;
            PlayerPrefs.SetInt(RecordKey, Score);
        }
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                if (_score > _record)
                {
                    Save();
                }
            }
        }
    }
}