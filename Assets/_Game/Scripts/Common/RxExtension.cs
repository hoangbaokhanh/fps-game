using System;
using UniRx;

namespace Fps.Common
{
    public static class RxExtensions
    {
        public static IObservable<SnapShot<TSource, TSource>> PairWithPrevious<TSource>(this IObservable<TSource> source)
        {
            return source.Scan(new SnapShot<TSource, TSource>(default, default), (acc, current) => new SnapShot<TSource, TSource>(acc.Item2, current));
        }
    }
}