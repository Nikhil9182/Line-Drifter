using UnityEngine;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private CarSpawner carSpawner;
    [SerializeField] private UIHandler button;

    public void Simulate()
    {
        if (carSpawner.car != null)
        {
            carSpawner.car.toStart = !carSpawner.car.toStart;
            if (carSpawner.car.toStart == true)
            {
                carSpawner.car.CarDustParticles();
            }
        }
    }

    public void Replay()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Next()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("index") + 1);
        
    }

    public int GetCurrentIndex()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.buildIndex;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("index"));
    }

    public void Level()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Hint()
    {
        button.HintUI();
    }

    public void BuyHint()
    {
        button.BuyHint();
    }

    public void LevelSelect(int i)
    {
        SceneManager.LoadScene(i);
    }
}
