using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private Tile floorTile;

    [SerializeField]
    private Tilemap floorMap;

    [SerializeField]
    private Tile wallTile;

    [SerializeField]
    private Tilemap wallMap;

    [SerializeField]
    private Tile midTile;

    [SerializeField]
    private Tile topTile;

    [SerializeField]
    private Tilemap topMap;

    private int i = 0;


    private Vector3Int startPos;


    private void Awake()
    {
        int bossPathLock = 1;

        startPos = new Vector3Int(0, 0, 0);

        generate(startPos, 0, 10, bossPathLock, true);

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

        int roomWidth = Random.Range(9, 19);
        if (roomWidth % 2 == 0) roomWidth--;
        int roomHeight = Random.Range(9, 19);
        if (roomHeight % 2 == 0) roomHeight--;

        spawnRoom(roomWidth, roomHeight, currPos, entranceSide);
        if (limit < 3) return;

        
        bool goDown = (Random.value > 0.5f);
        bool goRight = (Random.value > 0.5f);
        bool goUp = (Random.value > 0.5f);
        bool goLeft = (Random.value > 0.5f);


        switch (entranceSide) {
            // Sets Disables origin path and centralizes position in room
            case 0: goDown  = false; currPos.y += roomHeight / 2; break;
            case 1: goRight = false; currPos.x -= roomWidth  / 2; break;
            case 2: goUp    = false; currPos.y -= roomHeight / 2; break;
            case 3: goLeft  = false; currPos.x += roomWidth  / 2; break;
        }

        //Guarantees at least one corridor
        if (!(goDown || goRight || goUp || goLeft))
        {
            float f = Random.value;
            switch (entranceSide)
            {
                case 0:
                    if (f < 1 / 3) { goRight = true; }
                    else if (f < 2 / 3) { goLeft = true; }
                    else { goUp = true; }
                    break;
                case 1:
                    if (f < 1 / 3) { goDown = true; }
                    else if (f < 2 / 3) { goLeft = true; }
                    else { goUp = true; }
                    break;
                case 2:
                    if (f < 1 / 3) { goRight = true; }
                    else if (f < 2 / 3) { goLeft = true; }
                    else { goDown = true; }
                    break;
                case 3:
                    if (f < 1 / 3) { goRight = true; }
                    else if (f < 2 / 3) { goDown = true; }
                    else { goUp = true; }
                    break;
            }
        }

        /*if (goDown) 
        {
            Vector3Int CD = path(currPos, 2, roomWidth, roomHeight);

            int dirD = CD.z;
            CD.z = 0;

            Vector3Int probeD = CD;

            switch (dirD) {
                case 0: probeD.y += 1; break;
                case 1: probeD.x += 1; break;
                case 2: probeD.y -= 1; break;
                case 3: probeD.x -= 1; break;
            }

            if (!floorMap.HasTile(probeD)) {
                generate(CD, dirD, limit - 1, bossPathLock, true);
            }

        }
        if (goRight)
        {
            Vector3Int CR = path(currPos, 1, roomWidth, roomHeight);

            int dirR = CR.z;
            CR.z = 0;

            Vector3Int probeR = CR;

            switch (dirR)
            {
                case 0: probeR.y += 1; break;
                case 1: probeR.x += 1; break;
                case 2: probeR.y -= 1; break;
                case 3: probeR.x -= 1; break;
            }

            if (!floorMap.HasTile(probeR))
            {
                generate(CR, dirR, limit - 1, bossPathLock, true);
            }
        }
        if (goUp)
        {
            Vector3Int CU = path(currPos, 0, roomWidth, roomHeight);

            int dirU = CU.z;
            CU.z = 0;

            Vector3Int probeU = CU;

            switch (dirU)
            {
                case 0: probeU.y += 1; break;
                case 1: probeU.x += 1; break;
                case 2: probeU.y -= 1; break;
                case 3: probeU.x -= 1; break;
            }

            if (!floorMap.HasTile(probeU))
            {
                generate(CU, dirU, limit - 1, bossPathLock, true);
            }
        }
        if (goLeft)
        {
            Vector3Int CL = path(currPos, 3, roomWidth, roomHeight);

            int dirL = CL.z;
            CL.z = 0;

            Vector3Int probeL = CL;

            switch (dirL)
            {
                case 0: probeL.y += 1; break;
                case 1: probeL.x += 1; break;
                case 2: probeL.y -= 1; break;
                case 3: probeL.x -= 1; break;
            }

            if (!floorMap.HasTile(probeL))
            {
                generate(CL, dirL, limit - 1, bossPathLock, true);
            }
        }*/
        
        if (goDown)
        {
            Vector3Int down = path(currPos, 2, roomWidth, roomHeight);
            Vector3Int probeDown = down; probeDown.y -= 1;
            if(!floorMap.HasTile(probeDown))
                generate(down, 2, limit - 1, bossPathLock, true);
        }
        if (goRight)
        {
            Vector3Int right = path(currPos, 1, roomWidth, roomHeight);
            Vector3Int probeRight = right; probeRight.x += 1;
            if (!floorMap.HasTile(probeRight))
                generate(right, 3, limit - 1, bossPathLock, true);
        }
        if (goUp)
        {
            Vector3Int up = path(currPos, 0, roomWidth, roomHeight);
            Vector3Int probeUp = up; probeUp.y += 1;
            if (!floorMap.HasTile(probeUp))
                generate(up, 0, limit - 1, bossPathLock, true);
        }
        if (goLeft)
        {
            Vector3Int left = path(currPos, 3, roomWidth, roomHeight);
            Vector3Int probeLeft = left; probeLeft.x -= 1;
            if (!floorMap.HasTile(probeLeft))
                generate(left, 1, limit - 1, bossPathLock, true);
        }

    }

    //dir --> 0 = up, 1 = right, 2 = down, 3 = left
    private Vector3Int path(Vector3Int currPos, int dir, int roomWidth, int roomHeight) {
        Vector3Int exitPos;
        int length = Random.Range(10, 20);
        switch (dir) {
            case 0: currPos.y += roomHeight / 2; exitPos = currPos; exitPos.y += length; break; //Up
            case 1: currPos.x += roomWidth  / 2; exitPos = currPos; exitPos.x += length; break; //Right
            case 2: currPos.y -= roomHeight / 2; exitPos = currPos; exitPos.y -= length; break; //Down
            case 3: currPos.x -= roomWidth  / 2; exitPos = currPos; exitPos.x -= length; break; //Left
            default:                             exitPos = currPos; break;
        }

        bool stop = HasTile(exitPos.x - 1, exitPos.y - 1, 3, 3, floorMap);
        spawnCorridor(length, currPos, dir);

        //Assign direction for room
        exitPos.z = dir;

        //50% chance of continuing
        if (Random.value > 0.5f || stop)
        {
            return exitPos;
        }

        currPos = exitPos;
        length = Random.Range(10, 20);

        /*if (Random.value > 0.5f) //Right (if dir is assumed to be forward)
        {
            switch (dir)
            {
                case 0: exitPos = pathTurn(1, length, currPos); break; //True right
                case 1: exitPos = pathTurn(2, length, currPos); break; //True down
                case 2: exitPos = pathTurn(3, length, currPos); break; //True left
                case 3: exitPos = pathTurn(0, length, currPos); break; //True up
            }
        }
        else                     //Left (if dir is assumed to be forward)
        {
            switch (dir)
            {
                case 0: exitPos = pathTurn(3, length, currPos); break; //True left
                case 1: exitPos = pathTurn(0, length, currPos); break; //True up
                case 2: exitPos = pathTurn(1, length, currPos); break; //True right
                case 3: exitPos = pathTurn(2, length, currPos); break; //True down
            }
        }*/

            if (Random.value > 0.5f) //Right (if dir is assumed to be forward)
            {
                switch (dir) {
                    case 0: //True right
                        currPos.x -= 1;
                        Vector3Int probeRightR = exitPos; probeRightR.x += 9;
                        if (!floorMap.HasTile(probeRightR)) 
                        {
                            spawnCorridor(length, currPos, 1);
                            exitPos.x += length;
                        }
                        break;
                    case 1: //True down
                        currPos.y += 2;
                        Vector3Int probeDownR = exitPos; probeDownR.y -= 9;
                        if (!floorMap.HasTile(probeDownR))
                        {
                            spawnCorridor(length + 1, currPos, 2);
                            exitPos.y -= length;
                        }
                        break;
                    case 2: //True left
                        currPos.x += 2;
                        Vector3Int probeLeftR = exitPos; probeLeftR.x -= 9;
                        if (!floorMap.HasTile(probeLeftR))
                        {
                            spawnCorridor(length + 1, currPos, 3);
                            exitPos.x -= length;
                        }
                        break;
                    case 3: //True up
                        currPos.y -= 1;
                        Vector3Int probeUpR = exitPos; probeUpR.y += 9;
                        if (!floorMap.HasTile(probeUpR))
                        {
                            spawnCorridor(length, currPos, 0);
                            exitPos.y += length;
                        }
                        break;

                    default: break;
                }
            }
            else                     //Left (if dir is assumed to be forward)
            {
                switch (dir)
                {
                    case 0: //True left
                        currPos.x += 2;
                        Vector3Int probeLeftL = exitPos; probeLeftL.x -= 9;
                        if (!floorMap.HasTile(probeLeftL))
                        {
                            spawnCorridor(length + 1, currPos, 3);
                            exitPos.x -= length;
                        }
                        break;
                    case 1: //True up
                        currPos.y -= 1;
                        Vector3Int probeUpL = exitPos; probeUpL.y += 9;
                        if (!floorMap.HasTile(probeUpL))
                        {
                            spawnCorridor(length, currPos, 0);
                            exitPos.y += length;
                        }
                        break;
                    case 2: //True right
                        currPos.x -= 1;
                        Vector3Int probeRightL = exitPos; probeRightL.x += 9;
                        if (!floorMap.HasTile(probeRightL))
                        {
                            spawnCorridor(length, currPos, 1);
                            exitPos.x += length;
                        }
                        break;
                    case 3: //True down
                        currPos.y += 2;
                        Vector3Int probeDownL = exitPos; probeDownL.y -= 9;
                        if (!floorMap.HasTile(probeDownL))
                        {
                            spawnCorridor(length + 1, currPos, 2);
                            exitPos.y -= length;
                        }
                        break;

                    default: break;
                }
            }

            //direction stored in z component
            return exitPos;
    }

    private Vector3Int pathTurn(int dir, int length, Vector3Int pos) {

        Vector3Int probe = pos;
        Vector3Int probeHalf = pos;

        switch (dir) {
            case 0:
                probe.y += length;
                probeHalf.y += length / 2;
                break;        
            case 1:
                probe.x += length;
                probeHalf.x += length / 2;
                break;
            case 2:
                probe.y -= length;
                probeHalf.y -= length / 2;
                break;
            case 3:
                probe.x -= length;
                probeHalf.x -= length / 2;
                break;
        }

        //if (!floorMap.HasTile(probe) && !floorMap.HasTile(probeHalf)) {
            spawnCorridor(length, pos, dir);
            probe.z = dir;
            return probe;
        /*} 
        else
        {
            return pos;
        }*/
            
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

    //dir --> 0 = up, 1 = right, 2 = down, 3 = left
    private void spawnCorridor(int length, Vector3Int entrancePos, int dir) {
        int xOffset = 0;
        int yOffset = 0;

        if (dir == 0 || dir == 2)
        {
            if (dir == 2)
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
