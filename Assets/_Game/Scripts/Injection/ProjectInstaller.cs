using UnityEngine;
using Zenject;

namespace Fps.Injection
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("Injection init");
        }
    }
}