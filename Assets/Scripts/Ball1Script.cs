using System.Collections;
using UnityEngine;

public class BallCollide : MonoBehaviour
{
    private void Start()
    {
        //LevelManager.instance.ball1Transform = gameObject.transform;
        //LevelManager.instance.ball1Transform.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void Update()
    {
        if (transform.position.y < -5.7f)
        {
            StartCoroutine(GameOver());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball2"))
        {
            StartCoroutine(GameOver());
            Debug.Log("GameOver");
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Game Over");
        // StartCoroutine(SnapScreen());
        LevelManager.instance.GameOverScreenFunction();
    }

    //private IEnumerator SnapScreen()
    //{
    //    yield return new WaitForEndOfFrame(); // Wait for the end of the frame to capture the screen correctly

    //    string folderPath = Application.dataPath + "/Screenshots/";

    //    if (!System.IO.Directory.Exists(folderPath))
    //        System.IO.Directory.CreateDirectory(folderPath);

    //    string screenshotName = "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
    //    string screenshotPath = System.IO.Path.Combine(folderPath, screenshotName);

    //    ScreenCapture.CaptureScreenshot(screenshotPath);

    //    Debug.Log("Screenshot saved at: " + screenshotPath);

    //    // Wait a bit to ensure the screenshot file is written
    //    yield return new WaitForSeconds(0.5f);

    //    // Load the captured screenshot into the gameOverImage component
    //    //if (gameOverScreen != null && gameOverScreen.screenShotImage != null)
    //    //{
    //    //    byte[] imageData = System.IO.File.ReadAllBytes(screenshotPath);
    //    //    Texture2D texture = new Texture2D(2, 2);
    //    //    texture.LoadImage(imageData);
    //    //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //    //    gameOverScreen.screenShotImage.sprite = sprite;
    //    //}
    //}

}
