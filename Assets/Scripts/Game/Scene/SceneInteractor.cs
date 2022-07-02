namespace Game.Scene
{
    public class SceneInteractor
    {
        public SceneInteractor(SceneModel sceneModel)
        {
            _sceneModel = sceneModel;
        }
        private SceneModel _sceneModel;

        public void AddLevel()
        {
            _sceneModel.Save(LevelId + 1);
        }
        public int LevelId => _sceneModel.LevelId;
    }
}