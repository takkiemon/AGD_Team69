using UnityEngine;
using BehaviourControllers;

namespace GameObjectControllers
{
    /// <summary>
    /// The main camera controller, follows player
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The Player GameObject (set in inspector)
        /// </summary>
        public GameObject Player;
        
        private CameraMovementBehaviourController _movement;

        private void Awake()
        {
            //Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        }

        private void Start()
        {
            
            _movement = new CameraMovementBehaviourController(transform.position - Player.transform.position, 5.0f);
            transform.position = Player.transform.position + _movement.Offset;
        }

        private void OnEnable()
        {
            // OnEnable gets called in the beginning and between levels
            // We force instant movement here in order to avoid an awkward looking transition
            // transform.position = Player.transform.position + _movement.Offset;

        }

        private void LateUpdate()
        {
            // Moving in LateUpdate so the object we're following has already moved
            transform.position =
                _movement.CalculatePosition(transform.position, Player.transform.position, Time.deltaTime);
        }
    }
}