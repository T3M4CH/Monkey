using System;
using Channels;
using UnityEngine;
using Game.Tree.Branch;
using Zenject;

namespace Game.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        private event Action<BranchBase> OnGrabAction;
        private event Action OnJumpAction;

        [SerializeField] private JumpController jumpController;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite jumpSprite, grabSprite;

        private IChannel<BranchBase> _branchChannel;

        [Inject]
        private void Construct(IChannel<BranchBase> branchChannel)
        {
            _branchChannel = branchChannel;
        }

        private void ChangeState(bool isJumping)
        {
            spriteRenderer.sprite = isJumping ? jumpSprite : grabSprite;
        }

        private void OnEnable()
        {
            OnJumpAction = () => { ChangeState(true); };
            OnGrabAction = _ => { ChangeState(false); };
            jumpController.OnJump += OnJumpAction;
            _branchChannel.Subscribe(OnGrabAction);
        }

        private void OnDisable()
        {
            jumpController.OnJump -= OnJumpAction;
            _branchChannel.Unsubscribe(OnGrabAction);
        }
    }
}