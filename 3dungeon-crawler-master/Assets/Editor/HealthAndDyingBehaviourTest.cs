using BehaviourControllers;
using Interfaces;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor
{
    // Tests for HealthAndDyingBehaviourController
    public class HealthAndDyingBehaviourTest
    {
        // Values for testing

        private const int MaxHealth = 100;
        private const float DamageTakingTimerMax = 0.5f;

        // Test that object starts at full health
        [Test]
        public void FullHealthAtStart()
        {
            var healthAndDying = GetHealthAndDyingMock(GetObjectWithHealthMock());

            int health = healthAndDying.Health;

            Assert.AreEqual(MaxHealth, health);
        }

        // Test that object starts alive
        [Test]
        public void AliveAtStart()
        {
            var healthAndDying = GetHealthAndDyingMock(GetObjectWithHealthMock());

            Assert.IsFalse(healthAndDying.Dead);
        }

        // Test that Die gets called when dying
        [Test]
        public void DieGetsCalled()
        {
            var objectMock = GetObjectWithHealthMock();
            var healthAndDying = GetHealthAndDyingMock(objectMock);

            healthAndDying.GetHit(MaxHealth, false);

            // Easiest way I found how to check if a method was called. Works like an assert.
            objectMock.Received().Die();
        }

        // Test that, when using timer to prevent continous damage, no extra damage is taken
        [Test]
        public void DamageTakingTimerWorks()
        {
            var objectMock = GetObjectWithHealthMock();
            var healthAndDying = GetHealthAndDyingMock(objectMock);

            healthAndDying.GetHit(MaxHealth / 2, true);
            healthAndDying.Update(DamageTakingTimerMax / 2);
            healthAndDying.GetHit(MaxHealth, true);

            objectMock.DidNotReceive().Die();
        }

        // Test that hurting invokes color change
        [Test]
        public void ColorChangesToHurtingWhenHit()
        {
            var objectMock = GetObjectWithHealthMock();
            var healthAndDying = GetHealthAndDyingMock(objectMock);

            healthAndDying.GetHit(MaxHealth / 2, true);

            objectMock.Received().ChangeColor(Color.red);
        }

        // Test that color returns back to normal
        [Test]
        public void ColorChangesBackToNormal()
        {
            var objectMock = GetObjectWithHealthMock();
            var healthAndDying = GetHealthAndDyingMock(objectMock);

            healthAndDying.GetHit(MaxHealth / 2, true);
            healthAndDying.Update(DamageTakingTimerMax * 2);

            objectMock.Received().ChangeColor(Color.white);
        }

        // Mocks

        private static HealthAndDyingBehaviourController GetHealthAndDyingMock(IObjectWithHealth mock)
        {
            return new HealthAndDyingBehaviourController(mock, Color.white, Color.red, MaxHealth, DamageTakingTimerMax);
        }

        private static IObjectWithHealth GetObjectWithHealthMock()
        {
            return Substitute.For<IObjectWithHealth>();
        }
    }
}