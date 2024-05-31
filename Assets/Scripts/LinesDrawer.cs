using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;

public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;
    public float linePointsMinDistance;
    public float lineWidth;
    public Rigidbody2D Ball1 { get; private set; }
    public Rigidbody2D Ball2 { get; private set; }
    public float baseMassPerPoint = 0.2f; // Base mass per point
    public int maxAttempts = 10; // Maximum attempts allowed
    private int remainingAttempts; // Remaining attempts
    private int currentAttempt = 0; // Current attempt count
    public TextMeshProUGUI attemptsText; // Reference to TextMeshPro UI component
    Line currentLine;
    Camera cam;

    // List to store all lines
    private List<Line> lines = new List<Line>();

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("ballLayer");
        remainingAttempts = maxAttempts;
        //UpdateAttemptsText();
        StartCoroutine(UpdateAttemptsText1());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && remainingAttempts > 0)
        {
            BeginDraw();
        }

        if (currentLine != null )
        {
            Draw();
        }

        if (Input.GetMouseButtonUp(0) )
        {
            EndDraw();
        }
    }

    // Method to set ball references at runtime
    public void SetBalls(Rigidbody2D ball1Rigidbody, Rigidbody2D ball2Rigidbody)
    {
        Ball1 = ball1Rigidbody;
        Ball2 = ball2Rigidbody;

        Ball1.isKinematic = true;
        Ball2.isKinematic = true;
        Debug.Log("in setballs ");
    }

    // Begin Draw
    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        // Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
        currentAttempt++;
        remainingAttempts = maxAttempts - currentAttempt;
        //UpdateAttemptsText();
        StartCoroutine(UpdateAttemptsText1());
    }

    // Draw 
    void Draw()
    {
         Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw()
        if (Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 0f, cantDrawOverLayer))
        {
            EndDraw();
            Debug.Log("Layerhit" + cantDrawOverLayer);
        }
        else
        {
            currentLine.AddPoint(mousePosition);
        }
    }

    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                // Add the line to "CantDrawOver" layer
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                // Activate Physics on the line
                currentLine.UsePhysics(true);
                // Set the mass of the line based on the number of points
                currentLine.SetMass(baseMassPerPoint * currentLine.pointsCount * 2f);
                // Add the line to the list
                lines.Add(currentLine);
                currentLine = null;
            }
            // Make the balls dynamic again
            Ball1.isKinematic = false;
            Ball2.isKinematic = false;
        }
    }

    // Reset level by resetting attempts count and updating UI
    public void ResetLine()
    {
        remainingAttempts = maxAttempts;
        currentAttempt = 0;
        // UpdateAttemptsText();
        StartCoroutine(UpdateAttemptsText1());
        // Destroy all existing lines from the list
        foreach (Line line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

    private IEnumerator UpdateAttemptsText1()
    {
        yield return new WaitForSeconds(0.2f);

        if (attemptsText != null)
        {
            attemptsText.text = remainingAttempts.ToString();

            // Check if remaining attempts are zero
            if (remainingAttempts <= 0)
            {
                // Call a method to restart the game
                LevelManager.instance.ResetLevel1();
            }
        }

    }
}



//EndDraw phy
// Convert world space points to local space and set to EdgeCollider2D
//Vector2[] localPoints = new Vector2[currentLine.pointsCount];
//for (int i = 0; i < currentLine.pointsCount; i++)
//{
//    localPoints[i] = currentLine.transform.InverseTransformPoint(currentLine.points[i]);
//}
//currentLine.edgeCollider.points = localPoints;
