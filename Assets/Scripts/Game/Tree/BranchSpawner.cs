using Game.Tree.Branch;
using UnityEngine;
using Utils;

namespace Game.Tree
{
    public class BranchSpawner : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private float minXdistance;
        [SerializeField] private float maxXdistance;
        [SerializeField] private float minYdistance;
        [SerializeField] private float maxYdistance;
        [SerializeField] private int frequencyOfSpawn = 3;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private BranchBase fragileBranch;
        [SerializeField] private BranchBase defaultBranch;
        [SerializeField] private TrunkSpawner trunkSpawner;


        private bool _spawnCondition;
        private int _trunkIndex = 0;
        private float _angle = 180;
        private float _treeHeight = 31;

        private void CreateBranches()
        {
            float height = transform.position.y - 2;
            for (int i = 0; height < _treeHeight; i++)
            {
                _spawnCondition = i % frequencyOfSpawn == 0;
                if (height > trunkSpawner.trunks[_trunkIndex].position.y)
                {
                    _trunkIndex += 1;
                    _trunkIndex = Mathf.Clamp(_trunkIndex, 0, 9);
                }

                height = GetHeight(SpawnBranch(defaultBranch, height, _angle, _trunkIndex).transform);
                if (_spawnCondition)
                {
                    SpawnBranch(fragileBranch, height, _angle + 180, _trunkIndex);
                }

                _angle = GetAngle(_angle);
            }
        }

        private Transform SpawnBranch(BranchBase template, float height, float angle, int trunkIndex)
        {
            BranchBase branch = Instantiate(template, new Vector3(transform.position.x, height, transform.position.z),
                Quaternion.Euler(90, 0, angle));
            branch.Initialize(playerRigidbody);
            var branchTransform = branch.transform;
            branchTransform.position += branchTransform.up * radius;
            branchTransform.parent = trunkSpawner.trunks[trunkIndex];
            return branchTransform;
        }

        private float GetAngle(float angle)
        {
            return RandomDirection ? XSpread + angle : -XSpread + angle;
        }

        private float GetHeight(Transform priorBranch)
        {
            return priorBranch.position.y + YSpread;
        }

        private void Start()
        {
            CreateBranches();
        }

        private float YSpread => Random.Range(minYdistance, maxYdistance);
        private float XSpread => Random.Range(minXdistance, maxXdistance);
        private bool RandomDirection => Random.Range(0, 2) > 0;
    }
}