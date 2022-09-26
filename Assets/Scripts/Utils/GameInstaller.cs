using Clicker.Managers;
using UnityEngine;
using Zenject;

namespace Clicker.Utils
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BalanceManager balanceManager;
        [SerializeField] private SaveCallback saveCallback;
        
        public override void InstallBindings()
        {
            Container.BindInstance(balanceManager).AsSingle();
            Container.BindInstance(saveCallback).AsSingle();
        }
    }    
}

