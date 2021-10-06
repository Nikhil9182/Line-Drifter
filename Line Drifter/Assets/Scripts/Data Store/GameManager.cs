using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject defaultCarPrefab;
    public GameObject defaultPencilPrefab;
    public GameObject defaultLinePrefab;
    public GameObject selectedPencilPrefab;
    public GameObject selectedCarPrefab;
    public GameObject selectedLinePrefab;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
