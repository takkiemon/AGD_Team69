namespace LevelGeneration.Rooms
{
	/// <summary>
	/// Corner. In orientation 0, has doorway to 0 and 1.
	/// </summary>
	public class CornerRoom : Room
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x">Location in a two-dimensional array</param>
		/// <param name="z">Location in a two-dimensional array</param>
		/// <param name="orientation">Int 0-3. 0 is to south (default), 1 is to west (90 degrees turned) and so on.</param>
		public CornerRoom(int x, int z, int orientation) : base(x, z, orientation)
		{
		}

		/// <summary>
		/// Checks if the room has a doorway to the specified direction
		/// </summary>
		/// <param name="direction">Direction to check. 0 is south, 1 is west, and so on.</param>
		/// <returns>True if room has a doorway to the direction, false otherwise.</returns>
		public override bool HasDoorwayTo(int direction)
		{
			// Corner has a doorway to the direction it's 'coming' from, and the next direction (+1)
			return direction == Orientation || direction == (Orientation + 1) % 4;
		}
	}
}