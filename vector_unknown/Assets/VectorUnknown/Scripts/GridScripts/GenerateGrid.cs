using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject wood_outline;
    [SerializeField]
    private GameObject dirt_tile;
    [SerializeField]
    private GameObject grass_tile;
    [SerializeField]
    private GameObject rock;
    [SerializeField]
    private GameObject spotted_grass;

    public int rows = 20;
    public int columns;

    public int tileSize = 1;
    public float offset = 0.5f;

    public int quadrantSize = 10;

    public float gridHeight = 0.1f;

    public char[,] tileMapped;
    public GameObject[,] tileGrid;

    public GameObject tile;

    public Transform gridHolder;

    public TextAsset textInput;

    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        columns = rows;
        offset = tileSize / 2;
        //quadrantSize = rows / 2;


        wood_outline = Resources.Load("Cubix/Optimized/Prefabs/Buildings/Floors/CB_Floor_Wood_01_Side_B_Optimized", typeof(GameObject)) as GameObject;
        dirt_tile = Resources.Load("Cubix/Optimized/Prefabs/quad_squad/CB_Ground_Rock_01_A_Optimized", typeof(GameObject)) as GameObject;
		grass_tile = Resources.Load("Cubix/Optimized/Prefabs/quad_squad/CB_Ground_Grass_01_E_Optimized", typeof(GameObject)) as GameObject;
		spotted_grass = Resources.Load("Cubix/Optimized/Prefabs/quad_squad/CB_Ground_Grass_01_C_Optimized", typeof(GameObject)) as GameObject;
		rock = Resources.Load("Cubix/Optimized/Prefabs/quad_squad/CB_Wall_Pattern_02_White_Optimized", typeof(GameObject)) as GameObject;
        
        tileMapped = new char[rows, columns];
        ReadTextLevels();
        InitializeGrid();

        //mainCamera.transform.position = new Vector3(rows/2,rows,columns/2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeGrid()
    {

        offset = (float)tileSize;
        offset = offset / 2.0f;

        GameObject select = wood_outline;
        for (int i = 0; i < rows; i = i + 1)
        {
            for (int jay = 0; jay < columns; jay = jay + 1)
            {
                int choice = choose_color(tileMapped[i, jay]);
                if (choice == 0)
                    select = wood_outline;
                if (choice == 1)
                    select = dirt_tile;
                if (choice == 2)
                    select = grass_tile;
                if (choice == 3)
                    select = rock;
                if (choice == 4)
                    select = spotted_grass;

                float float_i = (float)i;
                float float_jay = (float)jay;
                GameObject block = Instantiate(select,
                    new Vector3(float_i - ((rows / 2) - 1.0f) - (offset),
                        0.0f, float_jay - ((columns / 2) - 1.0f) - (offset)),
                    Quaternion.identity);

				block.transform.SetParent (this.transform);
            }
        }

    }

    int choose_color(char tileData)
    {
        // setting tile colors
        if (tileData == '0')// tileData.baseColor = Color.black;
            return 0;
        else if (tileData == 't')// tileData.baseColor = Color.gray;
            return 1;
        else if (tileData == 'x')// tileData.baseColor = Color.gray;
            return 3;
        else if (tileData == 'd')
            return 4;

        return 2;//tileData.baseColor = Color.green;
    }

    void ReadTextLevels()
    {
        string text = textInput.text;

        //Debug.Log(text);
        // remove newlines and returns
        //row = text[0];
        for (int i = text.Length - 1; i >= 0; i--)
        {
            if (text[i] == '\n' || text[i] == '\r' || text[i] == ' ')
            {
                string newText = text.Remove(i, 1);
                text = newText;
            }

        }

        // convert text into 2D array
        // must be correct size

        int index = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                tileMapped[i, j] = text[index];
                index++;
            }
        }
			
    }
}