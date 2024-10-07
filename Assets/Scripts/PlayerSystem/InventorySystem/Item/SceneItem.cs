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
            //collectParticle = GetComponentInChildren<ParticleSystem>();
            //collectParticle.Stop();
        }

        private void Start()
        {
            emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.cheeseIdle, gameObject);
            emitter.Play();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isPickedUp)
                {
                    _inventoryController.AddItem(_itemType, _amount);
                    //collectParticle.Play();
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

            if (this.CompareTag("Cheese"))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.cheeseCollected, transform.position);
            }
            else
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.cheeseCollected, transform.position);
            }
        }
    }
}