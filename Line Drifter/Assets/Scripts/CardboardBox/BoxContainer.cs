using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxContainer : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private UIGameOver gameOver;
    [SerializeField] private UIHandler points;
    private AudioSource winningClip;

    private void Start()
    {
        winningClip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Car"))
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        winningClip.Play();
        points.StopLine();
        yield return new WaitForSeconds(1f);
        gameOver.NextScene();
    }
}
