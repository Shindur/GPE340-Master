using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBox : MonoBehaviour
{
    //vector for the box to be drawn
    public Vector3 scale;

    /// <summary>
    /// Draws the spawn boxes
    /// </summary>
    private void OnDrawGizmos()
    {
        //sets the matrix to draw new gizmos
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        //sets the color to stay between cyan and clear
        Gizmos.color = Color.Lerp(Color.cyan, Color.clear, 0.5f);
        //draws a cube, using the public scale variable
        Gizmos.DrawCube(Vector3.up * scale.y / 2f, scale);
        //gizmos color set to cyan
        Gizmos.color = Color.cyan;
        //draws a ray forward to show the direction
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * 0.4f);
    }
}
