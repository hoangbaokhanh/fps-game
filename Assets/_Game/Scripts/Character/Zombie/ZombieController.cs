using Cysharp.Threading.Tasks;
using Fps.Animation;
using Fps.Character.Player;
using Fps.Common;
using Fps.Gameplay;
using Fps.Message;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Fps.Character.Zombie
{
    public class ZombieController : MonoBehaviour
    {
        [SerializeField] private int baseHealth;
        [SerializeField] private int movementSpeed;
        [SerializeField] private int damage;
        [SerializeField] private Transform bloodSpawn;

        [SerializeField] private ZombieAnimationController animationController;
        [SerializeField] private NavMeshAgent agent;
        private PlayerController player;

        [Inject] private GameManager gameManager;
        [Inject] private VfxManager vfxManager;

        private int health;
        
        private void Start()
        {
            agent.speed = movementSpeed;
            health = baseHealth;
            WaitForPlayer().Forget();
        }

        public bool IsAlive() => health > 0;

        public void TakeDamage(int damage)
        {
            if (IsAlive())
            {
                health -= damage;

                vfxManager.PlayBloodVfx(bloodSpawn.position);

                if (health <= 0)
                {
                    MessageBroker.Default.Publish(new ZombieDie()
                    {
                        Position = transform.position
                    });
                    Destroy(gameObject);
                }
            }
        }

        public async UniTaskVoid WaitForPlayer()
        {
            await UniTask.WaitUntil(() => gameManager.GetPlayer() != null);
            player = gameManager.GetPlayer();
            Observable.EveryUpdate().Subscribe(_ => FindPlayer()).AddTo(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.IsPlayer())
            {
                var _player = other.GetComponent<PlayerController>();
                if (_player)
                {
                    _player.TakeDamage(damage);
                }
            }
        }

        private void FindPlayer()
        {
            if (player != null && IsAgentOnNavMesh(gameObject))
            {
                var success = agent.SetDestination(player.transform.position);
                if (!success)
                {
                    Debug.LogError("Failed to find destination " + gameObject.name);
                }
                animationController.Walk();
            }
        }
        
        float onMeshThreshold = 2;

        public bool IsAgentOnNavMesh(GameObject agentObject)
        {
            Vector3 agentPosition = agentObject.transform.position;
            NavMeshHit hit;

            // Check for nearest point on navmesh to agent, within onMeshThreshold
            if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
            {
                // Check if the positions are vertically aligned
                if (Mathf.Approximately(agentPosition.x, hit.position.x)
                    && Mathf.Approximately(agentPosition.z, hit.position.z))
                {
                    // Lastly, check if object is below navmesh
                    return agentPosition.y >= hit.position.y;
                }
            }

            return false;
        }
    }
}