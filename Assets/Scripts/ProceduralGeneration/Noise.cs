using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Noise : MonoBehaviour
{
    // 0 = air
    // 1 = dirt
    // 2 = stone
    // 3 = grass
    // 4 = snow
    // 5 = sand
    public const int airID = 0;
    public const int dirtID = 1;
    public const int stoneID = 2;
    public const int grassID = 3;
    public const int snowID = 4;
    public const int sandID = 5;

    public static int[,] SurfaceMapGeneration(Vector2 offset, int chunkSize, int chunkHeight, int seed, float frequency, int octaves)
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
                    float nx = (float)(x + offset.x) / (float)chunkHeight - 0.5f, ny = (float)(y + offset.y) / (float)chunkHeight - 0.5f;
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

    public static int[,,] MapGeneration3D(int mapSize, int mapHeight, int undergroundSize, int[,] heightMap, int nrOfBlocksBeforeStone)
    {
        int[,,] map = new int[mapSize, mapHeight , mapSize];

        /*for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int z = 0; z < mapSize; z++)
                {
                    map[x, y, z] = 0;
                }
            }
        }*/

        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                for (int y = mapHeight - 1; y >= 0; y--)
                {
                    //TODO CheckBiome
                    // 0 = air
                    // 1 = dirt
                    // 2 = stone
                    // 3 = grass
                    // 4 = snow
                    // 5 = sand
                    int biomeSurfaceBlockID = 5;
                     int blockBeforeStoneID = 5;
                     if (heightMap[x, z] + undergroundSize > y)
                     {
                         map[x, y, z] = 0;
                     }
                     else if (heightMap[x, z] + undergroundSize == y)
                     {
                         map[x, y, z] = biomeSurfaceBlockID;
                     }
                     else if (heightMap[x, z] + undergroundSize > y - nrOfBlocksBeforeStone)
                     {
                         map[x, y, z] = blockBeforeStoneID;
                     }
                     else
                     {
                         map[x, y, z] = 2;
                     }


                }
            }
        }
        return map;
    }

    public void LerpBlocks(bool lerpYpos, bool lerpYneg, bool lerpXpos, bool lerpXneg)
    {

    }
}
