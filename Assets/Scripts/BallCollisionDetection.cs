using System.Collections;
using UnityEngine;

public class BallCollisionDetection : MonoBehaviour
{
    public delegate void GameOver();
    public static GameOver gameOver;

    //private void Update()
    //{
    //    if (transform.position.y < -5.7f)
    //    {
    //        gameOver?.Invoke();
    //    }
    //}
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Ball2Script ball = collision.transform.GetComponent<Ball2Script>();
        if (ball != null)
        {
            gameOver?.Invoke();
        }
    }
}
