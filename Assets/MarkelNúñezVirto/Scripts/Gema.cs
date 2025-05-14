using UnityEngine;

public class Gema : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManagerAR.Instance.RecogerGema(this.gameObject);
    }
}

