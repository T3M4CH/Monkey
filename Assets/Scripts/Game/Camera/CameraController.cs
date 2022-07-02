using System;
using Zenject;
using Channels;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Game.Tree.Branch;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float offset = 5;
        [SerializeField] private Transform target;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private float _branchHeight = 0;
        private IChannel<BranchBase> _branchChannel;
        private IChannel<bool> _boolChannel;

        [Inject]
        private void Construct(IChannel<BranchBase> branchChannel, IChannel<bool> boolChannel)
        {
            _branchChannel = branchChannel;
            _boolChannel = boolChannel;
        }
        
        private void Reset()
        {
            _boolChannel.Fire(false);
            virtualCamera.enabled = false;
            transform.rotation =
                Quaternion.Euler(transform.rotation.x, target.rotation.eulerAngles.y, transform.rotation.z);
            transform.position =
                new Vector3(transform.position.x, Mathf.Max(1, transform.position.y), transform.position.z);
            enabled = false;
        }

        private void SetHeight(BranchBase branch)
        {
            _branchHeight = branch.transform.position.y;
        }

        private void FixedUpdate()
        {
            if (target.position.y < _branchHeight - offset) Reset();
        }

        private void OnEnable()
        {
            _branchChannel.Subscribe(SetHeight);
        }

        private void OnDisable()
        {
            _branchChannel.Unsubscribe(SetHeight);
        }
    }
}