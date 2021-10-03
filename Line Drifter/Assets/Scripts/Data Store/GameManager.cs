using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject defaultCarPrefab;
    public GameObject defaultPencilPrefab;
    public GameObject defaultLinePrefab;
    public GameObject selectedPencilPrefab;
    public GameObject selectedCarPrefab;
    public GameObject selectedLinePrefab;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
