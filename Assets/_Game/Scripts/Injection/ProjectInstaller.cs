using System;
using Fps.Input;
using UnityEngine;
using Zenject;

namespace Fps.Injection
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject eventSystem;
        [SerializeField] private GameObject rewiredManager;
        
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
            
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle().NonLazy();
        }
    }
}