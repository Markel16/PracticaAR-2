using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManagerAR : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public Text infoText; // UI para mostrar planos detectados

    private int planosHorizontalesObjetivo;
    private int planosVerticalesObjetivo;

    private int planosHorizontalesDetectados = 0;
    private int planosVerticalesDetectados = 0;

    private bool juegoListo = false;

    void Start()
    {
        // Leer datos desde PlayerPrefs
        planosHorizontalesObjetivo = PlayerPrefs.GetInt("Horizontal", 0);
        planosVerticalesObjetivo = PlayerPrefs.GetInt("Vertical", 0);

        infoText.text = $"Buscando planos...\nHorizontales: 0 / {planosHorizontalesObjetivo}\nVerticales: 0 / {planosVerticalesObjetivo}";
    }

    void Update()
    {
        if (juegoListo) return;

        planosHorizontalesDetectados = 0;
        planosVerticalesDetectados = 0;

        foreach (ARPlane plano in planeManager.trackables)
        {
            if (plano.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
                planosHorizontalesDetectados++;
            else if (plano.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical)
                planosVerticalesDetectados++;
        }

        infoText.text = $"Buscando planos...\nHorizontales: {planosHorizontalesDetectados} / {planosHorizontalesObjetivo}\nVerticales: {planosVerticalesDetectados} / {planosVerticalesObjetivo}";

        if (planosHorizontalesDetectados >= planosHorizontalesObjetivo &&
            planosVerticalesDetectados >= planosVerticalesObjetivo)
        {
            juegoListo = true;
            infoText.text = "¡Planos detectados! Puedes comenzar a colocar las gemas.";
            // Aquí puedes activar el botón de “Colocar gemas” o pasar directamente a instanciar
        }
    }
}

