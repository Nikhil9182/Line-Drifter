using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private CarSpawner carSpawner;
    [SerializeField] private UIHandler button;
    [SerializeField] private Animator crossfade;

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
        StartCoroutine(LevelCrossFade(0, scene.name));
    }

    public void Next()
    {
        StartCoroutine(LevelCrossFade(PlayerPrefs.GetInt("index") + 1, null));
    }

    public int GetCurrentIndex()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.buildIndex;
    }

    public void Home()
    {
        StartCoroutine(LevelCrossFade(0, "MainMenu"));
    }

    public void Shop()
    {
        StartCoroutine(LevelCrossFade(0, "Shop"));
    }

    public void Retry()
    {
        StartCoroutine(LevelCrossFade(PlayerPrefs.GetInt("index"), null));
    }

    public void Level()
    {
        StartCoroutine(LevelCrossFade(0, "Levels"));
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("GameEnd");
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
        StartCoroutine(LevelCrossFade(i, null));
    }

    IEnumerator LevelCrossFade(int index , string name)
    {
        //play animation
        crossfade.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(0.5f);
        //transition to next scene
        if(name == null)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            SceneManager.LoadScene(name);
        }
    }
}
