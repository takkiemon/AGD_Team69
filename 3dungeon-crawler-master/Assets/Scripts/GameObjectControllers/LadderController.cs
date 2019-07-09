using UnityEngine;

namespace GameObjectControllers
{
    /// <summary>
    /// Controller of ladders
    /// In charge of informing the game master when leveling is required
    /// </summary>
    public class LadderController : MonoBehaviour
    {
        private GameObject _gameController;

        private void Start()
        {
            _gameController = GameObject.FindWithTag("GameController");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerController>().key == true)
                {
                    other.gameObject.GetComponent<PlayerController>().key = false;
                    _gameController.GetComponent<LudoGameMasterController>().LevelUp();
                }
            }
        }
    }
}