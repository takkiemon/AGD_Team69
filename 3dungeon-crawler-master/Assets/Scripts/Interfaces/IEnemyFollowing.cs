using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// All enemies that follow the player (or any GameObject) implement this
    /// </summary>
    public interface IEnemyFollowing
    {
        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="movement">Vector describing required movement relative to current transform</param>
        void Move(Vector3 movement);
        
        /// <summary>
        /// Rotate the enemy
        /// </summary>
        /// <param name="rotation">Quaternion describing the new rotation</param>
        void Rotate(Quaternion rotation);
    }
}