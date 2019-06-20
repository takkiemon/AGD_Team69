using UnityEngine;

namespace BehaviourControllers
{
    /// <summary>
    /// Handles the calculations for main camera's movement
    /// </summary>
    public class CameraMovementBehaviourController
    {

        /// <summary>
        /// Offset between the camera and the object to follow
        /// </summary>
        public readonly Vector3 Offset;
        private readonly float _interpolation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="offset">Vector3 offset between the object to follow and object to move</param>
        /// <param name="interpolation">Smoothness of transitions</param>
        public CameraMovementBehaviourController(Vector3 offset, float interpolation)
        {
            Offset = offset;
            _interpolation = interpolation;
        }

        /// <summary>
        /// Calculates new relative position
        /// </summary>
        /// <param name="currentPosition">Current position of the object to move</param>
        /// <param name="objectToFollowPosition">Current position of the object to follow</param>
        /// <param name="deltaTime">Seconds passed since last call</param>
        /// <returns>Vector3 object describing the new relative position</returns>
        public Vector3 CalculatePosition(Vector3 currentPosition, Vector3 objectToFollowPosition, float deltaTime)
        {
            var newPosition = new Vector3
            {
                x = Mathf.Lerp(currentPosition.x, objectToFollowPosition.x + Offset.x,
                    _interpolation * deltaTime),
                y = currentPosition.y, // We don't want the camera to change altitude
                z = Mathf.Lerp(currentPosition.z, objectToFollowPosition.z + Offset.z,
                    _interpolation * deltaTime)
            };
            return newPosition;
        }
    }
}