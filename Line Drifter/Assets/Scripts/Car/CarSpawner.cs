using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private UIGameOver gameOver;
    [HideInInspector] public CarController car;
    [Tooltip("Distance for falling of from the cliff"),SerializeField] public float distance;

    private GameObject spawnedCar;
    private bool hasFalled = false;

    void Start()
    {
        SpawnCar();
        hasFalled = false;
        car = spawnedCar.GetComponent<CarController>();
    }

    private void Update()
    {
        if((spawnedCar.transform.position.y <= -distance || spawnedCar.transform.position.y >= 2*distance) && !hasFalled)
        {
            hasFalled = true;
            gameOver.Save(0);
            gameOver.NextScene();
        }
    }

    public void SpawnCar()
    {
        if (GameManager.Instance.selectedCarPrefab == null)
        {
            GameManager.Instance.selectedCarPrefab = GameManager.Instance.defaultCarPrefab;
        }
        spawnedCar = Instantiate(GameManager.Instance.selectedCarPrefab, gameObject.transform);
    }
}
