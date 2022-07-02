using Channels;
using UnityEngine;

namespace Game.Tree.Branch
{
    public abstract class BranchBase : MonoBehaviour
    {
        [SerializeField] protected BranchChannel OnGrab;
        protected Rigidbody PlayerRigidbody;

        public void Initialize(Rigidbody playerRigidbody)
        {
            PlayerRigidbody = playerRigidbody;
        }

        public abstract void MakeActive(BranchBase branchBase);
    }
}