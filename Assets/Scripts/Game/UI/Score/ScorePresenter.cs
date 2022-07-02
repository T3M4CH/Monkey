using TMPro;
using Zenject;
using Channels;
using UnityEngine;
using Game.Tree.Branch;

namespace Game.UI.Score
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private float _heightRecord = 0;
        private float _height;
        private ScoreModel _model;
        private IChannel<BranchBase> _branchChannel;

        [Inject]
        private void Construct(IChannel<BranchBase> branchChannel)
        {
            _branchChannel = branchChannel;
        }

        private void AddScore(BranchBase branch)
        {
            _height = branch.transform.position.y;
            if (_height <= _heightRecord) return;
            _model.Score += 10;
            _heightRecord = _height;
            scoreText.text = _model.Score.ToString();
        }

        private void OnEnable()
        {
            _branchChannel.Subscribe(AddScore);
            _model = new ScoreModel();
            _model.Initialize();
        }

        private void OnDisable()
        {
            _branchChannel.Unsubscribe(AddScore);
        }
    }
}