using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int gridSizeX, gridSizeY, gridSizeZ;
    public int tunnleWidthX, tunnleDepthZ, tunnleHightY;
    private Cell[,,] grid;
    public Material[] cellMaterials;
    public Texture2D cellTexture; // Reference to your PNG texture
    public Material cellMaterial;
    public Material cellMateriall;

 
    // private void InitMaterials()
    // {
    //     //cellMaterials = new Material[10];
    //     for(int i = 0; i < 10 ; i++){
    //         //Renderer renderer = cube.GetComponent<Renderer>();
    //         // Find the Standard shader
    //         //Shader shader = Shader.Find("Standard");

    //         // Create a new material with the Standard shader
    //         //Material material = cellMaterial;

    //         // Set the color for the cubes with full transparency
    //         //Color color = new Color(1, 0, 0, 0.01f); // Fully transparent white color
    //         cellMaterials[i].color = new Color((i+1)/11, 0, 0, 0.1f);

    //         //dering mode to transparent
    //         cellMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //         cellMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //         cellMaterials[i].SetInt("_ZWrite", 0);
    //         cellMaterials[i].DisableKeyword("_ALPHATEST_ON");
    //         cellMaterials[i].EnableKeyword("_ALPHABLEND_ON");
    //         cellMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //         cellMaterials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

    //         // Apply the material to the renderer
    //         //renderer.material = material;
    //     }
    // }
    private void InitializeGrid()
    {
        gridSizeX = 20;
        gridSizeY = 20;
        gridSizeZ = 80;

        tunnleWidthX = 20;
        tunnleHightY = 20;
        tunnleDepthZ = 80;

        // Initialize each cell in the grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    grid[x, y, z] = new Cell();
                    // Set initial properties of each cell
                    grid[x, y, z].density = 0f;
                    grid[x, y, z].velocity = Vector3.zero;
                    // grid[x, y, z].color = new Color(1, 0, 0, 0.03f);
                    // cellMateriall.color = grid[x, y, z].color;
                    // grid[x, y, z].material = cellMaterial;

                    if((x+y) %2 == 0){
                        grid[x, y, z].color = new Color(1, 0, 0, 0.05f);
                        cellMaterial.color = grid[x, y, z].color;
                        grid[x, y, z].material = cellMaterial;
                    }
                    else{
                        grid[x, y, z].color = new Color(0, 0, 1, 0.05f);
                        cellMateriall.color = grid[x, y, z].color;
                        grid[x, y, z].material = cellMateriall;
                    }
                    //grid[x, y, z].color = new Color((float)z/10, 0, (float)(10 - z)/10, 0.01f);
                   // Debug.Log(x + "HELLO" + y + "HELLO" + z);
                }
            }
        }

        int endZ = gridSizeZ/8;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < 80; z++)
                {
                    if(z < 10) grid[x, y, z].color = new Color(1f, 0f, 1f, 0.01f);
                    else grid[x, y, z].color = new Color(1f, 0f, 1f, 0.01f);
                    // grid[x, y, z].density = 8888;
                    //Debug.Log(i + "HELLO" + j + "HELLO" + k);
                }
            }
        }
    }
    private void VisualizeGrid()
    {
        float cubeX = tunnleWidthX / gridSizeX;
        float cubeY = tunnleHightY / gridSizeY;
        float cubeZ = tunnleDepthZ / gridSizeZ;

        float startX = -tunnleWidthX/2 + cubeX/2;
        float startY = cubeY/2;
        float startZ = -tunnleDepthZ/2 + cubeZ/2;

        float endX = tunnleWidthX/2;
        float endY = tunnleHightY;
        float endZ = tunnleDepthZ/2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    if(x == 0 && y == 0 && z == 0) Debug.Log(grid[x, y, z].density);

                    float posX = (x * cubeX) - tunnleWidthX/2 + cubeX/2;
                    float posY = (y * cubeY) + cubeY/2;
                    float posZ = (z * cubeZ) - tunnleDepthZ/2 + cubeZ/2;

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(posX, posY, posZ);
                    
                    // cube.transform.scale = new Vector3(cubeX, cubeY, cubeZ);

                    // Apply texture to cube
                    Renderer renderer = cube.GetComponent<Renderer>();
                    renderer.material = grid[x, y, z].material;
                }
            }
        }
    }

    private void SetPrimitiveColors()
    {
        int endZ = gridSizeZ/8;
        for(int i = 0; i < gridSizeX; i++){
            for(int j = 0; j < gridSizeY; j++){
                for(int k = 0; k < endZ; k++){
                    grid[i, j, k].color = new Color((float)j/endZ, 0, (float)(endZ - j)/endZ, 0.01f);
                }
            }
        }
    }

    void Start()
    {
        grid = new Cell[100, 100, 100];
        cellMaterials = new Material[1];

        InitializeGrid();
        //SetPrimitiveColors();
        VisualizeGrid();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //VisualizeGrid();
            // Debug.Log("printed");
            // Vector3 v = new Vector3(1, 1, 1);
            // transform.position += v;
        }
    }
}
