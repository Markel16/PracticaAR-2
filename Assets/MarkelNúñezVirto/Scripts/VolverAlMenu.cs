using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenu : MonoBehaviour
{
    public void Volver()
    {
        SceneManager.LoadScene("Scene1"); // Cambia "Scene1" por el nombre real de tu escena de menú
    }
}

