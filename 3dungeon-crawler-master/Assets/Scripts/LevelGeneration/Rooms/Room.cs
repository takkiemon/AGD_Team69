using System;

namespace LevelGeneration.Rooms
{
    /// <summary>
    /// Base class for Room objects
    /// </summary>
    public class Room
    {
        /// <summary>
        /// X position in a two-dimensional array
        /// </summary>
        public readonly int X;

        /// <summary>
        /// Z position in a two-dimensional array
        /// </summary>
        public readonly int Z;

        /// <summary>
        /// Orientation as int between 0-3. 0 means default (south), 1 means west (turned 90 degrees) and so on.
        /// </summary>
        public readonly int Orientation;

        /// <summary>
        /// Amount of enemies in this room
        /// </summary>
        public int Enemies;

        /// <summary>
        /// Whether this room is the beginning of the level. Currently level always starts with a 4-way intersection.
        /// </summary>
        public bool Beginning;

        /// <summary>
        /// Whether this room is the 'end' of the level, so whether is has a ladder or not.
        /// Currently only dead end rooms are eligible to hold ladders.
        /// </summary>
        public bool End;

        /// <summary>
        /// Whether there is a room to south of this room, so whether the southern wall needs to be hidden on generation.
        /// </summary>
        public bool HideSouthernWall;

        /// <summary>
        /// Whether there is a room to west of this room, so whether the western wall needs to be hidden on generation.
        /// </summary>
        public bool HideWesternWall;

        protected Room(int x, int z, int orientation)
        {
            X = x;
            Z = z;
            Orientation = orientation;
            Enemies = 0;
            Beginning = false;
            End = false;
            HideSouthernWall = false;
            HideWesternWall = false;
        }

        /// <summary>
        /// Checks if the room has a doorway to the specified direction. Implemented in children.
        /// </summary>
        /// <param name="direction">Direction to check. 0 is south, 1 is west, and so on.</param>
        /// <returns>True if room has a doorway to the direction, false otherwise.</returns>
        /// <exception cref="Exception">If trying to call from Room. Should call from children.</exception>
        public virtual bool HasDoorwayTo(int direction)
        {
            // Implement in children
            throw new Exception("All child rooms have to implement HasDoorwayTo(int direction)!");
        }
    }
}