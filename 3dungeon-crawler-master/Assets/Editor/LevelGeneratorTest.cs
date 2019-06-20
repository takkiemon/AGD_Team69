using System;
using LevelGeneration;
using LevelGeneration.Rooms;
using NUnit.Framework;

namespace Editor
{
    // Tests for LevelGenerator
    public class LevelGeneratorTest
    {
        // Testing that attempts at generating a level smaller than 3x3 throws exception
        [Test]
        public void SmallerThan3X3ThrowsException()
        {
            var generator = new LevelGenerator();

            Assert.Throws<Exception>(() => generator.GenerateLevel(2, 1));
        }

        // Testing that with big enough edge size, we get a level of said size
        [Test]
        public void GeneratedLevelIsCorrectSize()
        {
            var generator = new LevelGenerator();

            var level = generator.GenerateLevel(3, 1);

            // Testing both arrays since level is a two-dimensional array
            Assert.AreEqual(level.GetLength(0), 3);
            Assert.AreEqual(level.GetLength(1), 3);
        }

        // Testing that the known level has rooms of correct types
        [Test]
        public void LevelWithSeedHasCorrectRooms()
        {
            var room = GenerateLevel();

            Assert.AreEqual(room[0, 0].GetType(), typeof(DeadEndRoom));
            Assert.AreEqual(room[0, 1].GetType(), typeof(DeadEndRoom));
            Assert.AreEqual(room[0, 2], null);

            Assert.AreEqual(room[1, 0].GetType(), typeof(ThreeWayRoom));
            Assert.AreEqual(room[1, 1].GetType(), typeof(FourWayRoom));
            Assert.AreEqual(room[1, 2].GetType(), typeof(DeadEndRoom));

            Assert.AreEqual(room[2, 0].GetType(), typeof(DeadEndRoom));
            Assert.AreEqual(room[2, 1].GetType(), typeof(CornerRoom));
            Assert.AreEqual(room[2, 2].GetType(), typeof(DeadEndRoom));
        }

        // Testing that the known level has correct begin and end rooms, and only one of each
        [Test]
        public void LevelWithSeedHasCorrectBeginAndEnd()
        {
            var room = GenerateLevel();

            for (var x = 0; x < 3; x++)
            {
                for (var z = 0; z < 3; z++)
                {
                    if (x == 0 && z == 2)
                    {
                        // This room is null
                        Assert.That(room[x, z] == null);
                        continue;
                    }

                    if (x == 1 && z == 1) Assert.True(room[x, z].Beginning);
                    else Assert.False(room[x, z].Beginning);

                    if (x == 1 && z == 2) Assert.True(room[x, z].End);
                    else Assert.False(room[x, z].End);
                }
            }
        }

        // Testing that the known level has correct enemies
        [Test]
        public void LevelWithSeedHasCorrectEnemies()
        {
            var room = GenerateLevel();

            Assert.AreEqual(room[0, 0].Enemies, 2);
            Assert.AreEqual(room[0, 1].Enemies, 0);
            // Room [0,2] is null

            Assert.AreEqual(room[1, 0].Enemies, 0);
            Assert.AreEqual(room[1, 1].Enemies, 0);
            Assert.AreEqual(room[1, 2].Enemies, 0);

            Assert.AreEqual(room[2, 0].Enemies, 0);
            Assert.AreEqual(room[2, 1].Enemies, 4);
            Assert.AreEqual(room[2, 2].Enemies, 2);
        }

        // Generating a level with seed, the supposed layout is known (tested beforehand)
        private Room[,] GenerateLevel()
        {
            return new LevelGenerator(100).GenerateLevel(3, 1);
        }
    }
}