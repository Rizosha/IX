using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.Lion
{
    public class LionStateManager : MonoBehaviour
    {
        private LionBaseState currentState;
        public LionIdleState IdleState = new LionIdleState();
        public LionChaseState ChaseState = new LionChaseState();
        public LionAttackState AttackState = new LionAttackState();
        public LionDamageState DamageState = new LionDamageState();
    
        public Animator animator, playerAnimator;
        public Transform player;
        public NavMeshAgent navMeshAgent;
        public EnemyDamageScript damageScript;
        public LionPlayerDamage lionPlayerDamage;
        public bool dead;
        public float stopDistance;
    
        private void Awake() { 
            animator = GetComponent<Animator>(); 
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerAnimator = player.GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            stopDistance = navMeshAgent.stoppingDistance;
        }
    
        void Start() {
            //starting state for the state machine
            currentState = ChaseState;
            currentState.EnterState(this);
        }

        private void OnCollisionEnter(Collision collision) {
            currentState.OnCollisionEnter(this, collision);
        }

        void Update() {
            currentState.UpdateState(this);
        }

        public void SwitchState(LionBaseState state) {
            currentState = state;
            state.EnterState(this);
        }
    }
}
