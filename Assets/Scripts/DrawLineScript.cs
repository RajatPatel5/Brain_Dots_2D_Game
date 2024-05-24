using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    public GameObject prefab;
    private GameObject currentLine;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider2D;
    private List<Vector2> fingerPositions;

    [SerializeField] private Rigidbody2D circle1;
    [SerializeField] private Rigidbody2D circle2;

    private void Start()
    {
        fingerPositions = new List<Vector2>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (fingerPositions.Count == 0 || Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }

        if (Input.GetMouseButtonUp(0) && currentLine != null)
        {
            EnablePhysicsOnLine();
            currentLine = null; // Reset currentLine to allow new line drawing
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(prefab, Vector2.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider2D = currentLine.GetComponent<EdgeCollider2D>();

        fingerPositions.Clear();
        Vector2 startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(startPos);
        fingerPositions.Add(startPos);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider2D.points = fingerPositions.ToArray();

        // Add Rigidbody2D immediately after creating the line
        Rigidbody2D rb = currentLine.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = currentLine.AddComponent<Rigidbody2D>();
        }

        rb.isKinematic = true; // Set it to kinematic until line drawing is finished
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        // Update the edge collider points with the new position
        edgeCollider2D.points = fingerPositions.ToArray();
    }

    void EnablePhysicsOnLine()
    {
        // Enable physics on the line
        Rigidbody2D rb = currentLine.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.gravityScale = 2;  // Enable gravity

        // Enable physics on the circles
        //circle1.isKinematic = false;
        //circle2.isKinematic = false;

        Debug.Log("Physics enabled on circles: " + circle1 + ", " + circle2);
    }
}
