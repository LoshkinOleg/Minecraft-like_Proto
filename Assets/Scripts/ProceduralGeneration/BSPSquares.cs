using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPSquares : MonoBehaviour
{
    private List<Zone> allZones;
    private List<Zone> finalZones;
    private int index = 0;
    struct Zone
    {
        public Zone(int zId, int t, int b, Vector2Int s, Vector2Int e)
        {
            zoneID = zId;
            terrain = t;
            biome = b;
            start = s;
            end = e;
            neighboursIndexes = new List<int>();
        }
        public int zoneID;
        public int terrain;
        public int biome;
        public Vector2Int start;
        public Vector2Int end;
        public List<int> neighboursIndexes;
    }

    private void GenerateZones(int mapSize, int bspCutIterations, int bspCutPercentage)
    {
        //Cut Map
        Zone mapZone = new Zone(index++, 0, 0, new Vector2Int(0,0),new Vector2Int(mapSize,mapSize));
        CutZone(mapZone, bspCutIterations, bspCutPercentage);

        //Generate Mountain

        //Generate Mountain Terrain

        //Find Mountain Neighbors to generate Hills

        //Generate Hills

        //Lerp between mountains and hills

        //Find hills neightbours (that aren't hills or mountain)

        //Lerp between flatbiome and hills


    }
    private void CutZone(Zone ParentZone, int cutIteration, float maxCutPercentage)
    {
        if (maxCutPercentage > 1)
        {
            maxCutPercentage = 1;
        }

        Zone childZone1;
        Zone childZone2;
        if (ParentZone.end.x - ParentZone.start.x > ParentZone.end.y - ParentZone.start.y)
        {
            //Cut in x
            int distance = ParentZone.end.x - ParentZone.start.x;
            int random = Random.Range((int)((distance /2) - ((distance/2) * maxCutPercentage)), (int)(((distance /2) + ((distance / 2) * maxCutPercentage))));

            childZone1 = new Zone(index++, 0,0, ParentZone.start, new Vector2Int(ParentZone.end.x - random, ParentZone.end.y));
            childZone2 = new Zone(index++, 0, 0, new Vector2Int(ParentZone.start.x + random, ParentZone.start.y), ParentZone.end);
        }
        else
        {
            //Cut in y
            int distance = ParentZone.end.y - ParentZone.start.y;
            int random = Random.Range((int)((distance / 2) - ((distance / 2) * maxCutPercentage)), (int)(((distance / 2) + ((distance / 2) * maxCutPercentage))));

            childZone1 = new Zone(index++, 0, 0, ParentZone.start, new Vector2Int(ParentZone.end.x, ParentZone.end.y - random));
            childZone2 = new Zone(index++, 0, 0, new Vector2Int(ParentZone.start.x, ParentZone.start.y + random), ParentZone.end);
        }
        //TODO Generate Neighbours
        allZones.Add(childZone1);
        allZones.Add(childZone2);
        if (cutIteration <= 0)
        {
            finalZones.Add(childZone1);
            finalZones.Add(childZone2);
        }
        else
        {
            CutZone(childZone1, cutIteration - 1, maxCutPercentage);
            CutZone(childZone2, cutIteration - 1, maxCutPercentage);
        }

    }

    void LerpBlock(int[,] map, Vector2Int startPos, int lerpHeight, bool yPos, bool yNeg, bool xPos, bool xNeg)
    {
        //TODO check if value is higher

        if (yPos)
        {
            if (map[startPos.x, startPos.y] - lerpHeight > map[startPos.x, startPos.y + 1])
            {
                map[startPos.x, startPos.y + 1] = map[startPos.x, startPos.x] - lerpHeight;
                LerpBlock(map, new Vector2Int(startPos.x, startPos.y + 1), lerpHeight, yPos, yNeg, xPos, xNeg);
            }
        }

        if (yNeg)
        {
            if (map[startPos.x, startPos.y] - lerpHeight > map[startPos.x, startPos.y - 1])
            {
                map[startPos.x, startPos.y - 1] = map[startPos.x, startPos.x] - lerpHeight;
                LerpBlock(map, new Vector2Int(startPos.x, startPos.y - 1), lerpHeight, yPos, yNeg, xPos, xNeg);
            }

            if (xPos)
            {
                if (map[startPos.x, startPos.y] - lerpHeight > map[startPos.x + 1, startPos.y])
                {
                    map[startPos.x + 1, startPos.y] = map[startPos.x, startPos.x] - lerpHeight;
                    LerpBlock(map, new Vector2Int(startPos.x + 1, startPos.y), lerpHeight, yPos, yNeg, xPos, xNeg);
                }
            }

            if (xNeg)
            {
                if (map[startPos.x, startPos.y] - lerpHeight > map[startPos.x - 1, startPos.y])
                {
                    map[startPos.x - 1, startPos.y] = map[startPos.x, startPos.x] - lerpHeight;
                    LerpBlock(map, new Vector2Int(startPos.x - 1, startPos.y), lerpHeight, yPos, yNeg, xPos, xNeg);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
