using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private CarSpawner carSpawner;
    [SerializeField] private UIHandler handler;
    [SerializeField] private Animator crossfade;
    
    AudioSource click;

    private void Start()
    {
        click = GetComponent<AudioSource>();
        click.volume = 0.6f;
        click.pitch = 1.1f;
    }

    public void Simulate()
    {
        click.Play();
        if (carSpawner.car != null)
        {
            carSpawner.car.toStart = !carSpawner.car.toStart;
            if (carSpawner.car.toStart == true)
            {
                handler.playButtonImage.sprite = handler.Unplay;
                carSpawner.car.CarDustParticles();
            }
            if(carSpawner.car.toStart == false)
            {
                handler.playButtonImage.sprite = handler.Play;
                carSpawner.car.ApplyBrake();
            }
        }
    }

    public void Replay()
    {
        click.Play();
        Scene scene = SceneManager.GetActiveScene();
        StartCoroutine(LevelCrossFade(0, scene.name));
    }

    public void Next()
    {
        click.Play();
        StartCoroutine(LevelCrossFade(PlayerPrefs.GetInt("index") + 1, null));
    }

    public int GetCurrentIndex()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.buildIndex;
    }

    public void Home()
    {
        click.Play();
        StartCoroutine(LevelCrossFade(0, "MainMenu"));
    }

    public void Shop()
    {
        click.Play();
        StartCoroutine(LevelCrossFade(0, "Shop"));
    }

    public void Retry()
    {
        click.Play();
        StartCoroutine(LevelCrossFade(PlayerPrefs.GetInt("index"), null));
    }

    public void Level()
    {
        click.Play();
        StartCoroutine(LevelCrossFade(0, "Levels"));
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("GameEnd");
    }

    public void Hint()
    {
        click.Play();
        handler.HintUI();
    }

    public void BuyHint()
    {
        click.Play();
        handler.BuyHint();
    }

    public void LevelSelect(int i)
    {
        click.Play();
        StartCoroutine(LevelCrossFade(i, null));
    }

    public void Settings()
    {
        click.Play();
        StartCoroutine(LevelCrossFade(0, "Settings"));
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
