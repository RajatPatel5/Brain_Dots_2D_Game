using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PenUIManager : MonoBehaviour
{
    public List<Button> penButtons;
    public PenManager penManager;

    void Start()
    {
        for (int i = 0; i < penButtons.Count; i++)
        {
            int index = i;
            penButtons[i].onClick.AddListener(() => OnPenButtonClick(index));
        }
    }

    void OnPenButtonClick(int index)
    {
        penManager.SetPen(penManager.pens[index]);
        Debug.Log("Selected Pen Is :_" + penManager.pens[index].name);
    }
}
