using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private GameObject torch;
    [SerializeField]
    private Tilemap debugMap;
    [SerializeField]
    private Tile debugTile;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject bossDoorFront;
    [SerializeField]
    private GameObject bossDoorSide;
    [SerializeField]
    private GameObject doorFront;
    [SerializeField]
    private GameObject doorSide;
    [SerializeField]
    private GameObject treasure;
    [SerializeField]
    private GameObject meleeEnemy;
    [SerializeField]
    private GameObject rangedEnemy;
    [SerializeField]
    private Tilemap floorMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private Tilemap foregroundMap;
    [SerializeField]
    private Tilemap topMap;
    [SerializeField]
    private Tile[] tiles = new Tile[12];
    [SerializeField]
    private int recursionLimit;
    [SerializeField]
    private int roomWidth;
    [SerializeField]
    private int roomHeight;
    [SerializeField]
    private int corridorlength;

    private bool treasureHasSpawned;

    private List<GameObject> enemies = new List<GameObject>();

    private Vector3Int startPos;

    private Vector3 gridOffset;


    private void Awake()
    {
        gridOffset = gameObject.transform.position;

        startPos = new Vector3Int(0, 0, 0);

        int bossPathLock = Random.Range(0, 4);

        float r = Random.value;
        int startSide;

        if (r < 1/3) startSide = changeDir(bossPathLock, "straight");
        else if (r < 2/3) startSide = changeDir(bossPathLock, "right");
        else startSide = changeDir(bossPathLock, "left");



        generate(startPos, startSide, recursionLimit, bossPathLock, true);

        fillWalls();

        configAI();

        Invoke("enableEnemies", 0.2f);
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
                    
                    //foreground and top
                    if (!tileThreeBelow && !tileTwoBelow && !tileBelow) 
                    {
                        topMap.SetTile(pos, tiles[3]);//No line

                        if (tile2DownRight) topMap.SetTile(pos, tiles[6]); //Right line
                        else if (tile2DownLeft) topMap.SetTile(pos, tiles[7]); //Left line
                    }
                    else if (!tileTwoBelow && !tileBelow) 
                    {
                        topMap.SetTile(pos, tiles[5]);//Bottom line
                        if(tileRight) topMap.SetTile(pos, tiles[8]); //Corner down right
                        else if(tileLeft) topMap.SetTile(pos, tiles[9]); //Corner down left
                    }
                    else if (!tileBelow) foregroundMap.SetTile(pos, tiles[2]); 
                    //Wall_Tilemap
                    if (tileAbove || tileRight || tileLeft || tileBelow) wallMap.SetTile(pos, tiles[1]);
                    else if (tileUpRight || tileUpLeft) wallMap.SetTile(pos, tiles[1]);
                }
                else
                {
                    //top
                    if (!tileBelow)
                    {
                        topMap.SetTile(pos, tiles[3]);
                        if (tileDownRight) topMap.SetTile(pos, tiles[6]); //Right line
                        else if (tileDownLeft) topMap.SetTile(pos, tiles[7]); //Left line
                    }
                    else if (!tileTwoBelow)
                    {
                        topMap.SetTile(pos, tiles[4]); //Top line
                        if (tile2DownRight) topMap.SetTile(pos, tiles[10]); //Corner up right
                        else if (tile2DownLeft) topMap.SetTile(pos, tiles[11]); //Corner up left 

                    }
                }
            }
        }
    }

    private void generate(Vector3Int currPos, int entranceSide, int limit, int bossPathLock, bool isBossPath) {

        //debug
        //debugMap.SetTile(currPos ,debugTile);

        //Spawns room at current position
        spawnRoom(roomWidth, roomHeight, currPos, entranceSide);

        //Centralizes position in room and spawn door
        //entranceSide --> 0 = bottom, 1 = right, 2 = top, 3 = left
        switch (entranceSide) {
            case 0: Instantiate(doorFront, currPos + new Vector3(0.5f, 1, 0) + gridOffset, Quaternion.identity); currPos.y += roomHeight / 2;  break;
            case 1: Instantiate(doorSide, currPos + new Vector3(1.1f, 2.5f, 0) + gridOffset, Quaternion.identity); currPos.x -= roomWidth  / 2;  break;
            case 2: Instantiate(doorFront, currPos + new Vector3(0.5f, 2f, 0) + gridOffset, Quaternion.identity); currPos.y -= roomHeight / 2;  break;
            case 3: Instantiate(doorSide, currPos + new Vector3(-0.1f, 2.5f, 0) + gridOffset, Quaternion.identity); currPos.x += roomWidth  / 2; break;
        }

        //Puts light in middle of rooms
        Instantiate(torch, currPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);

        //Centralizes player in spawn room
        if (limit == recursionLimit)
            GameObject.Find("Player").transform.position = currPos;

        //Spawns bossroom
        if (limit < 2 && isBossPath)
        {
            spawnBossRoom(currPos, bossPathLock);
            return;
        }
        else if (limit < 2 && !treasureHasSpawned) //Spawns treasure in one random room on the edge
        {
            spawnTreasure(currPos);
            return;
        }
        else if (limit != recursionLimit) //Spawns enemies
        {
            spawnEnemies(currPos);
            if (limit < 2)
                return;
        }

        //Determining which directions to go and making sure there's at least one
        bool goingStraight = false, goingRight = false, goingLeft = false;
        if (Random.value < 0.5f) goingStraight = true;
        if (Random.value < 0.5f) goingRight    = true;
        if (Random.value < 0.5f) goingLeft     = true;
        if (!(goingStraight || goingRight || goingLeft)) 
        {
            int r = Random.Range(1,4);
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

    private void spawnTreasure(Vector3Int pos) {
        treasureHasSpawned = true;

        Instantiate(treasure, pos, Quaternion.identity);
    }

    private void spawnBossRoom(Vector3Int pos, int entranceSide)
    {
        int dir = invertDir(entranceSide, "straight");
        spawnCorridor(50, pos, dir);

        Vector3Int enterPos = pos;

        switch (dir)
        {
            case 0: enterPos.y -= 50; Instantiate(bossDoorFront, enterPos + new Vector3(0.5f, 2f, 0) + gridOffset, Quaternion.identity); break;
            case 1: enterPos.x += 50; Instantiate(bossDoorSide, enterPos + new Vector3(-0.1f, 2.5f, 0) + gridOffset, Quaternion.identity); break;
            case 2: enterPos.y += 50; Instantiate(bossDoorFront, enterPos + new Vector3(0.5f, 1, 0) + gridOffset, Quaternion.identity); break;
            case 3: enterPos.x -= 50; Instantiate(bossDoorSide, enterPos + new Vector3(+1.1f, 2.5f, 0) + gridOffset, Quaternion.identity); break;
        }

        
        spawnRoom(39, 31, enterPos, entranceSide);

        switch (dir)
        {
            case 0: enterPos.y -= 16; break;
            case 1: enterPos.x += 20; break;
            case 2: enterPos.y += 16; break;
            case 3: enterPos.x -= 20; break;
        }

        Instantiate(boss, enterPos, Quaternion.identity);

    }

    //dir --> 0 = down, 1 = right, 2 = up, 3 = left
    private void path(Vector3Int startPos, int dir, int limit, int bossPathLock, bool isBossPath)
    {
        //debugMap.SetTile(startPos, debugTile);
        Vector3Int exitPos = startPos;

        //Sets position of exit depending on direction
        switch (dir)
        {
            case 0: exitPos.y -= corridorlength; Instantiate(doorFront, startPos + new Vector3(0.5f, 1f, 0) + gridOffset, Quaternion.identity); break;
            case 1: exitPos.x += corridorlength; Instantiate(doorSide , startPos + new Vector3(1.1f, 2.5f, 0) + gridOffset, Quaternion.identity); break;
            case 2: exitPos.y += corridorlength; Instantiate(doorFront, startPos + new Vector3(0.5f, 2f, 0) + gridOffset, Quaternion.identity); break;
            case 3: exitPos.x -= corridorlength; Instantiate(doorSide , startPos + new Vector3(-0.1f, 2.5f, 0) + gridOffset, Quaternion.identity); break;
        }
        //debugMap.SetTile(exitPos, debugTile);

        //Checking if the end of the corridor has found generated floor
        bool stop = floorMap.HasTile(exitPos);

        //Spawning the corridor
        spawnCorridor(corridorlength, startPos, dir);

        //Spawn a room if end of recursion limit is soon to be reached
        if (limit < 2 && !stop && !isBossPath)
        {
            dir = invertDir(dir, "straight");
            generate(exitPos, dir, 1, 1, isBossPath);
        } 
        else if (stop && !isBossPath)                        //Stop corridor here and don't spawn room
        {
            return; //Doesn't spawn entrance door if this happens, not important FIX LATER
        }
        else
        {
            dir = invertDir(dir, "straight");
            generate(exitPos, dir, limit - 1, bossPathLock, isBossPath);
        } 
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
                xOffset = -width + entrancePos.x + 1;
                yOffset = -height / 2 + entrancePos.y;
                break;
            case 2:
                xOffset = -width / 2 + entrancePos.x;
                yOffset = -height + entrancePos.y + 1;
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
                putTile(x + xOffset, y + yOffset, floorMap, tiles[0]);
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
                putTile(entrancePos.x - 1, entrancePos.y + yOffset + i, floorMap, tiles[0]);
                putTile(entrancePos.x, entrancePos.y + yOffset + i, floorMap, tiles[0]);
                putTile(entrancePos.x + 1, entrancePos.y + yOffset + i, floorMap, tiles[0]);
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
                putTile(entrancePos.x + xOffset + i, entrancePos.y - 1, floorMap, tiles[0]);
                putTile(entrancePos.x + xOffset + i, entrancePos.y, floorMap, tiles[0]);
                putTile(entrancePos.x + xOffset + i, entrancePos.y + 1, floorMap, tiles[0]);
            }
        }
    }

    private void spawnEnemy(Vector3Int pos, string type)
    {
        GameObject enemy;
        switch (type)
        {
            case "melee": enemy = Instantiate(meleeEnemy, pos, Quaternion.identity); break;
            case "ranged": enemy = Instantiate(rangedEnemy, pos, Quaternion.identity); break;
            default: enemy = Instantiate(meleeEnemy, pos, Quaternion.identity); break;
        }

        enemy.SetActive(false);
        enemies.Add(enemy);
    }
    private void spawnEnemies(Vector3Int pos)
    {


        int roomType = Random.Range(1, 4);

        switch (roomType)
        {
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Vector3Int tempPos = new Vector3Int(
                            pos.x - (roomWidth / 4) + ((roomWidth / 2) * i),
                            pos.y - (roomHeight / 4) + ((roomHeight / 2) * j),
                            pos.z
                            );
                        spawnEnemy(tempPos, "melee");
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Vector3Int tempPos = new Vector3Int(
                            pos.x - (roomWidth / 4) + ((roomWidth / 2) * i),
                            pos.y - (roomHeight / 4) + ((roomHeight / 2) * j),
                            pos.z
                            );
                        spawnEnemy(tempPos, "ranged");
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Vector3Int tempPos = new Vector3Int(
                            pos.x - (roomWidth / 4) + ((roomWidth / 2) * i),
                            pos.y - (roomHeight / 4) + ((roomHeight / 2) * j),
                            pos.z
                            );
                        if (Random.value < 0.5f)
                        {
                            spawnEnemy(tempPos, "melee");
                        }
                        else
                        {
                            spawnEnemy(tempPos, "ranged");
                        }
                    }
                }
                break;
        }
    }

    private void enableEnemies()
    {
        foreach (GameObject gameObject in enemies)
        {
            gameObject.SetActive(true);
        }
    }
}
