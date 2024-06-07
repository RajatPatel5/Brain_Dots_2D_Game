using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    private int cantDrawOverLayerIndex;
    public float linePointsMinDistance;
    public float lineWidth;
    public Rigidbody2D Ball1 { get; private set; }
    public Rigidbody2D Ball2 { get; private set; }
    public float baseMassPerPoint = 0.2f;
    private Line currentLine;
    private Camera cam;
    private Vector2 lastMousePosition;
    private int lastUpdatePoint = 0;
    private List<Line> lines = new List<Line>();

    public delegate void UpdateInkSliderDelegate(int pointsToAdd);
    public static event UpdateInkSliderDelegate updateInkSlider;

    public delegate void ResetInkSlider();
    public static event ResetInkSlider resetInkSlider;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("ballLayer");

        updateInkSlider?.Invoke(lastUpdatePoint);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LineInstatiation();
        }
        if (currentLine != null)
        {
            Draw();
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    EndDraw();
        //}
    }

    public void SetBalls(Rigidbody2D ball1Rigidbody, Rigidbody2D ball2Rigidbody)
    {
        Ball1 = ball1Rigidbody;
        Ball2 = ball2Rigidbody;

        Ball1.isKinematic = true;
        Ball2.isKinematic = true;
        Debug.Log("Balls have been set.");
    }

    private void LineInstatiation()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentLine.rigidBody.isKinematic = true;
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
        lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        updateInkSlider?.Invoke(lastUpdatePoint);
    }

    private void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 0f, cantDrawOverLayer))
        {
            EndDraw();
        }
        else if (currentLine != null && Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }
        else 
        {
            if (mousePosition != lastMousePosition)
            {
                currentLine.AddPoint(mousePosition);
                lastUpdatePoint++;
                lastMousePosition = mousePosition;
                if (lastUpdatePoint > 0)
                {
                    updateInkSlider?.Invoke(lastUpdatePoint);
                    lastUpdatePoint = -1;
                }
            }
        }
    }

    private void EndDraw()
    {
        if (currentLine != null)
        {
            currentLine.gameObject.layer = cantDrawOverLayerIndex;
            currentLine.rigidBody.isKinematic = false;
            currentLine.SetMass(baseMassPerPoint * currentLine.pointsCount);
            lines.Add(currentLine);
            currentLine = null;
            Ball1.isKinematic = false;
            Ball2.isKinematic = false;
        }
    }

    public void ResetLine()
    {
        resetInkSlider?.Invoke();
        updateInkSlider?.Invoke(lastUpdatePoint);
        foreach (Line line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

    public void BallsKinematic()
    {
        Ball1.isKinematic = true;
        Ball2.isKinematic = true;
    }
}
