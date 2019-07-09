using BehaviourControllers;
using Interfaces;
using UnityEngine;

namespace GameObjectControllers
{
    /// <summary>
    /// Controls Ghost enemies
    /// </summary>
    public class GhostController : MonoBehaviour, IObjectWithHealth, IEnemyFollowing
    {
        private const int Damage = 20;

        private HealthAndDyingBehaviourController _healthAndDying;
        private EnemyMovementBehaviourController _movement;
        private EnemyAttackBehaviourController _attacking;

        /// <summary>
        /// The Player GameObject
        /// </summary>
        public GameObject Player;

        private void Start()
        {
            _healthAndDying = new HealthAndDyingBehaviourController(this, new Color(1f, 1f, 1f, 0.5f),
                new Color(1f, 0.8f, 0.8f, 0.5f), 40, 0.3f);
            _movement = new EnemyMovementBehaviourController(this, 3, 1.0f, 0.15f);
            _attacking = new EnemyAttackBehaviourController(1f);
            Player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            _healthAndDying.Update(Time.deltaTime);
            // No check for death here since if we died, we should be destroyed by now
            _movement.Update(transform.position, transform.rotation, Player.transform.position, Time.deltaTime);
            _attacking.Update(Time.deltaTime);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && _attacking.CanAttack())
            {
                Player.GetComponent<PlayerController>().GetHit(Damage);
            }
        }
        
        // Health and dying

        /// <summary>
        /// Called from other GameObjects hitting this object (namely, the player's sword)
        /// </summary>
        /// <param name="damage"></param>
        public void GetHit(int damage)
        {
            _healthAndDying.GetHit(damage, true);
        }

        /// <inheritdoc />
        public void ChangeColor(Color newColor)
        {
            gameObject.GetComponent<Renderer>().material.color = newColor;
        }

        /// <inheritdoc />
        public void Die()
        {
            Destroy(gameObject);
        }
        
        // Movement

        /// <inheritdoc />
        public void Move(Vector3 movement)
        {
            transform.Translate(movement, Space.World);
        }

        /// <inheritdoc />
        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}