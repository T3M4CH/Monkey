namespace Game.Tree.Branch
{
    public class FragileBranch : BranchBase
    {
        private bool _isCurrentBranch;

        public override void MakeActive(BranchBase branch)
        {
            if (branch == this)
            {
                _isCurrentBranch = true;
                Destroy(gameObject, 3);
            }
            else
            {
                _isCurrentBranch = false;
            }
        }

        private void Start()
        {
            OnGrab.Subscribe(MakeActive);
        }

        private void OnDestroy()
        {
            if (_isCurrentBranch && PlayerRigidbody != null) PlayerRigidbody.isKinematic = false;
            OnGrab.Unsubscribe(MakeActive);
        }
    }
}