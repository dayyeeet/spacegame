using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)] public int resolution = 10;

    [Range(6.5f, 11f)] public float gravityConstant = 9.8f;

    public bool autoUpdate = true;
    public string planetName;
    public int seed;
    public bool featuresGenerated = false;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    public FeatureSettings featureSettings;

    private readonly ShapeGenerator _shapeGenerator = new ShapeGenerator();
    private readonly ColorGenerator _colorGenerator = new ColorGenerator();
    private readonly FeatureGenerator _featureGenerator = new FeatureGenerator();

    [SerializeField, HideInInspector] private MeshFilter[] meshFilters;
    public GameObject gravityObject { get; private set; }
    private TerrainFace[] _terrainFaces;

    private void OnValidate()
    {
        GeneratePlanet();
    }

    public void Populate()
    {
        _featureGenerator.UpdateSeed(seed);
        _featureGenerator.UpdateSettings(featureSettings);
        foreach(var face in _terrainFaces)
            _featureGenerator.Populate(this, face);
        featuresGenerated = true;
    }

    public void ShowFeatures()
    {
        _featureGenerator.Show(this);
    }
    
    public void HideFeatures()
    {
        _featureGenerator.Hide(this);
    }

    void Initialize()
    {
        _shapeGenerator.UpdateSettings(shapeSettings);
        _colorGenerator.UpdateSettings(colorSettings);
        _featureGenerator.UpdateSettings(featureSettings);

        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];
        _terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        var gravity = GetObjectByName("gravity");
        if (gravity == null)
        {
            gravity = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gravity.name = "gravity";
        }
        gravityObject = gravity;
        DestroyImmediate(gravity.GetComponent<MeshRenderer>());
        gravity.transform.localScale = new Vector3(shapeSettings.planetRadius * 2, shapeSettings.planetRadius * 2,
            shapeSettings.planetRadius * 2);
        gravity.transform.parent = transform;
        gravity.transform.position = transform.position;
        for (var i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                var obj = new GameObject("mesh");
                obj.transform.parent = transform;
                obj.transform.position = transform.position;
                var mesh = new Mesh();
                obj.AddComponent<MeshRenderer>();
                meshFilters[i] = obj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = mesh;
                var newCollider = obj.AddComponent<MeshCollider>();
                newCollider.sharedMesh = mesh;
            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
            _terrainFaces[i] = new TerrainFace(_shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    public GameObject GetObjectByName(string objectName)
    {
        return transform.Find(objectName)?.gameObject;
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

        foreach (var obj in FindObjectsByType<GameObject>(FindObjectsSortMode.InstanceID))
        {
            if (obj.name == "mesh")
            {
                var collider = obj.GetComponent<MeshCollider>();
                collider.sharedMesh.RecalculateBounds();
                collider.convex = false;
                collider.enabled = true;
            }
        }
        _colorGenerator.UpdateElevation(_shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {
        _colorGenerator.UpdateColors();
    }
}