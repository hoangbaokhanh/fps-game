using System;

namespace Fps.Common
{
    public class SnapShot<T1, T2> : Tuple<T1, T2>
    {
        public SnapShot(T1 one, T2 two) :base(one, two) { }

        public T1 Previous { get{ return this.Item1; } }
        public T2 Current { get{ return this.Item2; } }
    }
}