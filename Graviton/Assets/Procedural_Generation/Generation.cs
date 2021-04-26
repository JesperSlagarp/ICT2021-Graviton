using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private Tile floorTile;

    [SerializeField]
    private Tilemap floorMap;

    private Vector3Int startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3Int(0,0,0);

        generate(startPos, 0, 10);
        
    }

    private void generate(Vector3Int currPos, int entranceSide, int limit) {
        if (limit < 1) return;

        int roomWidth = Random.Range(9, 19);
        if (roomWidth % 2 == 0) roomWidth--;
        int roomHeight = Random.Range(9, 19);
        if (roomHeight % 2 == 0) roomHeight--;

        spawnRoom(roomWidth, roomHeight, currPos, entranceSide);
        if (limit < 3) return;

        bool goDown = (Random.value > 0.5f), goRight = (Random.value > 0.5f), goUp = (Random.value > 0.5f), goLeft = (Random.value > 0.5f);

        switch (entranceSide) {
            // Sets Disables origin path and centralizes position in room
            case 0: goDown  = false; currPos.y += roomHeight / 2; break;
            case 1: goRight = false; currPos.x -= roomWidth  / 2; break;
            case 2: goUp    = false; currPos.y -= roomHeight / 2; break;
            case 3: goLeft  = false; currPos.x += roomWidth  / 2; break;
        }

        if (goDown)
        {
            Vector3Int down = path(currPos, 2, roomWidth, roomHeight);
            generate(down, 2, limit - 1);
        }
        if (goRight)
        {
            Vector3Int right = path(currPos, 1, roomWidth, roomHeight);
            generate(right, 3, limit - 1);
        }
        if (goUp)
        {
            Vector3Int up = path(currPos, 0, roomWidth, roomHeight);
            generate(up, 0, limit - 1);
        }
        if (goLeft)
        {
            Vector3Int left = path(currPos, 3, roomWidth, roomHeight);
            generate(left, 1, limit - 1);
        }

        /*int length = 0;
        if (goDown) {
            Vector3Int downEntrancePos = currPos;
            downEntrancePos.y -= roomHeight / 2;
            length = Random.Range(5, 10);
            spawnCorridor(length, downEntrancePos, 2);
            downEntrancePos.y -= length;
            generate(downEntrancePos, 2, --limit);
        }
        if (goRight)
        {
            Vector3Int rightEntrancePos = currPos;
            rightEntrancePos.x += roomWidth / 2;
            length = Random.Range(5, 10);
            spawnCorridor(length, rightEntrancePos, 1);
            rightEntrancePos.x += length;
            generate(rightEntrancePos, 3, --limit);
        }
        if (goUp)
        {
            Vector3Int upEntrancePos = currPos;
            upEntrancePos.y += roomHeight / 2;
            length = Random.Range(5, 10);
            spawnCorridor(length, upEntrancePos, 0);
            upEntrancePos.y += length;
            generate(upEntrancePos, 0, --limit);
        }
        if (goLeft)
        {
            Vector3Int leftEntrancePos = currPos;
            leftEntrancePos.x -= roomWidth / 2;
            length = Random.Range(5, 10);
            spawnCorridor(length, leftEntrancePos, 3);
            leftEntrancePos.x -= length;
            generate(leftEntrancePos, 1, --limit);
        }*/
    }

    //dir --> 0 = up, 1 = right, 2 = down, 3 = left
    private Vector3Int path(Vector3Int currPos, int dir, int roomWidth, int roomHeight) {
        Vector3Int exitPos;
        int length = Random.Range(5, 10);
        switch (dir) {
            case 0: currPos.y += roomHeight / 2; exitPos = currPos; exitPos.y += length; break; //Up
            case 1: currPos.x += roomWidth  / 2; exitPos = currPos; exitPos.x += length; break; //Right
            case 2: currPos.y -= roomHeight / 2; exitPos = currPos; exitPos.y -= length; break; //Down
            case 3: currPos.x -= roomWidth  / 2; exitPos = currPos; exitPos.x -= length; break; //Left
            default:                             exitPos = currPos; break;
        }
        spawnCorridor(length, currPos, dir);

        return exitPos;
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
