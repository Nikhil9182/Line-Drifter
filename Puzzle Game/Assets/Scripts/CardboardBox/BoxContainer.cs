using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxContainer : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private UIGameOver gameOver;
    [SerializeField] private UIHandler points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Car"))
        {
            points.StopLine();
            gameOver.NextScene();
        }
    }
}
