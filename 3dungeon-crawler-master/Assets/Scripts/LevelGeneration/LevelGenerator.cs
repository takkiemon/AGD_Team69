using System;
using LevelGeneration.Rooms;
using Random = System.Random;

namespace LevelGeneration
{
    /// <summary>
    /// Generates randomized levels
    /// </summary>
    public class LevelGenerator
    {
        // Directions
        private const int South = 0;
        private const int West = 1;
        private const int North = 2;
        private const int East = 3;

        // Number codes of types of rooms
        private const int Dead = 10;
        private const int Corridor = 11;
        private const int Corner = 12;
        private const int ThreeWay = 13;
        private const int FourWay = 14;

        // Changeable values
        private int _edgeSize;
        private int _difficulty;
        private bool _hasEnd;
        private Room[,] _level;
        private readonly Random _random;
        
        // Debug
        private readonly int _seed;

        /// <summary>
        /// Constructor
        /// </summary>
        public LevelGenerator()
        {
            _random = new Random();
            _seed = -1;
        }

        /// <summary>
        /// Debug constructor
        /// </summary>
        /// <param name="seed">Seed for Random</param>
        public LevelGenerator(int seed)
        {
            _random = new Random(seed);
            _seed = seed;
        }

        /// <summary>
        /// Generates a complete level, as a two-dimensional array
        /// </summary>
        /// <param name="edgeSize">Max size of the edges of the level. Set -1 to have size determined by difficulty</param>
        /// <param name="difficulty">Used in generating enemies and size of the level (if edgeSize was -1). 1 is easiest</param>
        /// <returns>A two-dimensional array of Room objects</returns>
        /// <exception cref="Exception">When trying to generate a level with edge size smaller than 3</exception>
        public Room[,] GenerateLevel(int edgeSize, int difficulty)
        {
            // Setup
            if (edgeSize == -1)
            {
                _edgeSize = 3 + difficulty * 2;
            }
            else if (edgeSize < 3)
            {
                throw new Exception("Cannot generate level smaller than 3x3");
            }
            else
            {
                _edgeSize = edgeSize;
            }

            _difficulty = difficulty;
            _hasEnd = false;
            _level = new Room[_edgeSize, _edgeSize];

            // Choose starting place
            int x = _random.Next(1, _edgeSize - 1);
            int z = _random.Next(1, _edgeSize - 1);
            _level[x, z] = new FourWayRoom(x, z, 0) {Beginning = true};

            // Starting the recursive generation
            ContinueFrom(_level[x, z]);

            // Generation finished, return room
            return _level;
        }

        private void ContinueFrom(Room room)
        {
            int x = room.X;
            int z = room.Z;
            int direction = _random.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (room.HasDoorwayTo(direction % 4) && GetRoomInDirection(room, direction % 4) == null)
                {
                    // Generating a room
                    switch (direction % 4)
                    {
                        case South:
                            Generate(x, z - 1, North);
                            break;
                        case West:
                            Generate(x - 1, z, East);
                            break;
                        case North:
                            Generate(x, z + 1, South);
                            break;
                        case East:
                            Generate(x + 1, z, West);
                            break;
                    }
                }

                direction++;
            }
        }

        private void Generate(int x, int z, int comingFrom)
        {
            Room room;
            while (true)
            {
                var shape = _random.Next(Dead, FourWay + 1);
                // Testing with every orientation
                var orientation = _random.Next(0, 4);
                for (int i = 0; i < 4; i++)
                {
                    room = GenerateRoom(x, z, orientation % 4, shape);
                    if (IsValid(room, comingFrom))
                    {
                        // Generation of room finished.
                        goto finish;
                    }

                    orientation++;
                }
            }

            finish:
            {
                _level[x, z] = room;
                // Here we disable overlapping walls to avoid glitches
                for (int direction = 0; direction < 4; direction++)
                {
                    if (GetRoomInDirection(room, direction) != null)
                    {
                        switch (direction)
                        {
                            case South:
                                room.HideSouthernWall = true;
                                break;
                            case West:
                                room.HideWesternWall = true;
                                break;
                            case North:
                                GetRoomInDirection(room, direction).HideSouthernWall = true;
                                break;
                            case East:
                                GetRoomInDirection(room, direction).HideWesternWall = true;
                                break;
                        }
                    }
                }

                // Generating a ladder room
                if (!_hasEnd && room.GetType().Name == "DeadEndRoom")
                {
                    room.End = true;
                    _hasEnd = true;
                }

                // Generating enemies
                if (!room.End)
                {
                    switch (_random.Next(_difficulty, 10 + _difficulty))
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            room.Enemies = 0;
                            break;
                        case 5:
                        case 6:
                        case 7:
                            room.Enemies = 2;
                            break;
                        default:
                            room.Enemies = 4;
                            break;
                    }
                }

                ContinueFrom(room);
            }
        }

        private static Room GenerateRoom(int x, int z, int orientation, int shape)
        {
            switch (shape)
            {
                case Dead:
                    return new DeadEndRoom(x, z, orientation);
                case Corridor:
                    return new CorridorRoom(x, z, orientation);
                case Corner:
                    return new CornerRoom(x, z, orientation);
                case ThreeWay:
                    return new ThreeWayRoom(x, z, orientation);
                case FourWay:
                    return new FourWayRoom(x, z, orientation);
                default:
                    throw new Exception("Trying to add a room not yet implemented");
            }
        }

        private bool IsValid(Room room, int comingFrom)
        {
            // Checking every direction
            for (int direction = 0; direction < 4; direction++)
            {
                if (comingFrom == direction)
                {
                    if (!room.HasDoorwayTo(direction)) return false;
                }
                else
                {
                    if (room.HasDoorwayTo(direction))
                    {
                        // Room has doorway to here
                        if (!DirectionIsLegal(room, direction))
                        {
                            // ... but we're out of bounds
                            return false;
                        }

                        if (GetRoomInDirection(room, direction) != null)
                        {
                            // ... but the path is already taken
                            // If the blocking door also has a nicely fitting doorway (to the opposite direction so they meet), this is a match
                            if (!GetRoomInDirection(room, direction).HasDoorwayTo((direction + 2) % 4))
                                return false;
                        }
                    }
                    else
                    {
                        // Room does not have a doorway to specific direction: Now we have to check that the neighbouring room is not trying to access this room
                        if (GetRoomInDirection(room, direction) != null)
                        {
                            // There is a room here
                            // If the other room has a doorway to where we're generating, this is not a match
                            if (GetRoomInDirection(room, direction).HasDoorwayTo((direction + 2) % 4)) return false;
                        }
                    }
                }
            }

            // Wow, we got this far. In that case the room is valid.
            return true;
        }

        private Room GetRoomInDirection(Room room, int direction)
        {
            if (!DirectionIsLegal(room, direction)) return null;
            switch (direction)
            {
                case South:
                    return _level[room.X, room.Z - 1];
                case North:
                    return _level[room.X, room.Z + 1];
                case West:
                    return _level[room.X - 1, room.Z];
                case East:
                    return _level[room.X + 1, room.Z];
                default:
                    // Direction not correct?
                    throw new Exception("Couldn't find room in direction" + direction);
            }
        }


        private bool DirectionIsLegal(Room room, int direction)
        {
            switch (direction)
            {
                case South:
                    return room.Z - 1 >= 0;
                case West:
                    return room.X - 1 >= 0;
                case North:
                    return room.Z + 1 < _edgeSize;
                case East:
                    return room.X + 1 < _edgeSize;
                default:
                    throw new Exception("Direction not valid: " + direction);
            }
        }
    }
}