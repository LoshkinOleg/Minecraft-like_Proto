using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class ChunkDisplay : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    
    [SerializeField] private const int chunkSize = 128;
    [SerializeField] private const int mapSize = 64;
    [SerializeField] private GameObject cube;
    [SerializeField] private int seed;
    [SerializeField] private float frequency = 1.0f;
    [SerializeField] private float redistribution;
    [SerializeField] private int octaves;

    [SerializeField] private int chunkHeight = 16;
    [SerializeField] private int mapHeight = 32;

    [SerializeField] private int undergroundHeight = 16;
    [SerializeField] private int blocksNrBeforeStone = 4;

    [SerializeField] private Vector2Int chunks;

    [SerializeField] private int VisibleChunksDist = 3;

    [SerializeField] private GameObject water;
    [SerializeField] private int waterLevel = 6;
    
    [SerializeField] private GameObject sand;
    [SerializeField] private int beachLevel = 7;

    [SerializeField] private GameObject grass;
    [SerializeField] private int grassLevel = 8;

    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject snow;

    enum DisplayMode
    {
        ENTIRE_MAP,
        EACH_CHUNK
    }

    [SerializeField] private DisplayMode displayMode;

    Dictionary<Vector2, Chunk> chunksDictionary = new Dictionary<Vector2, Chunk>();
    static List<Chunk> chunksVisibleLastUpdate = new List<Chunk>();

    private Vector2Int currentPlayerPosition;
    void Start()
    {
        if (displayMode == DisplayMode.EACH_CHUNK)
        {
            for (int x = 0; x < chunks.x; x++)
            {
                for (int y = 0; y < chunks.y; y++)
                {
                    InstantiateChunk(new Vector2Int(x, y));
                }
            }
        }
        else if (displayMode == DisplayMode.ENTIRE_MAP)
        {
            CreateMap();
        }
    }

    void CreateMap()
    {
        int[,,] map = new int[mapSize,mapHeight,mapSize];
        map = Noise.MapGeneration3D(mapSize, mapHeight,undergroundHeight,Noise.SurfaceMapGeneration(offset, mapSize, mapHeight, seed, frequency, octaves),blocksNrBeforeStone);
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int z = 0; z < mapSize; z++)
                {
                    // 0 = air
                    // 1 = dirt
                    // 2 = stone
                    // 3 = grass
                    // 4 = snow
                    // 5 = sand
                    switch (map[x,y,z])
                    {
                        case 0:
                            break;
                        case 1:
                            Instantiate(dirt, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(stone, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(grass, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(snow, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        case 5:
                            Instantiate(sand, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    void CheckPlayer(Vector3 PlayerPosition, Vector2Int currentPlayerPosition)
    {
        if (PlayerPosition.x < currentPlayerPosition.x - chunkSize)
        {
            currentPlayerPosition.x -= 1;
        }
        else if (PlayerPosition.y < currentPlayerPosition.y - chunkSize)
        {
            currentPlayerPosition.y -= 1;
        }
        else if (PlayerPosition.x > currentPlayerPosition.x + chunkHeight)
        {
            currentPlayerPosition.x += 1;
        }
        else if (PlayerPosition.y > currentPlayerPosition.y + chunkHeight)
        {
            currentPlayerPosition.y += 1;
        }
    }

    /*void UpdateVisibleChunks()
    {
        for (int i = 0; i < chunksVisibleLastUpdate.Count; i++)
        {
            //chunksVisibleLastUpdate[i].SetVisible(false);
        }
        chunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(currentPlayerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(currentPlayerPosition.y / chunkSize);
        for (int yOffset = -VisibleChunksDist; yOffset <= VisibleChunksDist; yOffset++)
        {
            for (int xOffset = -VisibleChunksDist; xOffset <= VisibleChunksDist; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (chunksDictionary.ContainsKey(viewedChunkCoord))
                {
                    //chunksDictionary[viewedChunkCoord].UpdateTerrainChunk();
                }
                else
                {
                    chunksDictionary.Add(viewedChunkCoord, new Chunk());
                }
            }
        }
    }*/

    void DisplayChunks()
    {
        
    }

    void InstantiateChunk(Vector2Int position)
    {
        int[,] map = Noise.SurfaceMapGeneration(offset + new Vector2(chunkSize * position.x, chunkSize * position.y), chunkSize, chunkHeight, seed, frequency, octaves);
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                if (map[x,y] < waterLevel)
                {
                    Instantiate(water, new Vector3(x + (chunkSize * position.y), waterLevel, y + (chunkSize * position.x)), Quaternion.identity);
                }
                else if(map[x,y] < beachLevel)
                {
                    Instantiate(sand, new Vector3(x + (chunkSize * position.y), waterLevel, y + (chunkSize * position.x)), Quaternion.identity);

                }
                else
                {
                    Instantiate(grass, new Vector3(x + (chunkSize * position.y), map[x, y], y + (chunkSize * position.x)), Quaternion.identity);
                }
            }
        }
    }

    public struct Chunk
    {
        public int[,,] blocks;

        Chunk(int chunkSize)
        {
            blocks = new int[chunkSize, chunkSize, chunkSize];
        }
    }
}

