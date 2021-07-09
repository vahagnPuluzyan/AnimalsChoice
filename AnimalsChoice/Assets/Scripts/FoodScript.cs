using System.Collections;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] bool _edible;

    ParticleSystem foodFx;
    Collider playerCollider;
    MeshRenderer mesh;
    
    private void Start()
    {
        foodFx = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_edible)
            {
                playerCollider = other.gameObject.GetComponentInChildren<Collider>();
                foodFx.Play();
                mesh.enabled = false;
                Vibration.Vibrate(20);
                StartCoroutine(ColliderOff());         
            }
            else
            {
                AnimalTransformation._instance.playerAnimator.SetBool("Death", true);
                GameManager._instance.gameIsOver = true;
                Vibration.Vibrate(40);
                StartCoroutine(Dead());
                foodFx.Play();
                mesh.enabled = false;
            }
        }
    }

    IEnumerator ColliderOff()
    {
        playerCollider.enabled = false;
        yield return new WaitForSecondsRealtime(2f);
        playerCollider.enabled = true;
    }


    public IEnumerator Dead()
    {
        yield return new WaitForSecondsRealtime(2.3f);
        GameManager._instance.CharacterDead();
    }
}