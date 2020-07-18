using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BSP : MonoBehaviour
{
    struct TestPoint
    {
        public Vector2Int position;
        private int type;
    }

    struct Zone
    {
        Zone(Vector2Int[] b1, Vector2Int[] b2, Vector2Int[] b3, Vector2Int[] b4)
        {
            border1 = b1;
            border2 = b2;
            border3 = b3;
            border4 = b4;
        }
        public Vector2Int[] border1;
        public Vector2Int[] border2;
        public Vector2Int[] border3;
        public Vector2Int[] border4;
    }

    struct Border
    {
        public Vector2Int point1;
        public Vector2Int point2;
    }

    void MapBSP(int mapSize, int iterations, int maxDistFromMiddleCutPercent)
    {
        int[,] map = new int[mapSize,mapSize];

        int random1 = Random.Range((int)((mapSize / 2) - mapSize* maxDistFromMiddleCutPercent), (int)((mapSize / 2) + mapSize * maxDistFromMiddleCutPercent));
        int random2 = Random.Range((int)((mapSize / 2) - mapSize * maxDistFromMiddleCutPercent), (int)((mapSize / 2) + mapSize * maxDistFromMiddleCutPercent));
        Vector2Int cutPoint1 = new Vector2Int(random1, 0);
        Vector2Int cutPoint2 = new Vector2Int(random2, mapSize);
        
        //Vector2Int cutVector2 = new Vector2Int(cutPoint2.x - cutPoint1.x, cutPoint2.y - cutPoint1.y);
    }

    int CheckDirection(Vector2Int p1, Vector2Int p2)
    {
        int direction = -1;

        //Check Directions
        // 0 = Right
        // 1 = Left
        // 2 = Up
        // 3 = Down
        if (Mathf.Abs(p1.x) - Mathf.Abs(p2.x) > Mathf.Abs(p1.y) - Mathf.Abs(p2.y))
        {
            if (p1.x - p2.x > 0)
            {
                //Right
                direction = 0;
            }
            else
            {
                //Left
                direction = 1;
            }
        }
        else
        {
            if (p1.y - p2.y > 0)
            {
                //Up
                direction = 2;
            }
            else
            {
                //Right
                direction = 3;
            }
        }
        return direction;
    }

    List<TestPoint> CheckPoints(int direction)
    {
        List<TestPoint> points = new List<TestPoint>();
        return points;
    }

    void GenerateZones(Zone zone, int maxDistFromMiddleCutPercent, bool borderToCutIsEven)
    {
        //Step 1: Get Zone

        //Step 2: Chose 1 side and opposite of the Zone

        //Step 3: Chose 1 pts on each side

        if (borderToCutIsEven)
        {
            int random1 = Random.Range((zone.border2.Length / 2) - (zone.border2.Length / 2) * maxDistFromMiddleCutPercent, (zone.border2.Length / 2) + (zone.border2.Length / 2) * maxDistFromMiddleCutPercent);
            int random2 = Random.Range((zone.border4.Length / 2) - (zone.border4.Length / 2) * maxDistFromMiddleCutPercent, (zone.border4.Length / 2) + (zone.border4.Length / 2) * maxDistFromMiddleCutPercent);
        }

        //Step 4: Create two Chlid Zones

        Zone zone1;
        Zone zone2;

        //Step 5: Generate the known pts

        //Step 6: Add the unknown pts to the Zone

    }

}
