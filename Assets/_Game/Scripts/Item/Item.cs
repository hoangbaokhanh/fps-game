using Fps.Character.Player;
using Fps.Common;
using Fps.Message;
using UniRx;
using UnityEngine;

namespace Fps.Item
{
    public enum EItem
    {
        MedicBag,
        Battery,
        Fish,
        WaterBottle
    }
    
    public class Item : MonoBehaviour
    {
        [SerializeField] private EItem itemKind;
        private void OnTriggerEnter(Collider other)
        {
            if (other.IsPlayer())
            {
                var player = other.GetComponent<PlayerController>();
                if (player)
                {
                    MessageBroker.Default.Publish(new PickupItem()
                    {
                        Item = itemKind
                    });
                    Destroy(gameObject);
                }
            }
        }
    }
}