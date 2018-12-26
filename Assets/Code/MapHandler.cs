using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapHandler : MonoBehaviour {


    //Random rand = new Random();

    public int[,] Map;

    public int MapWidth;
    public int MapHeight;
    public int PercentAreWalls;

    public int PlatformChance;

    public int smoothingIterations;


    public Tilemap ground;
    public Tilemap platforms;
    public Tilemap walls;
    public Tilemap background;


    public TileBase tileGround;
    public TileBase tilePlatforms;
    public TileBase tileWalls;
    public TileBase tileBackground;

    // Use this for initialization
    void Start () {
        RandomFillMap();

        for (int i = 0; i < smoothingIterations; i++){
            MakeCaverns();
        }

        markGroundandPlatforms();
        placeTiles();
        //setBackground();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void markGroundandPlatforms(){

        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {

                if(Map[column, row] == 0 && Random.Range(0,100) < PlatformChance)
                {
                    int platformWidth = (int)Random.Range(5, 8);
                    int horizontalClearance = (int) Random.Range(2,5);
                    int verticalClearance = (int)Random.Range(3, 5);
                    bool canPlace = true;

                    if(column + platformWidth + horizontalClearance < MapWidth &&
                        column - horizontalClearance > 0 &&
                        row + verticalClearance < MapHeight && row - verticalClearance > 0)
                    {
                        for (int i = -horizontalClearance; i < platformWidth + horizontalClearance; i++)
                        {
                            for (int j = -verticalClearance; j < verticalClearance; j++)
                            {
                                if (Map[column + i, row + j] != 0)
                                {
                                    canPlace = false;
                                }
                            }

                        }

                        if (canPlace)
                        {
                            for (int i = 0; i < platformWidth; i++)
                            {
                                //print(new Vector3Int(column, row, verticalClearance));
                                Map[column + i, row] = 3;
                            }
                        }
                    }

                 
                }

                if(Map[column, row] == 1){

                    if(row + 1 < MapHeight){

                        if(Map[column, row + 1] == 0){
                            Map[column, row] = 2;
                        }
                    }

                }

            }
        }

    }

    void placeTiles(){

        /*
         * 0 = empty space (background)
         * 1 = wall
         * 2 = ground
         * 3 = platform
         */


        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                if(Map[column, row] == 1){
     
                    walls.SetTile(new Vector3Int(column, row, 0), tileGround);
                }

                else if(Map[column, row] == 2){
         
                    ground.SetTile(new Vector3Int(column, row, 0), tileGround);

                }

                else if(Map[column, row] == 0)
                {
 
                    background.SetTile(new Vector3Int(column, row, 0), tileBackground);
                }

                else if(Map[column, row] == 3)
                {

                    platforms.SetTile(new Vector3Int(column, row, 0), tilePlatforms);
                }
            }
        }

    }

    public void MakeCaverns()
    {
        // By initilizing column in the outter loop, its only created ONCE
        for (int column = 0, row = 0; row <= MapHeight - 1; row++)
        {
            for (column = 0; column <= MapWidth - 1; column++)
            {
                Map[column, row] = PlaceWallLogic(column, row);
            }
        }
    }

    public int PlaceWallLogic(int x, int y)
    {
        int numWalls = GetAdjacentWalls(x, y, 1, 1);


        if (Map[x, y] == 1)
        {
            if (numWalls >= 4)
            {
                return 1;
            }
            if (numWalls < 2)
            {
                return 0;
            }

        }
        else
        {
            if (numWalls >= 5)
            {
                return 1;
            }
        }
        return 0;
    }

    public int GetAdjacentWalls(int x, int y, int scopeX, int scopeY)
    {
        int startX = x - scopeX;
        int startY = y - scopeY;
        int endX = x + scopeX;
        int endY = y + scopeY;

        int iX = startX;
        int iY = startY;

        int wallCounter = 0;

        for (iY = startY; iY <= endY; iY++)
        {
            for (iX = startX; iX <= endX; iX++)
            {
                if (!(iX == x && iY == y))
                {
                    if (IsWall(iX, iY))
                    {
                        wallCounter += 1;
                    }
                }
            }
        }
        return wallCounter;
    }

    bool IsWall(int x, int y)
    {
        // Consider out-of-bound a wall
        if (IsOutOfBounds(x, y))
        {
            return true;
        }

        if (Map[x, y] == 1)
        {
            return true;
        }

        if (Map[x, y] == 0)
        {
            return false;
        }
        return false;
    }

    bool IsOutOfBounds(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return true;
        }
        else if (x > MapWidth - 1 || y > MapHeight - 1)
        {
            return true;
        }
        return false;
    }

    public void BlankMap()
    {
        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                Map[column, row] = 0;
            }
        }
    }

    public void RandomFillMap()
    {
        // New, empty map
        Map = new int[MapWidth, MapHeight];

        int mapMiddle = 0; // Temp variable
        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                // If coordinants lie on the the edge of the map (creates a border)
                if (column == 0)
                {
                    Map[column, row] = 1;
                }
                else if (row == 0)
                {
                    Map[column, row] = 1;
                }
                else if (column == MapWidth - 1)
                {
                    Map[column, row] = 1;
                }
                else if (row == MapHeight - 1)
                {
                    Map[column, row] = 1;
                }
                // Else, fill with a wall a random percent of the time
                else
                {
                    mapMiddle = (MapHeight / 2);

                    if (row == mapMiddle)
                    {
                        Map[column, row] = 0;
                    }
                    else
                    {
                        Map[column, row] = RandomPercent(PercentAreWalls);
                    }
                }
            }
        }
    }

    int RandomPercent(int percent)
    {
        if (percent >= (int) Random.Range(1, 101))
        {
            return 1;
        }
        return 0;
    }

}