using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDisplay : MonoBehaviour
{
    [SerializeField] private Vector2 offset;

    [SerializeField] private int chunkSize = 16;
    [SerializeField] private GameObject cube;
    [SerializeField] private int seed;
    [SerializeField] private float frequency = 1.0f;
    [SerializeField] private int octaves;

    [SerializeField] private int chunkHeight = 16;

    [SerializeField] private Vector2Int chunks;

    void Start()
    {
        for (int x = 0; x < chunks.x; x++)
        {
            for (int y = 0; y < chunks.y; y++)
            {
                InstantiateChunk(new Vector2Int(x,y));
            }
        }
    }

    void InstantiateChunk(Vector2Int position)
    {
        int[,] map = Noise.MapGeneration(offset + new Vector2(chunkSize * position.x, chunkSize * position.y), chunkSize, chunkHeight, seed, frequency, octaves);
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Instantiate(cube, new Vector3(x + (chunkSize* position.y), map[x, y], y + (chunkSize * position.x)), Quaternion.identity);
            }
        }
    }
}


public struct Chunk
{
    public int[,,] blocks;

    Chunk(int chunkSize)
    {
        blocks = new int[chunkSize,chunkSize,chunkSize];
    }
}
