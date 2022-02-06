using System.Collections.Generic;

namespace Fps.Common
{
    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}