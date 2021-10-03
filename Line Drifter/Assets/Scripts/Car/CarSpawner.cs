using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameManager manager;
    [SerializeField] private UIGameOver gameOver;
    [HideInInspector] public CarController car;
    [SerializeField] public float distance;

    private GameObject spawnedCar;
    private bool hasFalled;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SpawnCar();
        hasFalled = false;
        car = spawnedCar.GetComponent<CarController>();
    }

    private void Update()
    {
        if(spawnedCar.transform.position.y <= -distance && !hasFalled)
        {
            hasFalled = true;
            gameOver.Save(0);
            gameOver.NextScene();
        }
    }

    public void SpawnCar()
    {
        if (manager.selectedCarPrefab == null)
        {
            manager.selectedCarPrefab = manager.defaultCarPrefab;
        }
        spawnedCar = Instantiate(manager.selectedCarPrefab, gameObject.transform);
    }
}
