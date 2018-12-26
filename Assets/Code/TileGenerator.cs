using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour {



    public Tilemap ground;
    public Tilemap platforms;
    public Tilemap walls;
    public Tilemap background;


    public TileBase tileGround;
    public TileBase tilePlatforms;
    public TileBase tileWalls;
    public TileBase tileBackground;

    public Vector2Int size;

    void Start()
    {
        /*
        setGround(10);
        setBackground();
        setWalls();
        setPlatforms();
        */
    }

    void setGround(int depth)
    {

        ground.BoxFill(new Vector3Int(0, -depth, 0), tileGround, 0, -depth, size.x, 0);

        /*
    
        Vector3Int[] positions = new Vector3Int[size.x * depth];
        TileBase[] tileArray = new TileBase[positions.Length];
        int currentDepth = 1;

        for (int index = 0; index < positions.Length; index++)
        {
            if (index % size.x == 0)
            {
                currentDepth--;
            }
            positions[index] = new Vector3Int(index % size.x, currentDepth, 0);
            tileArray[index] = tileGround;

        }

        ground.SetTiles(positions, tileArray);
        */

    }

    void setBackground()
    {
        /*
        Vector3Int[] positions = new Vector3Int[size.x * size.y];
        TileBase[] tileArray = new TileBase[positions.Length];

        int index = 0;

        for (int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                positions[index] = new Vector3Int(i, j, 0);
                tileArray[index] = tileBackground;
                index++;
            }
        }

        background.SetTiles(positions, tileArray);
        */

        background.BoxFill(new Vector3Int(0, 0, 0), tileBackground, 0, 0, size.x, size.y);

    }

    void setWalls()
    {
        Vector3Int[] positions = new Vector3Int[size.x + 2*size.y + 1];
        TileBase[] tileArray = new TileBase[positions.Length];

        int index = 0;

        for (int j = 0; j < size.x + 1; j++)
        {
            positions[index] = new Vector3Int(j, size.y, 0);
            tileArray[index] = tileWalls;
            index++;
        }

        for (int i = 0; i < size.y; i++)
        {
            positions[index] = new Vector3Int(0, i, 0);
            tileArray[index] = tileWalls;
            index++;

            positions[index] = new Vector3Int(size.x, i, 0);
            tileArray[index] = tileWalls;
            index++;
        }

        walls.SetTiles(positions, tileArray);

    }


    void setPlatforms()
    {

        //Random.Range(min, max);

        int area = size.x * size.y;

        int numPlat = area / 100;

        int[] platWidths = new int[numPlat];

        int width = (int)Random.Range(3, 7);

        Vector3Int[] positions = new Vector3Int[width];
        TileBase[] tileArray = new TileBase[width];

        for(int count = 0; count < numPlat; numPlat++)
        {

        }

        //int widthIndex = 0;
        for (int i = 0; i < width; i++)
        {
            positions[i] = new Vector3Int(i + 1, 3, 0);
            tileArray[i] = tilePlatforms;
        }

        platforms.SetTiles(positions, tileArray);


    }

}
