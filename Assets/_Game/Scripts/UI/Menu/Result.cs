using TMPro;
using UnityEngine;

namespace Fps.UI.Menu
{
    public class Result : ScreenContainer
    {
        [SerializeField] private TMP_Text status;

        public void SetWinStatus(bool win)
        {
            status.text = win ? "You Win" : "You Lose";
        }
    }
}