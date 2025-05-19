using UnityEngine;

public class MusicaPersistente : MonoBehaviour
{
    private static MusicaPersistente instanciaUnica;

    void Awake()
    {
        if (instanciaUnica == null)
        {
            instanciaUnica = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}

