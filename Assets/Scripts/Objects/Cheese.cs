using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class Cheese : MonoBehaviour
{
    //[SerializeField] private EventReference _objectCollectedSound;

    private SpriteRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private StudioEventEmitter emitter;

    private void Awake()
    {
        visual = GetComponentInChildren<SpriteRenderer>();
        //collectParticle = GetComponentInChildren<ParticleSystem>();
        //collectParticle.Stop();
    }

    private void Start()
    {
        emitter = AudioManager.Instance.InitializeEventEmitter(FMODEvents.Instance.CheeseIdle, gameObject);
        emitter.Play();
    }

    private void OnTriggerEnter2D()
    {
        if (!collected)
        {
            //collectParticle.Play();
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        collected = true;
        visual.gameObject.SetActive(false);

        emitter.Stop();
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.CheeseCollected, transform.position);

    }

}
