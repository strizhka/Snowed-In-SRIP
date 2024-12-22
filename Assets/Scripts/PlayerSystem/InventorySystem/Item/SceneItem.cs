using AudioSystem;
using FMODUnity;
using UnityEngine;
using Zenject;

namespace PlayerSystem.InventorySystem.Item
{
    public class SceneItem : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private int _amount;
        //[SerializeField] private EventReference _objectCollectedSound;
        private InventoryController _inventoryController;
        private bool _isPickedUp = false;
        private SpriteRenderer visual;
        private ParticleSystem collectParticle;


        private StudioEventEmitter emitter;

        [Inject]
        public void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        private void Awake()
        {
            visual = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            emitter = AudioManager.Instance.InitializeEventEmitter(FMODEvents.Instance.CheeseIdle, gameObject);
            emitter.Play();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isPickedUp)
                {
                    _inventoryController.AddItem(_itemType, _amount);
                    CollectItem();
                    _isPickedUp = true;
                }
                Destroy(gameObject);

            }
        }

        private void CollectItem()
        {
            visual.gameObject.SetActive(false);

            emitter.Stop();

            if (_itemType == ItemType.Cheese)
            {
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.CheeseCollected, transform.position);
            }
            else
            {
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ItemCollected, transform.position);
            }
        }
    }
}