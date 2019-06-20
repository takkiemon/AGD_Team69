using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Player and Enemy implement this, as they have health and can be killed
    /// </summary>
    public interface IObjectWithHealth
    {
        /// <summary>
        /// Called when the tint of the object should change
        /// Objects that have health turn red when they are hit, and back to normal after a short time
        /// </summary>
        /// <param name="newColor">The Color the material of the object should use</param>
        void ChangeColor(Color newColor);
        
        /// <summary>
        /// Called when the object dies (health reaches 0)
        /// </summary>
        void Die();
    }
}