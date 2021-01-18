using Assets.Scripts;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    int xSize = 100;
    int ySize = 100;
    private readonly NoiseGenerator noiseGenerator;

    [SerializeField]
    MeshFilter meshFilter;

    [SerializeField]
    MeshRenderer meshRenderer;

    public MapGenerator()
    {
        noiseGenerator = new NoiseGenerator();
        meshRenderer = new MeshRenderer();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap()
    {
        Vector3[] meshVertices = meshFilter.mesh.vertices;
        //я тут не совсем понял почему так
        var tileDepth = Mathf.Sqrt(meshVertices.Length);
        var tileWidth = tileDepth;
        var heightMap = noiseGenerator.GetNoise((int)tileDepth, (int)tileWidth, 1);
        meshRenderer.material.mainTexture = CreateTexture(heightMap);
    }

    private Texture2D CreateTexture(float [,] heightMap){
        var depth = heightMap.GetLength(0);
        var width = heightMap.GetLength(1);

        Color[] colorMap = new Color[depth* width];
        for (int zIndex = 0; zIndex < depth; zIndex++)
        {
            for (int xIndex = 0; xIndex < width; xIndex++)
            {
                var colorIndex = zIndex * width + depth;
                var hight = heightMap[zIndex, xIndex];
                colorMap[xIndex] = Color.Lerp(Color.black, Color.white, colorIndex);
            }
        }

        var texture = new Texture2D(depth, depth);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();

        return texture;
    }
}
