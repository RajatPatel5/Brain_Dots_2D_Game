using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallCollide : MonoBehaviour
{
    public LinesDrawer linesdrawer;
    public GameOverScreen gameOverScreen;
    public Canvas GameOverCanvas;
    //public Image gameOverImage; // Reference to the Image component in the Game Over canvas

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball2"))
        {
            Debug.Log("Game Over");
            StartCoroutine(SnapScreen());
            linesdrawer.enabled = false;
            //GameOverCanvas.enabled = true;

            //gameOverScreen.enabled = true;
            SceneControllerScript.instance.NextLevel();

        }
    }

    private IEnumerator SnapScreen()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(Application.persistentDataPath);

        string folderPath = "Assets/Screenshots/";

        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        var screenshotName = "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
                                ".png";

        string screenshotPath = System.IO.Path.Combine(folderPath, screenshotName);
        ScreenCapture.CaptureScreenshot(screenshotPath, 2);

        Debug.Log("Screenshot saved at: " + screenshotPath);

        // Load the captured screenshot as a Texture2D
        byte[] bytes = System.IO.File.ReadAllBytes(screenshotPath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);

        // Assign the loaded texture to the Image component's sprite
      //  gameOverImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
}
