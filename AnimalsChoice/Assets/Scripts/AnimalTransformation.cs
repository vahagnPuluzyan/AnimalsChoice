using System.Collections.Generic;
using UnityEngine;

public class AnimalTransformation : MonoBehaviour
{
    public static AnimalTransformation _instance;

    [SerializeField] List<GameObject> animals;

    ParticleSystem transformationFx;

    public Animator playerAnimator;
    private CameraShake camera;

    private void Start()
    {
        _instance = this;
        transformationFx = GetComponentInChildren<ParticleSystem>();
        playerAnimator = GetComponentInChildren<Animator>();
        camera = FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Transformation")
        {
            camera.shakeDuration = 0.1f;
            Vibration.Vibrate(30);
            for (int i = 0; i < animals.Count; i++)
            {
                animals[i].SetActive(false);
            }
            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i].name == other.name)
                {
                    animals[i].SetActive(true);
                    transformationFx.Play();
                    other.GetComponent<Collider>().enabled = false;
                    playerAnimator = GetComponentInChildren<Animator>();
                    playerAnimator.SetBool("Run", true);
                }
            }
        }
    }
}