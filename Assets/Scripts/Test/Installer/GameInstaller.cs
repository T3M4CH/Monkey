using Zenject;
using Channels;
using UnityEngine;

namespace Test.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BoolChannel boolChannel;
        [SerializeField] private BranchChannel branchChannel;
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BoolChannel>()
                .FromInstance(boolChannel)
                .AsSingle();

            Container
                .BindInterfacesTo<BranchChannel>()
                .FromInstance(branchChannel)
                .AsSingle();
        }
    }
}