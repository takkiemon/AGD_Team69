using Interfaces;
using UnityEngine;

namespace BehaviourControllers
{
    /// <summary>
    /// Controls the movement of enemies / objects implementing IEnemyFollowing
    /// </summary>
    public class EnemyMovementBehaviourController
    {
        private readonly IEnemyFollowing _enemy;
        private readonly int _maxRange;
        private readonly float _speedOfMovement;
        private readonly float _speedOfTurn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemy">The object to control</param>
        /// <param name="maxRange">When the object we're following is out of this range, no movement happens. Measured in Unity units</param>
        /// <param name="speedOfMovement">The speed of movement, units per second</param>
        /// <param name="speedOfTurn">The speed of turning. 1f is instant, 0.5f is somewhat smooth</param>
        public EnemyMovementBehaviourController(IEnemyFollowing enemy, int maxRange, float speedOfMovement,
            float speedOfTurn)
        {
            _enemy = enemy;
            _maxRange = maxRange;
            _speedOfMovement = speedOfMovement;
            _speedOfTurn = speedOfTurn;
        }

        /// <summary>
        /// Master object should call Update so the controller can calculate differences correctly
        /// </summary>
        /// <param name="currentPosition">Master object's position right now</param>
        /// <param name="currentRotation">Master object's rotation right now</param>
        /// <param name="objectToFollowPosition">The position of the object to follow</param>
        /// <param name="deltaTime">Time since last call</param>
        public void Update(Vector3 currentPosition, Quaternion currentRotation, Vector3 objectToFollowPosition,
            float deltaTime)
        {
            var heading = objectToFollowPosition - currentPosition;
            // Using sqrMagnitude to save processing power
            if (heading.sqrMagnitude < _maxRange * _maxRange)
            {
                // Object is within range! Let's get 'em!!
                var distance = heading.magnitude;
                var movement = heading / distance;
                _enemy.Move(movement * deltaTime * _speedOfMovement);
                _enemy.Rotate(Quaternion.Slerp(currentRotation, Quaternion.LookRotation(movement * -1), _speedOfTurn));
            }
        }
    }
}