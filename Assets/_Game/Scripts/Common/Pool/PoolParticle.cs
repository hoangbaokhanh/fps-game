namespace Fps.Common.Pool
{
    public class PoolParticle : PoolObject
    {
        public void OnParticleSystemStopped()
        {
            Done();
        }
    }
}