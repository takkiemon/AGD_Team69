using BehaviourControllers;
using NUnit.Framework;
using UnityEngine;

namespace Editor
{
    // Tests for CameraController
    public class CameraBehaviourTest
    {
        // Testing that new position is not same as previous, if should move
        [Test]
        public void CalculatePositionIsDifferentFromOriginal()
        {
            CameraMovementBehaviourController movement =
                new CameraMovementBehaviourController(new Vector3(0, 10, 0),0.5f);
            Vector3 currentPosition = new Vector3(10, 11, 5);
            Vector3 playerPosition = new Vector3(11, 1, 4);
            float deltaTime = 0.01f;

            Vector3 newPosition = movement.CalculatePosition(currentPosition, playerPosition, deltaTime);

            Assert.AreNotEqual(currentPosition, newPosition);
        }

        // Testing that going in the correct direction
        [Test]
        public void CalculatePositionGoesInCorrectDirection()
        {
            CameraMovementBehaviourController movement =
                new CameraMovementBehaviourController(new Vector3(0, 10, 0), 0.5f);
            Vector3 currentPosition = new Vector3(0, 11, 0);
            Vector3 playerPosition = new Vector3(0, 1, 2);
            float deltaTime = 0.01f;

            Vector3 newPosition = movement.CalculatePosition(currentPosition, playerPosition, deltaTime);

            Assert.IsTrue(newPosition.z > 0);
        }

        // Testing that camera is slower than the object we're following
        [Test]
        public void CalculatePositionNotTooFast()
        {
            CameraMovementBehaviourController movement =
                new CameraMovementBehaviourController(new Vector3(0, 10, 0), 0.5f);
            Vector3 currentPosition = new Vector3(0, 11, 0);
            Vector3 playerPosition = new Vector3(0, 1, 2);
            float deltaTime = 0.01f;

            Vector3 newPosition = movement.CalculatePosition(currentPosition, playerPosition, deltaTime);

            Assert.IsTrue(newPosition.z < 2);
        }
    }
}