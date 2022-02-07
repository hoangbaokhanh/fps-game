using System;
using System.Collections.Generic;
using System.Linq;
using Fps.Common;
using UnityEngine;

namespace Fps.UI
{
    public class MenuStack : MonoBehaviour
    {
        protected List<ScreenContainer> stack = new List<ScreenContainer>();

        public ScreenContainer Top => stack.Last();
        public int Size => stack.Count;

        public virtual void Push(ScreenContainer menu)
        {
            stack.Add(menu);
            if (stack.Count > 1)
            {
                stack[stack.Count - 2].Deactivate();
            }

            Top.transform.SetParent(transform);
        }

        public void PushBackground(ScreenContainer menu)
        {
            var index = Mathf.Max(stack.Count - 1, 0);
            stack.Insert(index, menu);
            stack[index].transform.SetParent(transform);
        }

        public void Replace(ScreenContainer menu)
        {
            if (stack.Count > 0)
            {
                stack.ReplaceLast(menu, true);
            }
            else
            {
                stack.Add(menu);
            }

            Top.transform.SetParent(transform);
        }

        public virtual void Pop()
        {
            if (stack.Count > 1)
            {
                stack[stack.Count - 2].Active();
            }

            Destroy(Top.gameObject);
            stack.RemoveLast();
        }

        public void PopAllAbove(Type menuType)
        {
            var menuIndex = stack.FindIndex(menu => menu.GetType() == menuType);
            stack[menuIndex].Active();
            for (var i = stack.Count - 1; i > menuIndex; --i)
            {
                Destroy(stack[i].gameObject);
                stack.RemoveAt(i);
            }
        }

        public void PopToBottom()
        {
            if (stack.Count > 1)
            {
                for (int i = stack.Count - 1; i > 0; i--)
                {
                    Destroy(stack[i].gameObject);
                    stack.RemoveAt(i);
                }
            }
        }

        public void PopAllMenu<T>()
        {
            if (stack.Count > 0)
            {
                foreach (var item in stack)
                {
                    if (item is T)
                    {
                        Destroy(item.gameObject);
                        stack.Remove(item);
                    }
                }
            }
        }
    }
}