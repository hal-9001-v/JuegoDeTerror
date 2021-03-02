using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{

    const int gridMaxSize = 100;
    const int cellMaxSize = 100;


    [Range(1, gridMaxSize)]
    public int xSize;

    [Space(2)]
    [Range(1, gridMaxSize)]
    public int ySize;

    [Space(3)]
    [Range(0.1f, cellMaxSize)]
    public float cellSize;

    public Vector2[,] points;

    [Space(4)]
    [Range(0, 100)]
    public float cubeSize;

    public Color cubeColor;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDrawGizmos()
    {
        if ((!Application.isPlaying && transform.hasChanged) || points == null)
            points = getPoints();

        Vector3 gizmosSize = new Vector3(cubeSize, cubeSize, cubeSize);


        Gizmos.color = cubeColor;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {

                Gizmos.DrawCube(
                    new Vector3(points[i, j].x, transform.position.y, points[i, j].y),
                    gizmosSize);
            }


        }
    }

    public Vector2[,] getPoints()
    {

        Vector2[,] matrix = new Vector2[xSize, ySize];

        for (int i = 0; i < xSize; i++)
        {
            //matrix[i] = new Vector3[ySize];

            for (int j = 0; j < ySize; j++)
            {
                Vector3 pos = transform.position;

                matrix[i, j].x = Mathf.Round(pos.x / cellSize) * cellSize + cellSize * i;
                matrix[i, j].y = Mathf.Round(pos.z / cellSize) * cellSize + cellSize * j;
            }
        }


        return matrix;
    }

    public Vector3 getGridPosition(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x / cellSize) * cellSize, transform.position.y, Mathf.Round(pos.z / cellSize) * cellSize);
    }

    public Vector3 getGridPositionWithOffset(Vector3 pos, int xOffset, int yOffset)
    {
        Vector3 aux = getGridPosition(pos); ;

        aux.x += xOffset * cellSize;
        aux.z += yOffset * cellSize;


        return aux;
    }

}
