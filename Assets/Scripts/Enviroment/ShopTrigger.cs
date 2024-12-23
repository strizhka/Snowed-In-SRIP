using InputLogic.Readers;
using ShopLogic;
using UnityEngine;
using Zenject;

namespace Enviroment
{
    public class ShopTrigger : MonoBehaviour
    {
        [SerializeField] private Canvas _hintCanvas;
        [SerializeField] private ShopManager _shopManager;

        private GameplayInputReader _gameplayInputReader;

        [Inject]
        public void Construct(GameplayInputReader gameplayInputReader)
        {
            _gameplayInputReader = gameplayInputReader;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _hintCanvas.gameObject.SetActive(true);
                _gameplayInputReader.OnInteractionTriggered += OpenShop;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _hintCanvas.gameObject.SetActive(false);
                _gameplayInputReader.OnInteractionTriggered -= OpenShop;
            }
        }

        private void OpenShop()
        {
            _shopManager.gameObject.SetActive(true);
        }
    }
}