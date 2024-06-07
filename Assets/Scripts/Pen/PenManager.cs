using UnityEngine;
using System.Collections.Generic;

public class PenManager : MonoBehaviour
{
    public List<Pen> pens;
    public GameObject linePrefab;
    private Pen currentPen;

    void Start()
    {
        if (pens.Count > 0)
        {
            currentPen = pens[0];
            Debug.Log("Default pen selected: " + currentPen.name);
        }
    }

    public void SetPen(Pen pen)
    {
        currentPen = pen;
        Debug.Log("Pen switched to: " + currentPen.name);
    }

    public Pen GetCurrentPen()
    {
        return currentPen;
    }
}
