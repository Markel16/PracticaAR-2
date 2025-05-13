using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // Importante para usar TextMeshProUGUI

public class MenuManager : MonoBehaviour
{
    public Slider tiempoSlider;
    public Slider verticalSlider;
    public Slider horizontalSlider;
    public Toggle oclusionToggle;

    public TextMeshProUGUI tiempoTexto;
    public TextMeshProUGUI verticalTexto;
    public TextMeshProUGUI horizontalTexto;

    void Start()
    {
        // Iniciar los textos con los valores actuales
        ActualizarTextoTiempo();
        ActualizarTextoVertical();
        ActualizarTextoHorizontal();

        // Añadir listeners para actualizar los textos cuando cambie el valor
        tiempoSlider.onValueChanged.AddListener(delegate { ActualizarTextoTiempo(); });
        verticalSlider.onValueChanged.AddListener(delegate { ActualizarTextoVertical(); });
        horizontalSlider.onValueChanged.AddListener(delegate { ActualizarTextoHorizontal(); });
    }

    void ActualizarTextoTiempo()
    {
        tiempoTexto.text = Mathf.RoundToInt(tiempoSlider.value).ToString();
    }

    void ActualizarTextoVertical()
    {
        verticalTexto.text = Mathf.RoundToInt(verticalSlider.value).ToString();
    }

    void ActualizarTextoHorizontal()
    {
        horizontalTexto.text = Mathf.RoundToInt(horizontalSlider.value).ToString();
    }

    public void ComenzarJuego()
    {
        PlayerPrefs.SetInt("Tiempo", Mathf.RoundToInt(tiempoSlider.value));
        PlayerPrefs.SetInt("Vertical", Mathf.RoundToInt(verticalSlider.value));
        PlayerPrefs.SetInt("Horizontal", Mathf.RoundToInt(horizontalSlider.value));
        PlayerPrefs.SetInt("Oclusion", oclusionToggle.isOn ? 1 : 0);

        SceneManager.LoadScene("JuegoAR"); // Asegúrate de tener esta escena en el Build Settings
    }
}

