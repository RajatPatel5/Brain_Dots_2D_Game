using UnityEngine;

public class LinesDrawer : MonoBehaviour
{

    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;

    public float linePointsMinDistance;
    public float lineWidth;

    public Rigidbody2D Ball1;
    public Rigidbody2D Ball2;

    public float baseMassPerPoint = 0.2f; // Base mass per point
    Line currentLine;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("ballLayer");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    // Begin Draw ----------------------------------------------
    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        // Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    // Draw ----------------------------------------------------
    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        // Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw()
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit)
            EndDraw();
        else
            currentLine.AddPoint(mousePosition);
    }

    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                // If line has only one point, destroy it
                Destroy(currentLine.gameObject);
            }
            else
            {
                // Add the line to "CantDrawOver" layer
                currentLine.gameObject.layer = cantDrawOverLayerIndex;

                // Activate Physics on the line
                currentLine.UsePhysics(true);

                Debug.Log("currentpoints:" +currentLine.points);

                // Convert world space points to local space
                Vector2[] localPoints = new Vector2[currentLine.points.Count];
                for (int i = 0; i < currentLine.points.Count; i++)
                {
                    localPoints[i] = currentLine.transform.InverseTransformPoint(currentLine.points[i]);

                    Debug.Log("localpoints:"+localPoints);
                }

                // Set the local space points to the EdgeCollider2D
                currentLine.edgeCollider.points = localPoints;

                // Set the mass of the line based on the number of points
                currentLine.SetMass(baseMassPerPoint * currentLine.pointsCount * 2f);

                currentLine = null;
            }
            Ball1.isKinematic = false;
            Ball2.isKinematic = false;
        }
    }
}
