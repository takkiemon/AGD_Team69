namespace LevelGeneration.Rooms
{
	/// <summary>
	/// Corridor. In orientation 0, has doorway to 0 and 2.
	/// </summary>
	public class CorridorRoom : Room
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x">Location in a two-dimensional array</param>
		/// <param name="z">Location in a two-dimensional array</param>
		/// <param name="orientation">Int 0-3. 0 is to south (default), 1 is to west (90 degrees turned) and so on.</param>
		public CorridorRoom(int x, int z, int orientation) : base(x, z, orientation)
		{
		}

		/// <summary>
		/// Checks if the room has a doorway to the specified direction
		/// </summary>
		/// <param name="direction">Direction to check. 0 is south, 1 is west, and so on.</param>
		/// <returns>True if room has a doorway to the direction, false otherwise.</returns>
		public override bool HasDoorwayTo(int direction)
		{
			// A corridor has a doorway to the direction it's 'coming' from. and to the opposite direction.
			return (direction == Orientation || direction == (Orientation + 2) % 4);
		}
	}
}