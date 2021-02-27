using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public Color colorOfPivot;

    [Range(0.1f, 5)]
    public float sizeOfPivot;

    private void OnDrawGizmos()
    {
        Gizmos.color = colorOfPivot;
        Gizmos.DrawSphere(transform.position,sizeOfPivot);
    }
}
