using UnityEngine;
using System.Collections;
using System.Threading;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    private LineRenderer line;
    private int lineLength = 2;

    public Material mat;
    public Transform startPos;
    public Transform endPos;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        // set length
        line.positionCount = lineLength;
        line.material = mat;

        // set color and width
        line.startColor = Color.yellow;
        line.endColor = Color.red;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
    }
    void Update()
    {
        // set startPosition and endPosition
        line.SetPosition(0, startPos.position);
        line.SetPosition(1, endPos.position);
    }
}