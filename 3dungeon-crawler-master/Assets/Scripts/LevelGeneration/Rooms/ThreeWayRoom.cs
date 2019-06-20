namespace LevelGeneration.Rooms
{
	/// <summary>
	/// Intersection, three doorways. In orientation 0, has a doorway to 0, 1 and 3.
	/// </summary>
	public class ThreeWayRoom : Room
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x">Location in a two-dimensional array</param>
		/// <param name="z">Location in a two-dimensional array</param>
		/// <param name="orientation">Int 0-3. 0 is to south (default), 1 is to west (90 degrees turned) and so on.</param>
		public ThreeWayRoom(int x, int z, int orientation) : base(x, z, orientation)
		{
		}

		/// <summary>
		/// Checks if the room has a doorway to the specified direction
		/// </summary>
		/// <param name="direction">Direction to check. 0 is south, 1 is west, and so on.</param>
		/// <returns>True if room has a doorway to the direction, false otherwise.</returns>
		public override bool HasDoorwayTo(int direction)
		{
			// A 3 way room has a doorway to the specified direction, and +1 and -1 (which is same as +3).
			return direction == Orientation || direction == (Orientation + 1) % 4 || direction == (Orientation + 3) % 4;
		}
	}
}