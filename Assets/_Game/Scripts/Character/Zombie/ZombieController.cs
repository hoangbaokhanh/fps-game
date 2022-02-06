using Fps.Animation;
using Fps.Common;
using UnityEngine;
using UnityEngine.AI;

namespace Fps.Character.Zombie
{
    public class ZombieController : MonoBehaviour
    {
        [SerializeField] private float baseHealth;
        [SerializeField] private float movementSpeed;
        
        [SerializeField] private ZombieAnimationController animationController;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private GameObject player;
        

        private float health;

        private void Start()
        {
            agent.speed = movementSpeed;
            health = baseHealth;
        }

        public bool IsAlive() => health > 0;
        
        public void TakeDamage(Vector3 position, float damage)
        {
            if (IsAlive())
            {
                health -= damage;
                
                // TODO: spawn blood particle here

                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.IsPlayer())
            {
                Attack();
            }
        }

        public void Attack()
        {
            animationController.Attack();
        }

        public void FindPlayer()
        {
            
        }

        public void Update()
        {
            agent.SetDestination(player.transform.position);
            animationController.Walk();
        }
    }
}