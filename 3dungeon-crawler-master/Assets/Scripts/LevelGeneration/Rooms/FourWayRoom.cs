namespace LevelGeneration.Rooms
{
    /// <summary>
    /// Intersection, four doorways. Has a doorway to every direction in all orientations.
    /// </summary>
    public class FourWayRoom : Room
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">Location in a two-dimensional array</param>
        /// <param name="z">Location in a two-dimensional array</param>
        /// <param name="orientation">Int 0-3. 0 is to south (default), 1 is to west (90 degrees turned) and so on.</param>
        public FourWayRoom(int x, int z, int orientation) : base(x, z, orientation)
        {
        }


        /// <summary>
        /// Checks if the room has a doorway to the specified direction
        /// </summary>
        /// <param name="direction">Direction to check. 0 is south, 1 is west, and so on.</param>
        /// <returns>True if room has a doorway to the direction, false otherwise.</returns>
        public override bool HasDoorwayTo(int direction)
        {
            // A 4 way room has a doorway in every direction
            return true;
        }
    }
}