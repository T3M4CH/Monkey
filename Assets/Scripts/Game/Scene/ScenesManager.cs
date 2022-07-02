using Zenject;
using Channels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.Scene
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private SceneInteractor _sceneInteractor;
        private SceneModel _sceneModel;
        private IChannel<bool> _boolChannel;

        [Inject]
        private void Construct(IChannel<bool> boolChannel)
        {
            _boolChannel = boolChannel;
            _boolChannel.Subscribe(SetButtonAssignment);
        }
        private void Start()
        {
            _sceneModel = new SceneModel();
            _sceneModel.Initialize();
            _sceneInteractor = new SceneInteractor(_sceneModel);
        }

        private void SetButtonAssignment(bool isWinner)
        {
            if (isWinner)
            {
                button.onClick.AddListener(OnPlayerWin);
            }
            else
            {
                button.onClick.AddListener(OnPlayerLose);
            }
            _boolChannel.Unsubscribe(SetButtonAssignment);
        }
        private void OnPlayerWin()
        {
            _sceneInteractor.AddLevel();
            SceneManager.LoadScene(0);
        }
        private static void OnPlayerLose()
        {
            SceneManager.LoadScene(0);
        }

    }
}