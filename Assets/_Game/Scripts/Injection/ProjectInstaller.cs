using System;
using Fps.Common;
using Fps.Gameplay;
using Fps.Input;
using Fps.Setting;
using UnityEngine;
using Zenject;

namespace Fps.Injection
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject eventSystem;
        [SerializeField] private GameObject rewiredManager;
        [SerializeField] private AudioController audioController;
        [SerializeField] private GameObject poolManager;
        [SerializeField] private VfxManager vfxManager;
         
        public override void InstallBindings()
        {
            Debug.Log("Injection init");
            
            // bind prefabs
            Container.Bind<Transform>().WithId(Guid.NewGuid())
                .FromComponentInNewPrefab(rewiredManager)
                .WithGameObjectName(rewiredManager.name)
                .UnderTransform(null as Transform)
                .AsCached()
                .NonLazy();
            
            Container.Bind<Transform>().WithId(Guid.NewGuid())
                .FromComponentInNewPrefab(eventSystem)
                .WithGameObjectName(eventSystem.name)
                .UnderTransform(null as Transform)
                .AsCached()
                .NonLazy();
            
            
            Container.Bind<Transform>().WithId(Guid.NewGuid())
                .FromComponentInNewPrefab(poolManager)
                .WithGameObjectName(poolManager.name)
                .UnderTransform(null as Transform)
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle().NonLazy();
            Container.Bind<AudioController>().FromComponentInNewPrefab(audioController).AsSingle().NonLazy();
            Container.Bind<VfxManager>().FromComponentInNewPrefab(vfxManager).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            
        }
    }
}