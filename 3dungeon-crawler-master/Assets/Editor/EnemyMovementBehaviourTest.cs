using BehaviourControllers;
using Interfaces;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor
{
    // Tests for EnemyMovementBehaviourController
    public class EnemyMovementBehaviourTest
    {
        // Values for testing

        private const int MaxRange = 5;
        private const float SpeedOfMovement = 1.0f;
        private const float SpeedOfTurn = 0.15f;

        // Test that when player is out of range, Move is not called
        [Test]
        public void NoMoveCalledWhenOutOfRange()
        {
            var enemyMock = GetEnemyFollowingMock();
            var controller = GetEnemyMovementMock(enemyMock);
            var start = new Vector3(3.7f, 1, -10.0f);
            var rotation = new Quaternion();
            var playerLocation = new Vector3(start.x + MaxRange, start.y, start.z);

            controller.Update(start, rotation, playerLocation, 0.5f);

            enemyMock.DidNotReceiveWithAnyArgs().Move(new Vector3());
        }

        // Test that when player is out of range, Rotate is not called
        [Test]
        public void NoRotateCalledWhenOutOfRange()
        {
            var enemyMock = GetEnemyFollowingMock();
            var controller = GetEnemyMovementMock(enemyMock);
            var start = new Vector3(3.7f, 1, -10.0f);
            var rotation = new Quaternion();
            var playerLocation = new Vector3(start.x + MaxRange, start.y, start.z);

            controller.Update(start, rotation, playerLocation, 0.5f);

            enemyMock.DidNotReceiveWithAnyArgs().Rotate(new Quaternion());
        }

        // Test that Move is called with a proper argument
        [Test]
        public void MoveCalledWithGoodArgument()
        {
            var enemyMock = GetEnemyFollowingMock();
            var controller = GetEnemyMovementMock(enemyMock);
            var start = new Vector3(3.7f, 1, -10.0f);
            var rotation = new Quaternion();
            var playerLocation = new Vector3(start.x + 1f, start.y, start.z);

            controller.Update(start, rotation, playerLocation, 0.5f);

            enemyMock.Received().Move(Arg.Is<Vector3>(x => x.x > 0f));
        }

        // Test that Rotate is called with a proper argument
        [Test]
        public void RotateCalledWithGoodArgument()
        {
            var enemyMock = GetEnemyFollowingMock();
            var controller = GetEnemyMovementMock(enemyMock);
            var start = new Vector3(3.7f, 1, -10.0f);
            var rotation = new Quaternion();
            var playerLocation = new Vector3(start.x + 1f, start.y, start.z + 1f);

            controller.Update(start, rotation, playerLocation, 0.5f);

            enemyMock.Received().Rotate(Arg.Is<Quaternion>(x => x != rotation));
        }

        // Test that move is called the exact number of times it should be
        [Test]
        public void MoveCalledRightNumberOfTimes()
        {
            var enemyMock = GetEnemyFollowingMock();
            var controller = GetEnemyMovementMock(enemyMock);
            var start = new Vector3();
            var rotation = new Quaternion();

            // No call here
            var playerLocation = new Vector3(MaxRange, 1f, 0f);
            controller.Update(start, rotation, playerLocation, 0.5f);

            // Call expected
            playerLocation = new Vector3(MaxRange / 2f, 1f, MaxRange / 2f);
            controller.Update(start, rotation, playerLocation, 0.5f);

            // Call expected
            playerLocation = new Vector3(0f, 1f, 0f);
            controller.Update(start, rotation, playerLocation, 0.5f);

            // No call here
            playerLocation = new Vector3(-MaxRange*2, 1f, 0f);
            controller.Update(start, rotation, playerLocation, 0.5f);

            enemyMock.ReceivedWithAnyArgs(2).Move(new Vector3());
        }

        // Mocks

        private static EnemyMovementBehaviourController GetEnemyMovementMock(IEnemyFollowing mock)
        {
            return new EnemyMovementBehaviourController(mock, MaxRange, SpeedOfMovement, SpeedOfTurn);
        }

        private static IEnemyFollowing GetEnemyFollowingMock()
        {
            return Substitute.For<IEnemyFollowing>();
        }
    }
}