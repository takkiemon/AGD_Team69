namespace BehaviourControllers
{
    /// <summary>
    /// Handles logic behind enemy attacks (timer)
    /// </summary>
    public class EnemyAttackBehaviourController
    {
        private readonly float _minTimeBetweenAttacks;
        private float _attackTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minTime">The minimum time between attacks this enemy makes</param>
        public EnemyAttackBehaviourController(float minTime)
        {
            _minTimeBetweenAttacks = minTime;
        }

        /// <summary>
        /// Called from the owner GameObject so time-sensitive issues can be controlled
        /// </summary>
        /// <param name="deltaTime">Seconds passed since last call</param>
        public void Update(float deltaTime)
        {
            _attackTimer += deltaTime;
        }

        /// <summary>
        /// Check if attacking is allowed
        /// If it is, an attack is assumed, and the timer is reset
        /// </summary>
        /// <returns>True if attack is allowed, false otherwise</returns>
        public bool CanAttack()
        {
            if (_attackTimer < _minTimeBetweenAttacks) return false;

            _attackTimer = 0f;
            return true;
        }
    }
}