using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineDraw;
    public bool state = false;
    public bool clear = false;
    public bool ColorRed = true;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        lineDraw.startWidth = 0.2f;
        lineDraw.endWidth = 0.2f;
        lineDraw.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
                Debug.Log(lineDraw.positionCount);
        if (start != true)
        {
            if (state != true)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 currentPoint = GetWorldCoordinate(Input.mousePosition);
                    lineDraw.positionCount++;
                    lineDraw.SetPosition(lineDraw.positionCount - 1, currentPoint);
                }
                else if (clear == true)
                {
                    Debug.Log("ClearLine");
                    lineDraw.positionCount = 0;
                    Debug.Log(lineDraw.positionCount);
                    clear = false;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    lineDraw.positionCount = 0;
                    Debug.Log(lineDraw.positionCount);
                }
            }
        }
    }

    private Vector3 GetWorldCoordinate(Vector3 mousePosition)
    {
        Vector3 mousePoint = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
