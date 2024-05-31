using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //LevelManager.instance.ball2Transform = gameObject.transform;
    }

    private void Update()
    {
        if (transform.position.y < -5.7f)
        {
            LevelManager.instance.GameOverScreenFunction();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball1"))
        {
            LevelManager.instance.GameOverScreenFunction();
        }
    }
}
