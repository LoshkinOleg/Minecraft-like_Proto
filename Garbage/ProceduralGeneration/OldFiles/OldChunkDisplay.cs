using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldChunkDisplay : MonoBehaviour
{
    private List<GameObject> cubes = new List<GameObject>();
    public GameObject cube;
    public void DrawChunk(float[,] map, float heightRange)
    {
        foreach (GameObject element in cubes)
        {
            DestroyImmediate(element);
        }
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                cubes.Add(Instantiate(cube, new Vector3(x, (int)(map[x, y] * heightRange), y), Quaternion.identity, this.transform));
                Debug.Log(map[x,y]);
            }
        }
    }
}
