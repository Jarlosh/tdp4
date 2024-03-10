using TDPF.FuckUp;
using TDPF.FuckUp.DialogueSystem;
using TDPF.Player.Inventory;
using UnityEngine;
using Utils;
using Zenject;

namespace System.Installers
{
    public class FuckUpInstaller: MonoInstaller
    {
        [SerializeField] private DialogConfig dialogConfig;
        [SerializeField] private SoundsConfig soundsConfig;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindFuckUp();
            BindSubSystems();


            Container.Bind<InventoryController>().AsSingle();
        }

        private void BindSubSystems()
        {
            Container.BindInterfacesAndSelfTo<DialogueSubsystem>().AsSingle().NonLazy();
            Container.Bind<DialogueQueue>().AsSingle();
        }

        private void BindFuckUp()
        {
            Container.BindInterfacesAndSelfTo<FuckUpController>().AsSingle().NonLazy();
        }

        private void BindConfigs()
        {
            Container.BindInstances(
                dialogConfig,
                soundsConfig);
        }
    }
}