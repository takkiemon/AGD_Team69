using LevelGeneration.Rooms;
using NUnit.Framework;

namespace Editor
{
	// Tests for Room and its children
	public class RoomTest
	{
		// Testing that CornerRoom can be initialized, and HasDoorwayTo works
		[Test]
		public void CornerRoomWorks()
		{
			var room = new CornerRoom(8,1,2);
			
			Assert.True(room.HasDoorwayTo(2));
			Assert.True(room.HasDoorwayTo(3));
		}
		
		// Testing that CorridorRoom can be initialized, and HasDoorwayTo works
		[Test]
		public void CorridorRoomWorks()
		{
			var room = new CorridorRoom(3,10,3);
			
			Assert.True(room.HasDoorwayTo(3));
			Assert.True(room.HasDoorwayTo(1));
		}
		
		// Testing that DeadENdRoom can be initialized, and HasDoorwayTo works
		[Test]
		public void DeadEndRoomWorks()
		{
			var room = new DeadEndRoom(2,5,1);
			
			Assert.True(room.HasDoorwayTo(1));
		}
		
		// Testing that FourWayRoom can be initialized, and HasDoorwayTo works
		[Test]
		public void FourWayRoomWorks()
		{
			var room = new FourWayRoom(11,9,3);
			
			Assert.True(room.HasDoorwayTo(0));
			Assert.True(room.HasDoorwayTo(1));
			Assert.True(room.HasDoorwayTo(2));
			Assert.True(room.HasDoorwayTo(3));
		}
		
		// Testing that ThreeWayRoom can be initialized, and HasDoorwayTo works
		[Test]
		public void ThreeWayRoomWorks()
		{
			var room = new ThreeWayRoom(0,4,0);
			
			Assert.True(room.HasDoorwayTo(0));
			Assert.True(room.HasDoorwayTo(1));
			Assert.True(room.HasDoorwayTo(3));
		}
		
		// Testing that can set room as end or beginning
		[Test]
		public void RoomCanBeEndOrBeginning()
		{
			var room = new DeadEndRoom(8,8,0);

			room.Beginning = true;
			
			Assert.True(room.Beginning);
			Assert.False(room.End);

			room.Beginning = false;
			room.End = true;
			
			Assert.True(room.End);
			Assert.False(room.Beginning);
		}
		
		// Testing that can set number of enemies
		[Test]
		public void RoomCanHaveEnemies()
		{
			var room = new CorridorRoom(10,10,2);

			Assert.That(room.Enemies == 0);

			room.Enemies = 4;
			
			Assert.That(room.Enemies == 4);
		}
	}
}