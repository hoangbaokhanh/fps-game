using System.Collections.Generic;
using Fps.Common;
using Fps.Item;
using UnityEngine;

namespace Fps.Gameplay
{
    [CreateAssetMenu(fileName = "CollectItem", menuName = "Fps/Collect-Item", order = 0)]
    public class CollectItemQuest : ScriptableObject
    {
        [SerializeField] private List<EItem> items;
        
        public EItem GetItemTarget()
        {
            return items.Random();
        }
    }
}