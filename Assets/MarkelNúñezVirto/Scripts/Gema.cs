using UnityEngine;

public class Gema : MonoBehaviour
{
    private AudioSource audioSource;
    private bool recogida = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (recogida) return;

        
        if (other.CompareTag("Player"))
        {
            recogida = true;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                Invoke("Recoger", audioSource.clip.length);
            }
            else
            {
                Recoger();
            }
        }
    }

    void Recoger()
    {
        GameManagerAR.Instance.RecogerGema(this.gameObject);
    }
}




