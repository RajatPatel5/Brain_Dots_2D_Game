using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;
    float pointsMinDistance = 0.1f;
    float circleColliderRadius;

    public void AddPoint(Vector2 newPoint)
    {
        points.Add(newPoint);
        pointsCount++;
        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
        if (pointsCount > 1)
        {
            edgeCollider.points = points.ToArray();
        }
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;
        edgeCollider.edgeRadius = circleColliderRadius;
    }

    public void SetMass(float mass)
    {
        rigidBody.mass = mass;
    }
}
