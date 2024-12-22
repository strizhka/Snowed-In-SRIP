using FMODUnity;
using UnityEngine;

namespace AudioSystem
{
    public class FMODEvents : MonoBehaviour
    {
        [field: Header("Ambience")]
        [field: SerializeField] public EventReference Ambience { get; private set; }

        [field: Header("Music")]
        [field: SerializeField] public EventReference Music { get; private set; }

        [field: Header("Player SFX")]
        [field: SerializeField] public EventReference PlayerSnowFootsteps { get; private set; }

        [field: Header("Cheese SFX")]
        [field: SerializeField] public EventReference CheeseCollected { get; private set; }
        [field: SerializeField] public EventReference CheeseIdle { get; private set; }

        [field: Header("Items SFX")]
        [field: SerializeField] public EventReference ItemCollected { get; private set; }
        [field: SerializeField] public EventReference ItemIdle { get; private set; }

        public static FMODEvents Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one FMOD Events instance in the scene.");
            }

            Instance = this;
        }
    }
}