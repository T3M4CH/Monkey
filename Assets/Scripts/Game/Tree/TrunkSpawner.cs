using System;
using Zenject;
using Channels;
using UnityEngine;
using Game.Tree.Branch;
using Random = UnityEngine.Random;

namespace Game.Tree
{
    public class TrunkSpawner : MonoBehaviour
    {
        public Transform[] trunks = new Transform[10];

        [SerializeField] private GameObject trunkTemplate;

        private int _previousId = 0;
        private int _divisionFactor = 3;
        private IChannel<BranchBase> _branchChannel;

        [Inject]
        private void Construct(IChannel<BranchBase> branchChannel)
        {
            _branchChannel = branchChannel;
        }

        private Vector3 GetPosition(Transform currentSegment)
        {
            return new Vector3(currentSegment.position.x,
                currentSegment.position.y + 3,
                currentSegment.position.z);
        }

        private void CalculateHeight(BranchBase branch)
        {
            int id = 0;
            float minDistance = 5;
            for (int i = 0; i < 10; i++)
            {
                if (Mathf.Abs(trunks[i].transform.position.y - branch.transform.position.y) < minDistance)
                {
                    minDistance = Mathf.Abs(trunks[i].transform.position.y - branch.transform.position.y);
                    id = i;
                }
            }

            while (_previousId != id)
            {
                _previousId++;
                if (_previousId > 9)
                {
                    _previousId = 0;
                }
                trunks[CalculateIndex(_previousId - trunks.Length / 2)].position = GetPosition(trunks[CalculateIndex(_previousId + trunks.Length / 2 - 1)]);
                trunks[CalculateIndex(_previousId - trunks.Length / 2)].rotation = Quaternion.Euler(-90, Random.Range(-180, 180), 0);
            }

            _previousId = id;
        }

        private int CalculateIndex(int value)
        {
            if (value < 0)
            {
                return trunks.Length + value;
            }

            if (value > 9)
            {
                return (value % 9) - 1;
            }

            return value;
        }

        private void Awake()
        {
            _branchChannel.Subscribe(CalculateHeight);
            trunks[9] = transform;
            for (int i = 0; i < 10; i++)
            {
                trunks[i] = Instantiate(trunkTemplate, GetPosition(trunks[CalculateIndex(i - 1)]),
                    Quaternion.Euler(-90, 0, 0), transform).transform;
            }
        }

        private void OnDisable()
        {
            _branchChannel.Unsubscribe(CalculateHeight);
        }

        public int Height => 36;
    }
}