using System;
using Fps.Common;
using Fps.Gameplay;
using Fps.Input;
using Fps.Setting;
using Fps.UI;
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
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Spawner spawner;
        [SerializeField] private UIRoot uiRoot;
         
        public override void InstallBindings()
        {
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
            Container.Bind<Spawner>().FromComponentInNewPrefab(spawner).AsSingle().NonLazy();
            Container.Bind<GameManager>().FromComponentInNewPrefab(gameManager).AsSingle().NonLazy();
            Container.Bind<UIRoot>().FromComponentInNewPrefab(uiRoot).AsSingle().NonLazy();
        }
    }
}