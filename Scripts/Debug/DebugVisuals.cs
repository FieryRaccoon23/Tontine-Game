using UnityEngine;
using System.Collections;
using System;

public class DebugVisuals : MonoBehaviour
{
    [SerializeField]
    private Color debugSphereColor = Color.red;

    [SerializeField]
    private float radius = 1.0f;

    void OnDrawGizmosSelected()
    {
        DrawSphere();
    }

    private void DrawSphere()
    {
        Gizmos.color = debugSphereColor;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
