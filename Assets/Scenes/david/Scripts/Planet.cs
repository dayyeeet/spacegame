using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;

    public bool autoUpdate = true;
    
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    private ShapeGenerator _shapeGenerator;
    
    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;
    private TerrainFace[] _terrainFaces;

    private void OnValidate()
    {
        GeneratePlanet();
    }

    void Initialize()
    {
        _shapeGenerator = new ShapeGenerator(shapeSettings);
        
        if(meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];
        _terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (var i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                var obj = new GameObject("mesh");
                obj.transform.parent = transform;
                obj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                meshFilters[i] = obj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            _terrainFaces[i] = new TerrainFace(_shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        if (!autoUpdate) return;
        Initialize();
        GenerateMesh();
    }

    public void OnColorSettingsUpdated()
    {
        if (!autoUpdate) return;
        Initialize();
        GenerateColors();
    }

    void GenerateMesh()
    {
        foreach (var face in _terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColors()
    {
        foreach (var filter in meshFilters)
        {
            filter.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.terrainColor;
        }
    }
}
