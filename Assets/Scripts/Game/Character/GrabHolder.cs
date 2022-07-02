using System;
using Zenject;
using Channels;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Game.Tree.Branch;

namespace Game.Character
{
    public class GrabHolder : MonoBehaviour
    {
        private event Action<BranchBase> OnGrabAction;

        [SerializeField] private IChannel<BranchBase> _branchChannel;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform playerModel;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float grabRadius = 0.75f;

        private Sequence _sequence;

        [Inject]
        private void Construct(IChannel<BranchBase> branchChannel)
        {
            _branchChannel = branchChannel;
        }

        public void Grab()
        {
            var branch = Physics
                .OverlapSphere(playerModel.position, grabRadius, layerMask)
                .Select(x => x.GetComponent<BranchBase>()).FirstOrDefault(x => x != null);
            if (branch == null) return;
            _branchChannel.Fire(branch);
        }

        private void PlayGrabAnimation(Transform branch)
        {
            transform.eulerAngles = new Vector3(0, branch.eulerAngles.y + 180, 0);
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(() =>
            {
                playerModel.DOMoveX(branch.position.x, animationDuration)
                    .SetEase(Ease.OutBounce);
                playerModel.DOMoveZ(branch.position.z, animationDuration)
                    .SetEase(Ease.OutBounce);
                transform.DOMoveY(branch.position.y - 1, animationDuration)
                    .SetEase(Ease.OutBounce);
            }).OnComplete(() => { _sequence.Kill(); });
        }

        private void Start()
        {
            OnGrabAction = x => PlayGrabAnimation(x.transform);
            _branchChannel.Subscribe(OnGrabAction);
        }

        private void OnDestroy()
        {
            _branchChannel.Unsubscribe(OnGrabAction);
        }
    }
}