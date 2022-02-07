using System.Collections.Generic;
using Fps.UI;

namespace Fps.Common
{
    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        public static void RemoveLast<T>(this IList<T> list)
        {
            list.RemoveAt(list.Count -1);
        }

        public static void ReplaceLast<T>(this IList<T> list, T item)
        {
            list.Add(item);
            list.RemoveAt(list.Count - 2);
        }
        
        public static void ReplaceLast<T>(this IList<T> list, T item, bool cleanup) where T : ScreenContainer
        {
            list.Add(item);
            list[list.Count - 2].Destroy();
            list.RemoveAt(list.Count - 2);
        }
    }
}