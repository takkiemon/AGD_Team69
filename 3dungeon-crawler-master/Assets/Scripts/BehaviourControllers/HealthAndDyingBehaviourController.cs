using Interfaces;
using UnityEngine;

namespace BehaviourControllers
{
    /// <summary>
    /// Controls the health and dying of objects implementing IObjectWithHealth
    /// </summary>
    public class HealthAndDyingBehaviourController
    {
        private readonly IObjectWithHealth _objectWithHealth;

        private readonly Color _normalColor;
        private readonly Color _hurtingColor;
        private readonly float _damageTakingTimerMax;
        private float _timeSinceLastHit;

        /// <summary>
        /// Current health of the object
        /// </summary>
        public int Health;
        /// <summary>
        /// Indicator of current aliveness
        /// </summary>
        public bool Dead;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objectWithHealth">The object to control</param>
        /// <param name="normalColor">Shader when status is 'normal'</param>
        /// <param name="hurtingColor">Shader when status is 'hurting'</param>
        /// <param name="maxHealth">Maximum health</param>
        /// <param name="damageTakingTimerMax">How often this object is allowed to take damage</param>
        public HealthAndDyingBehaviourController(IObjectWithHealth objectWithHealth, Color normalColor,
            Color hurtingColor, int maxHealth, float damageTakingTimerMax)
        {
            _objectWithHealth = objectWithHealth;
            _normalColor = normalColor;
            _hurtingColor = hurtingColor;
            Health = maxHealth; // Assuming we start at full health
            Dead = false; // Assuming we start alive
            _damageTakingTimerMax = damageTakingTimerMax;
            _timeSinceLastHit = damageTakingTimerMax;
        }

        /// <summary>
        /// When object is getting hit and should take damage
        /// </summary>
        /// <param name="damage">Amount of damage object should take</param>
        /// <param name="useTimer">If true, a timer is used to avoid continous damage</param>
        /// <returns>True if damage was taken, false otherwise</returns>
        public bool GetHit(int damage, bool useTimer)
        {
            if (Dead) return false;

            if (useTimer)
            {
                if (_timeSinceLastHit < _damageTakingTimerMax)
                {
                    return false;
                }
            }

            Health -= damage;
            if (Health <= 0)
            {
                Dead = true;
                _objectWithHealth.Die();
                return true;
            }

            _objectWithHealth.ChangeColor(_hurtingColor);
            _timeSinceLastHit = 0f;
            return true;
        }

        /// <summary>
        /// Master object should call Update so the controller can be in charge of time-sensitive things
        /// </summary>
        /// <param name="deltaTime">Time since last call</param>
        public void Update(float deltaTime)
        {
            if (Dead) return;
            _timeSinceLastHit += deltaTime;
            if (_timeSinceLastHit >= _damageTakingTimerMax && _timeSinceLastHit - deltaTime < _damageTakingTimerMax)
            {
                // Changing from hurting to normal
                _objectWithHealth.ChangeColor(_normalColor);
            }
        }
    }
}