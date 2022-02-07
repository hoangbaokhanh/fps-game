using System;
using UnityEngine;

namespace Fps.UI
{
    public class ScreenContainer : MonoBehaviour
    {
        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            
        }
    }
}