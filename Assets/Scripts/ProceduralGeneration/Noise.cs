using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static int[,] MapGeneration(Vector2 offset, int chunkSize, int chunkHeight, int seed, float frequency, int octaves)
    {
        if (octaves <= 0)
        {
            octaves = 1;
        }

        /*System.Random pseudoRandomNumberGenerator = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = pseudoRandomNumberGenerator.Next(-100000, 100000) + offset.x;
            float offsetY = pseudoRandomNumberGenerator.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }*/
        //Map Generation
        int[,] map = new int[chunkSize,chunkSize];

        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                float result = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float nx = (float)(x + offset.x) / (float)chunkSize - 0.5f, ny = (float)(y + offset.y) / (float)chunkSize - 0.5f;
                    nx *= frequency;
                    ny *= frequency;
                    //nx += octaveOffsets[i].x;
                    //ny += octaveOffsets[i].y;
                    result = ((float)1/(float)(i + 1) * (Mathf.PerlinNoise(nx, ny)));
                }

                map[y, x] = (int)(result * chunkHeight);
            }
        }

        return map;
    }
}
