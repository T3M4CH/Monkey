using UnityEngine;

namespace Game.Scene
{
    public class SceneModel
    {
        private const string LevelKey = "levelKey";
        public void Initialize()
        {
            LevelId = PlayerPrefs.GetInt(LevelKey, 1);
        }

        public void Save(int value)
        {
            LevelId = value;
            PlayerPrefs.SetInt(LevelKey, LevelId);
        }
        public int LevelId { get; private set; }
    }
}