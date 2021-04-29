using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private Tilemap topMap;
    [SerializeField]
    private Tile floorTile;
    [SerializeField]
    private Tile wallTile;
    [SerializeField]
    private Tile midTile;
    [SerializeField]
    private Tile topTile;
    [SerializeField]
    private int recursionLimit;
    [SerializeField]
    private int maxRoomSize;
    [SerializeField]
    private int minRoomSize;
    [SerializeField]
    private int maxCorridorLength;
    [SerializeField]
    private int minCorridorLength;

    private Vector3Int startPos;


    private void Awake()
    {

        startPos = new Vector3Int(0, 0, 0);

        int bossPathLock = 0;

        int startSide = 0;

        generate(startPos, startSide, recursionLimit, bossPathLock, true);

        fillWalls();

        configAI();
    }

    private void configAI() {
        
        BoundsInt bounds = floorMap.cellBounds;
        int width = bounds.xMax - bounds.xMin + 2;
        int depth = bounds.yMax - bounds.yMin + 2;
        Vector3 center = bounds.center;
        center.z = 0;

        AstarData data = AstarPath.active.data;

        GridGraph gg = data.FindGraphOfType(typeof(GridGraph)) as GridGraph;
        
        float nodeSize = 1;

        gg.center = center;

        gg.SetDimensions(width, depth, nodeSize);

        Invoke("Scan", 0.1f);
    }

    private void Scan()
    {
        AstarPath.active.Scan();
    }

    private void fillWalls()
    {
        BoundsInt bounds = floorMap.cellBounds;
        for (int xMap = bounds.xMax + 10; xMap >= bounds.xMin - 10; xMap--)
        {
            for (int yMap = bounds.yMax + 10; yMap >= bounds.yMin - 10; yMap--)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posThreeBelow = new Vector3Int(xMap, yMap - 3, 0);
                Vector3Int posTwoBelow = new Vector3Int(xMap, yMap - 2, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posTwoAbove = new Vector3Int(xMap, yMap + 2, 0);
                Vector3Int posThreeUp = new Vector3Int(xMap, yMap + 3, 0);
                Vector3Int posRight = new Vector3Int(xMap + 1, yMap, 0);
                Vector3Int posLeft = new Vector3Int(xMap - 1, yMap, 0);

                Vector3Int pos3DownRight = new Vector3Int(xMap + 1, yMap - 3, 0);
                Vector3Int pos3DownLeft = new Vector3Int(xMap - 1, yMap - 3, 0);
                Vector3Int posUpRight = new Vector3Int(xMap + 1, yMap + 1, 0);
                Vector3Int posUpLeft = new Vector3Int(xMap - 1, yMap + 1, 0);

                TileBase tile = floorMap.GetTile(pos);
                TileBase tileThreeBelow = floorMap.GetTile(posThreeBelow);
                TileBase tileTwoBelow = floorMap.GetTile(posTwoBelow);
                TileBase tileBelow = floorMap.GetTile(posBelow);
                TileBase tileAbove = floorMap.GetTile(posAbove);
                TileBase tileRight = floorMap.GetTile(posRight);
                TileBase tileLeft = floorMap.GetTile(posLeft);

                TileBase tile3DownRight = floorMap.GetTile(pos3DownRight);
                TileBase tile3DownLeft = floorMap.GetTile(pos3DownLeft);
                TileBase tile2DownRight = floorMap.GetTile(new Vector3Int(xMap + 1, yMap - 2, 0));
                TileBase tile2DownLeft = floorMap.GetTile(new Vector3Int(xMap - 1, yMap - 2, 0));
                TileBase tileDownRight = floorMap.GetTile(new Vector3Int(xMap + 1, yMap - 1, 0));
                TileBase tileDownLeft = floorMap.GetTile(new Vector3Int(xMap - 1, yMap - 1, 0));
                TileBase tileUpRight = floorMap.GetTile(posUpRight);
                TileBase tileUpLeft = floorMap.GetTile(posUpLeft);


                if (!tile)
                {
                    //Wall_Tilemap
                    if (tileAbove || tileRight || tileLeft || tileBelow) wallMap.SetTile(pos, wallTile);
                    else if(tileUpRight || tileUpLeft) wallMap.SetTile(pos, wallTile);

                    //Foreground_Tilemap

                    if (!tileBelow) 
                    {
                        if (tile3DownRight || tile3DownLeft) topMap.SetTile(pos, topTile);
                        else if(tile2DownRight || tile2DownLeft) topMap.SetTile(pos, topTile);
                        else if(tileDownRight || tileDownLeft) topMap.SetTile(pos, topTile);
                        else if (tileRight || tileLeft) topMap.SetTile(pos, midTile);
  
                        if (tileTwoBelow) topMap.SetTile(pos, midTile);
                        else if (tileThreeBelow) topMap.SetTile(pos, topTile);
                    }
                    

                }
                else {
                    //Foreground_Tilemap
                    if (!tileBelow) {
                        if (!(tile3DownRight || tile3DownLeft) ) topMap.SetTile(pos, midTile);
                        else topMap.SetTile(pos, topTile); 

                        if(tileAbove && (tileDownLeft != tileDownRight) && !tileTwoBelow) topMap.SetTile(pos, topTile);
                    } else if (!tileTwoBelow) topMap.SetTile(pos, topTile);
                }
            }
        }
    }

    private void generate(Vector3Int currPos, int entranceSide, int limit, int bossPathLock, bool isBossPath) {
        if (limit < 1) return;

        if (limit < 2 && isBossPath)
        {
            Debug.Log("Spawning boss room. isbossPath = " + isBossPath);
            spawnBossRoom(currPos, entranceSide);
            return;
        }

        //Spawns randomly sized room at current position
        int roomWidth = Random.Range(minRoomSize, maxRoomSize);
        if (roomWidth % 2 == 0) roomWidth--;
        int roomHeight = Random.Range(minRoomSize, maxRoomSize);
        if (roomHeight % 2 == 0) roomHeight--;
        spawnRoom(roomWidth, roomHeight, currPos, entranceSide);

        if(limit < 2)
            return;

        //Centralizes position in room
        //entranceSide --> 0 = bottom, 1 = right, 2 = top, 3 = left
        switch (entranceSide) {
            case 0: currPos.y += roomHeight / 2; break;
            case 1: currPos.x -= roomWidth  / 2; break;
            case 2: currPos.y -= roomHeight / 2; break;
            case 3: currPos.x += roomWidth  / 2; break;
        }

        //Determining which directions to go and making sure there's at least one
        bool goingStraight = false, goingRight = false, goingLeft = false;
        if (Random.value < 0.5f) goingStraight = true;
        if (Random.value < 0.5f) goingRight    = true;
        if (Random.value < 0.5f) goingLeft     = true;
        if (!(goingStraight || goingRight || goingLeft)) 
        {
            int r = Random.Range(1,3);
            switch (r) {
                case 1: goingStraight = true; break;
                case 2: goingRight    = true; break;
                case 3: goingLeft     = true; break;
            }
        }

        //Assigns a singular boss path
        bool bossIsStraight = false; bool bossIsRight = false; bool bossIsLeft = false;
        if (isBossPath) {
            bool potentialStraight = invertDir(entranceSide, "straight") != bossPathLock;
            bool potentialRight    = invertDir(entranceSide, "right"   ) != bossPathLock;
            bool potentialLeft     = invertDir(entranceSide, "left"    ) != bossPathLock;

            //If no paths are potential boss paths, make one (COULD USE SOME RANDOMNESS LATER BUT THIS WORKS FOR NOW)
            if (!((potentialStraight && goingStraight) || (potentialRight && goingRight) || (potentialLeft && goingLeft))) 
            {
                if (potentialStraight  ) goingStraight = true;
                else if (potentialRight) goingRight    = true;
                else if (potentialLeft ) goingLeft     = true;
            }

            //Will go Straight if possible, otherwise to the Right, otherwise to the Left
            if (potentialStraight && goingStraight) bossIsStraight = true;
            else if (potentialRight && goingRight) bossIsRight = true;
            else if (potentialLeft && goingLeft) bossIsLeft = true;
            

            Debug.Log("S: " + bossIsStraight + " " + goingStraight + " R: " + bossIsRight + " " + goingRight + " L: " + bossIsLeft + " " + goingLeft + " LIMIT: " + limit);
        }

        

        if (goingStraight) //Going straight
        {
            int straightDir = invertDir(entranceSide, "straight");
            Vector3Int straightPos = doorPos(currPos, straightDir, roomWidth, roomHeight);
            path(straightPos, straightDir, limit, bossPathLock, bossIsStraight);
        }
        if (goingRight) //Going right
        {
            int rightDir = invertDir(entranceSide, "right");
            Vector3Int rightPos = doorPos(currPos, rightDir, roomWidth, roomHeight);
            path(rightPos, rightDir, limit, bossPathLock, bossIsRight);
        }
        if (goingLeft) //Going left
        {
            int leftDir = invertDir(entranceSide, "left");
            Vector3Int leftPos = doorPos(currPos, leftDir, roomWidth, roomHeight);
            path(leftPos, leftDir, limit, bossPathLock, bossIsLeft);

        }
    }

    private void spawnBossRoom(Vector3Int pos, int entranceSide) {
        int dir = invertDir(entranceSide, "straight");
        spawnCorridor(50, pos, dir);

        Vector3Int enterPos = pos;

        switch (dir)
        {
            case 0: enterPos.y -= 50; break;
            case 1: enterPos.x += 50; break;
            case 2: enterPos.y += 50; break;
            case 3: enterPos.x -= 50; break;
        }

        spawnRoom(50, 50, enterPos, entranceSide);

    }

    //Returns position to the middle of the exit side of a room given the room's center position and dimensions
    private Vector3Int doorPos(Vector3Int pos, int dir, int roomWidth, int roomHeight) {

        switch (dir) {
            case 0: pos.y -= roomHeight / 2; break;
            case 1: pos.x += roomWidth  / 2; break;
            case 2: pos.y += roomHeight / 2; break;
            case 3: pos.x -= roomWidth  / 2; break;
        }

        return pos;
    }

    //Returns the direction of a corridor given the origin room's entrance side and a relative direction
    private int invertDir(int entranceSide, string change) {

        int dir = 0;

        //Straight direction is opposite of entrance side
        switch (entranceSide) {
            case 0: dir = 2; break;
            case 1: dir = 3; break;
            case 2: dir = 0; break;
            case 3: dir = 1; break;
        }

        return changeDir(dir, change);
    }

    private int changeDir(int dir, string change) {

        switch (change)
        {
            //case "straight dir = dir";
            case "right": dir -= 1; break;
            case "left": dir += 1; break;
        }

        //Makes it loop around
        if (dir == -1) dir = 3;
        else if (dir == 4) dir = 0;

        return dir;
    }

    //dir --> 0 = down, 1 = right, 2 = up, 3 = left
    private void path(Vector3Int startPos, int dir, int limit, int bossPathLock, bool isBossPath) {
        Vector3Int exitPos = startPos;

        //Randomizing length of corridor
        int length = Random.Range(minCorridorLength, maxCorridorLength);

        //Sets position of exit depending on direction
        switch (dir)
        {
            case 0: exitPos.y -= length; break;
            case 1: exitPos.x += length; break;
            case 2: exitPos.y += length; break;
            case 3: exitPos.x -= length; break;
        }

        //Checking if the end of the corridor has found generated floor
        bool stop = floorMap.HasTile(exitPos);

        //Spawning the corridor
        spawnCorridor(length, startPos, dir);

        //Spawn a room if end of recursion limit is soon to be reached
        if (limit < 2 && !stop && !isBossPath) { 
            dir = invertDir(dir, "straight");
            generate(exitPos, dir, 1, 1, isBossPath);
        }

        if (stop && !isBossPath)                        //Stop corridor here and don't spawn room
        {
            return;
        }
        else if (Random.value < 0.5f || isBossPath)    //Stop corridor here and DO spawn room | Does no extra turns if on path to boss for simplicity
        {
            dir = invertDir(dir, "straight");
            generate(exitPos, dir, limit - 1, bossPathLock, isBossPath);
        }
        else if (Random.value < 0.5f)    //Turn corridor to the right
        {
            dir = changeDir(dir, "right");
            for (int x = 0; x < 3; x++) {
                for (int y = 0; y < 3; y++) {
                    floorMap.SetTile(new Vector3Int(exitPos.x + x - 1, exitPos.y + y - 1, 0), floorTile);
                }
            }
            path(exitPos, dir, limit - 1, bossPathLock, false);
        }
        else                             //Turn corridor to the left
        {
            dir = changeDir(dir, "left");
            for (int x = 0; x < 3; x++) {
                for (int y = 0; y < 3; y++) {
                    floorMap.SetTile(new Vector3Int(exitPos.x + x - 1, exitPos.y + y - 1, 0), floorTile);
                }
            }
            path(exitPos, dir, limit - 1, bossPathLock, false);
        }
    }

    //x and y are coordinates for bottom left corner
    private bool HasTile(int x, int y, int width, int height, Tilemap map) {

        for (int i = x; i <= x + width; i++) {
            for (int j = y; j <= y + height; j++) {
                if (map.HasTile(new Vector3Int(i, j, 0))) return true;
            }
        }

        return false;
    }

    //Puts a tile at specified coordinates
    private void putTile(int x, int y, Tilemap map, Tile tile)
    {
        map.SetTile(new Vector3Int(x, y, 0), tile);
    }

    //entranceSide --> 0 = bottom, 1 = right, 2 = top, 3 = left
    private void spawnRoom(int width, int height, Vector3Int entrancePos, int entranceSide) {

        int xOffset;
        int yOffset;

        switch (entranceSide) 
        {
            case 0:
                xOffset = -width / 2 + entrancePos.x;
                yOffset = entrancePos.y;
                break;
            case 1:
                xOffset = -width + entrancePos.x;
                yOffset = -height / 2 + entrancePos.y;
                break;
            case 2:
                xOffset = -width / 2 + entrancePos.x;
                yOffset = -height + entrancePos.y;
                break;
            case 3:
                xOffset = entrancePos.x;
                yOffset = -height / 2 + entrancePos.y;
                break;
            default:
                xOffset = entrancePos.x;
                yOffset = entrancePos.y;
                break;
        }

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                putTile(x + xOffset, y + yOffset, floorMap, floorTile);
            }
        }
    }

    //dir --> 0 = down, 1 = right, 2 = up, 3 = left
    private void spawnCorridor(int length, Vector3Int entrancePos, int dir) {
        int xOffset = 0;
        int yOffset = 0;

        if (dir == 0 || dir == 2)
        {
            if (dir == 0)
            {
                yOffset = -length;
            }

            for (int i = 0; i < length; i++)
            {
                putTile(entrancePos.x - 1, entrancePos.y + yOffset + i, floorMap, floorTile);
                putTile(entrancePos.x, entrancePos.y + yOffset + i, floorMap, floorTile);
                putTile(entrancePos.x + 1, entrancePos.y + yOffset + i, floorMap, floorTile);
            }
        }
        else if (dir == 1 || dir == 3)
        {
            if (dir == 3)
            {
                xOffset = -length;
            }

            for (int i = 0; i < length; i++)
            {
                putTile(entrancePos.x + xOffset + i, entrancePos.y - 1, floorMap, floorTile);
                putTile(entrancePos.x + xOffset + i, entrancePos.y, floorMap, floorTile);
                putTile(entrancePos.x + xOffset + i, entrancePos.y + 1, floorMap, floorTile);
            }
        }
    }
}