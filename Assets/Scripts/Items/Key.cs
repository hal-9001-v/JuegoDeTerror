using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public override void useItem()
    {
        
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;
        pos.y += 40;
        Gizmos.DrawSphere(pos, 15);
    }
}
