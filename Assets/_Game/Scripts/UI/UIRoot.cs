using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fps.UI
{
    [Serializable]
    public class MenuResource
    {
        public string Name;
        public GameObject Prefab;
    }
    
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private MenuStack menuStack;
        [SerializeField] private List<MenuResource> menuResources;
        
        private Dictionary<Type, string> menuDict = new Dictionary<Type, string>();
        

        public void PushMenu(string menu)
        {
            var menuResource = menuResources.Find(m => m.Name == menu);
            if (menuResource != null)
            {
                var menuObj = Instantiate(menuResource.Prefab);
                var menuComp = menuObj.GetComponent<ScreenContainer>();
                menuStack.Push(menuComp);
            }
        }

        public void ReplaceMenu(string menu)
        {
            var menuResource = menuResources.Find(m => m.Name == menu);
            if (menuResource != null)
            {
                var menuObj = Instantiate(menuResource.Prefab);
                var menuComp = menuObj.GetComponent<ScreenContainer>();
                menuStack.Replace(menuComp);
            }
        }

        public void Pop()
        {
            if (menuStack.Size > 1)
            {
                menuStack.Pop();
            }
            else
            {
                //error
            }
        }
    }
}