using BehaviourControllers;
using NUnit.Framework;

namespace Editor
{
    // Tests for EnemyAttackBehaviourController
    public class EnemyAttackTest
    {
        // Testing that CanAttack works
        [Test]
        public void CanAttackWorks()
        {
            var controller = new EnemyAttackBehaviourController(0.5f);
            
            Assert.False(controller.CanAttack());
            
            controller.Update(0.5f);
            Assert.True(controller.CanAttack());
            
            controller.Update(0.3f);
            Assert.False(controller.CanAttack());
            
            controller.Update(0.3f);
            Assert.True(controller.CanAttack());
            
            Assert.False(controller.CanAttack());
            
            controller.Update(0.2f);
            controller.Update(3f);
            Assert.True(controller.CanAttack());
        }
    }
}