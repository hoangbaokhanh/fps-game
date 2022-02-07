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
        

        public ScreenContainer PushMenu(string menu)
        {
            var menuResource = menuResources.Find(m => m.Name == menu);
            if (menuResource != null)
            {
                var menuObj = Instantiate(menuResource.Prefab);
                var menuComp = menuObj.GetComponent<ScreenContainer>();
                menuStack.Push(menuComp);
                return menuComp;
            }

            throw new Exception("Cannot init menu");
        }

        public ScreenContainer ReplaceMenu(string menu)
        {
            var menuResource = menuResources.Find(m => m.Name == menu);
            if (menuResource != null)
            {
                var menuObj = Instantiate(menuResource.Prefab);
                var menuComp = menuObj.GetComponent<ScreenContainer>();
                menuStack.Replace(menuComp);
                return menuComp;
            }
            throw new Exception("Cannot init menu");
        }

        public void Pop()
        {
            if (menuStack.Size > 0)
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