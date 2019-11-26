using System;
using LevelGeneration;
using LevelGeneration.Rooms;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameObjectControllers
{
    /// <summary>
    /// Controller of GameMaster - the workhorse
    /// All public variables set in inspector
    /// </summary>
    public class GameMasterController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private int _currentLevel;
        private GameObject _currentLevelObject;
        private bool _gameOver;

        // Variables set in inspector
        public GameObject DeadEndRoom;
        public GameObject CorridorRoom;
        public GameObject CornerRoom;
        public GameObject ThreeWayRoom;
        public GameObject FourWayRoom;
        public GameObject Ladder;
        public GameObject Ghosts2;
        public GameObject Ghosts4;
        public GameObject Player;
        public GameObject MainCamera;
        public GameObject BlankCamera;
        public GameObject PausedTintPlane;
        public GameObject PlayingUi;
        public GameObject PausedUi;
        public GameObject GameOverUi;
        public Text GameOverLevel;
        public Text Level;

        private void Start()
        {
            // Set scene to dark
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = Color.black;
            
            // Generate the first level
            SetViewBlank();
            _levelGenerator = new LevelGenerator();
            _currentLevel = 1;
            _gameOver = false;
            GenerateLevel();
            
            // Start the game
            SetViewNormal();
            Level.text = "Level: " + 1;
        }

        private void Update()
        {
            if (_gameOver)
            {
                // Restart the game if game is over and r pressed
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                return;
            }
            
            // Check for pause
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (Time.timeScale.Equals(1.0f))
                {
                    Pause();
                    return;
                }
                else
                {
                    UnPause();
                }
            }
            
            // Check for game over
            if (Player.GetComponent<PlayerController>().Dead)
            {
                GameOver();
            }
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            PausedTintPlane.SetActive(true);
            PlayingUi.SetActive(false);
            PausedUi.SetActive(true);
        }

        private void UnPause()
        {
            PausedUi.SetActive(false);
            PlayingUi.SetActive(true);
            PausedTintPlane.SetActive(false);
            Time.timeScale = 1.0f;
        }

        private void GameOver()
        {
            PausedTintPlane.SetActive(true);
            PlayingUi.SetActive(false);
            GameOverUi.SetActive(true);
            GameOverLevel.text = "Reached level " + _currentLevel + "!";
            _gameOver = true;
        }

        // For turning the screen black during level generation
        private void SetViewBlank()
        {
            MainCamera.SetActive(false);
            BlankCamera.SetActive(true);
        }

        private void SetViewNormal()
        {
            BlankCamera.SetActive(false);
            MainCamera.SetActive(true);
        }

        /// <summary>
        /// LadderController calls this when the player steps into a ladder
        /// </summary>
        public void LevelUp()
        {
            SetViewBlank();
            //_currentLevel++;
            Destroy(_currentLevelObject);
            GenerateLevel();
            SetViewNormal();
            Level.text = "Level: " + _currentLevel;
        }

        private void GenerateLevel()
        {
            // Use LevelGenerator to generate the level
            _currentLevelObject = new GameObject();
            Room[,] level = _levelGenerator.GenerateLevel(-1, _currentLevel);
            
            // Create the rooms
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int z = 0; z < level.GetLength(1); z++)
                {
                    Room room = level[x, z];
                    if (room == null) continue;
                    Vector3 position = new Vector3
                    {
                        x = ((0 - level.GetLength(0) / 2) + room.X) * 10,
                        z = ((0 - level.GetLength(1) / 2) + room.Z) * 10
                    };
                    Quaternion rotation = Quaternion.Euler(0, 90 * room.Orientation, 0);
                    GameObject builtRoom;
                    switch (room.GetType().Name)
                    {
                        case "FourWayRoom":
                            builtRoom = Instantiate(FourWayRoom, position, rotation);
                            break;
                        case "CorridorRoom":
                            builtRoom = Instantiate(CorridorRoom, position, rotation);
                            break;
                        case "CornerRoom":
                            builtRoom = Instantiate(CornerRoom, position, rotation);
                            break;
                        case "ThreeWayRoom":
                            builtRoom = Instantiate(ThreeWayRoom, position, rotation);
                            break;
                        case "DeadEndRoom":
                            builtRoom = Instantiate(DeadEndRoom, position, rotation);
                            break;
                        default:
                            throw new Exception("Trying to add a room not yet implemented. Room name was: " +
                                                room.GetType().Name);
                    }

                    // Overlapping walls cause a glitch so we disable all overlapping walls
                    // What makes this ugly is that because we're rotating the rooms, we have to check which walls are the new southern and western walls
                    // TODO: Make prettier?
                    if (room.HideSouthernWall)
                    {
                        switch (room.Orientation)
                        {
                            case 0:
                                builtRoom.transform.Find("Wall_south").gameObject.SetActive(false);
                                break;
                            case 1:
                                builtRoom.transform.Find("Wall_east").gameObject.SetActive(false);
                                break;
                            case 2:
                                builtRoom.transform.Find("Wall_north").gameObject.SetActive(false);
                                break;
                            case 3:
                                builtRoom.transform.Find("Wall_west").gameObject.SetActive(false);
                                break;
                        }
                    }

                    if (room.HideWesternWall)
                    {
                        switch (room.Orientation)
                        {
                            case 0:
                                builtRoom.transform.Find("Wall_west").gameObject.SetActive(false);
                                break;
                            case 1:
                                builtRoom.transform.Find("Wall_south").gameObject.SetActive(false);
                                break;
                            case 2:
                                builtRoom.transform.Find("Wall_east").gameObject.SetActive(false);
                                break;
                            case 3:
                                builtRoom.transform.Find("Wall_north").gameObject.SetActive(false);
                                break;
                        }
                    }

                    // Placing ladders
                    if (room.End)
                    {
                        Vector3 ladderPosition = position;
                        ladderPosition.y = 0.1f;
                        Instantiate(Ladder, ladderPosition, new Quaternion()).transform.parent = builtRoom.transform;
                    }

                    GameObject enemies = null;
                    // Placing enemies
                    switch (room.Enemies)
                    {
                        case 0:
                            break;
                        case 2:
                            enemies = Instantiate(Ghosts2, position, new Quaternion());
                            break;
                        case 4:
                            enemies = Instantiate(Ghosts4, position, new Quaternion());
                            break;
                    }

                    if (enemies != null)
                    {
                        enemies.transform.parent = builtRoom.transform;
                    }

                    // Place the player in the start of dungeon
                    if (room.Beginning)
                    {
                        Vector3 playersPosition = position;
                        // Rooms are generated at y=0; Player's origin is at 1
                        playersPosition.y = 1;
                        Player.transform.position = playersPosition;
                    }

                    // We set enemies and ladders as children of the room, and rooms as children of the leve object,
                    // so we can easily delete the entire level when switching levels
                    builtRoom.transform.parent = _currentLevelObject.transform;
                }
            }
        }
    }
}