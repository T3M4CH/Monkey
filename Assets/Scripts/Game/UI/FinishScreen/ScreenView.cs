using TMPro;
using Zenject;
using Channels;
using UnityEngine;

namespace Game.UI.FinishScreen
{
    public class ScreenView : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public TextMeshProUGUI recordText;
        public TextMeshProUGUI buttonText;
        
        
        private FinishScreen _finishScreen;
        private IChannel<bool> _boolChannel;

        [Inject]
        private void Construct(IChannel<bool> boolChannel)
        {
            _boolChannel = boolChannel;
        }
        private void Start()
        {
            _finishScreen = new FinishScreen(_boolChannel, this);
        }

        private void OnDestroy()
        {
            _finishScreen?.Dispose();
        }
    }
}