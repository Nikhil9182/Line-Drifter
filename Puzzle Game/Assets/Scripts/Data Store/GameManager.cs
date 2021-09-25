using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject defaultCarPrefab;
    [HideInInspector] public GameObject defaultPencilPrefab;
    public GameObject selectedPencilPrefab;
    public GameObject selectedCarPrefab;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
