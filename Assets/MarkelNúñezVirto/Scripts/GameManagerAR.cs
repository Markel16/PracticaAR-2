using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

public class GameManagerAR : MonoBehaviour
{
    public static GameManagerAR Instance;

    [Header("AR")]
    public ARPlaneManager planeManager;

    [Header("UI")]
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI gemasText;
    public Button botonCrear;
    public GameObject panelDeteccion;
    public GameObject panelJuego;
    public GameObject panelVictoria;

    [Header("Prefab")]
    public GameObject gemaPrefab;

    private int planosHorizontalesObjetivo;
    private int planosVerticalesObjetivo;

    private int planosHorizontalesDetectados = 0;
    private int planosVerticalesDetectados = 0;

    private int gemasTotales = 0;
    private int gemasRecogidas = 0;

    private bool gemasInstanciadas = false;

    void Start()
    {
        Instance = this;

        planosHorizontalesObjetivo = PlayerPrefs.GetInt("Horizontal", 0);
        planosVerticalesObjetivo = PlayerPrefs.GetInt("Vertical", 0);

        botonCrear.interactable = false;
        botonCrear.onClick.AddListener(InstanciarGemas);

        panelJuego.SetActive(false);
        panelVictoria.SetActive(false);
        ActualizarTextoGemas();
    }

    void Update()
    {
        planosHorizontalesDetectados = 0;
        planosVerticalesDetectados = 0;

        foreach (ARPlane plano in planeManager.trackables)
        {
            if (plano.alignment == PlaneAlignment.HorizontalUp)
                planosHorizontalesDetectados++;
            else if (plano.alignment == PlaneAlignment.Vertical)
                planosVerticalesDetectados++;
        }

        infoText.text = $"ENCUENTRA PLANOS\n" +
                        $"VERTICALES: {planosVerticalesDetectados} / {planosVerticalesObjetivo}\n" +
                        $"HORIZONTALES: {planosHorizontalesDetectados} / {planosHorizontalesObjetivo}";

        bool requisitosCumplidos = planosHorizontalesDetectados >= planosHorizontalesObjetivo &&
                                   planosVerticalesDetectados >= planosVerticalesObjetivo;

        botonCrear.interactable = requisitosCumplidos && !gemasInstanciadas;
    }

    void InstanciarGemas()
    {
        if (gemasInstanciadas) return;

        gemasTotales = planosHorizontalesObjetivo + planosVerticalesObjetivo;
        int colocadas = 0;

        foreach (ARPlane plano in planeManager.trackables)
        {
            if (colocadas >= gemasTotales)
                break;

            Vector3 posicion = plano.center + Vector3.up * 0.05f;

            GameObject nuevaGema = Instantiate(gemaPrefab, posicion, Quaternion.identity);
            nuevaGema.AddComponent<Gema>(); // Asigna el script de recogida
            colocadas++;
        }

        infoText.text = "¡GEMAS COLOCADAS!";
        ActualizarTextoGemas();

        panelDeteccion.SetActive(false);
        panelJuego.SetActive(true);

        gemasInstanciadas = true;
        botonCrear.interactable = false;
    }

    public void RecogerGema(GameObject gema)
    {
        Destroy(gema);
        gemasRecogidas++;
        ActualizarTextoGemas();

        if (gemasRecogidas >= gemasTotales)
        {
            infoText.text = "¡Has encontrado todas las gemas!";
            panelVictoria.SetActive(true); // Mostrar mensaje final
        }
    }

    void ActualizarTextoGemas()
    {
        gemasText.text = $"Gemas encontradas: {gemasRecogidas} / {gemasTotales}";
    }
}
