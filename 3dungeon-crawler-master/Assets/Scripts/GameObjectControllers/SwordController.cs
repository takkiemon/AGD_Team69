using UnityEngine;

namespace GameObjectControllers
{
    /// <summary>
    /// Controller of player's sword
    /// </summary>
    public class SwordController : MonoBehaviour
    {
        private const int Damage = 20;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Swings the sword. GetHit gets called on all enemies hit
        /// </summary>
        public void Attack()
        {
            _animator.SetTrigger("Sword_attack");
        }

        private void OnTriggerEnter(Collider colliderOfSecondObject)
        {
            // If the hit object has the Enemy tag, and the swinging animation is playing, we hit the object
            // Possible because of multiple contact points, OnTriggerEnter gets called repeatedly. Handle this in GetHit.
            if (colliderOfSecondObject.gameObject.CompareTag("Enemy") &&
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_swing"))
                colliderOfSecondObject.gameObject.GetComponent<GhostController>().GetHit(Damage);
        }
    }
}