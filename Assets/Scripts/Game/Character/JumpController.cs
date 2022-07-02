using System;
using Zenject;
using Channels;
using UnityEngine;
using Game.Tree.Branch;

namespace Game.Character
{
    public class JumpController : MonoBehaviour
    {
        private event Action<BranchBase> CatchAction;
        public event Action OnJump = () => { }; 
        
        [SerializeField] private Rigidbody characterRigidbody;

        private IChannel<BranchBase> _branchChannel;
        private float _yPos;

        [Inject]
        private void Construct(IChannel<BranchBase> branchChannel)
        {
            _branchChannel = branchChannel;
        }
        
        public void BeginStretch()
        {
            _yPos = transform.position.y;
        }

        public void Stretch(float force)
        {
            if (!characterRigidbody.isKinematic) return;
            transform.position -= Vector3.down * force;
            transform.position = new Vector3(transform.position.x,
                Mathf.Clamp(transform.position.y, _yPos - 0.5f, _yPos), transform.position.z);
        }

        public void AddForce()
        {
            OnJump.Invoke();
            characterRigidbody.velocity = Vector3.zero;
            characterRigidbody.AddForce(Vector3.up * JumpForce(_yPos, transform.position.y), ForceMode.Impulse);
        }


        private void Catch(BranchBase branch)
        {
            characterRigidbody.isKinematic = true;
            characterRigidbody.velocity = Vector3.zero;
            _yPos = transform.position.y;
        }

        private void Start()
        {
            _branchChannel.Subscribe(Catch);
        }

        private void OnDestroy()
        {
            _branchChannel.Unsubscribe(Catch);
        }

        private static float JumpForce(float start, float end) => (-end + start) * 20;
    }
}