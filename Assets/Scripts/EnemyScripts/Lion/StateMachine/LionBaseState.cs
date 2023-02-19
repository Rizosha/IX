using UnityEngine;

namespace EnemyScripts.Lion
{
    public abstract class LionBaseState
    {
        public abstract void EnterState(LionStateManager lion);
    
        public abstract void UpdateState(LionStateManager lion);
    
        public abstract void OnCollisionEnter(LionStateManager lion, Collision collision);
    }
}
