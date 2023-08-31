using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOutline : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        //placed in start since i will decouple it as soon as I create the upgrade system.
        SetCircleRadius(3f);
    }

    private void SetCircleRadius(float radius)
    {
        if (lineRenderer != null)
        {
            int numSegments = 360;

            Vector3[] positions = new Vector3[numSegments + 1];
            for (int i = 0; i <= numSegments; i++)
            {
                float angle = (i / (float)numSegments) * 360f;
                float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                positions[i] = new Vector3(x, 0, z);
            }

            // Set the positions for the LineRenderer
            lineRenderer.positionCount = numSegments + 1;
            lineRenderer.SetPositions(positions);
        }
    }
}
