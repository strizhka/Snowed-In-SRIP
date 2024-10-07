using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerSnowFootsteps { get; private set; }

    [field: Header("Cheese SFX")]
    [field: SerializeField] public EventReference cheeseCollected { get; private set; }
    [field: SerializeField] public EventReference cheeseIdle { get; private set; }

    [field: Header("Items SFX")]
    [field: SerializeField] public EventReference itemCollected { get; private set; }
    [field: SerializeField] public EventReference itemIdle { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}