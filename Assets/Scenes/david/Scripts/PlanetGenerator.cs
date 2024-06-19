using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetGenerator : MonoBehaviour
{
    public int seed;
    public bool randomSeed = true;

    public float minCoordsDistance;
    public float coordsMultiplier = 1f;
    
    private PlanetNameGenerator _nameGenerator;

    public ColorSettings materialSettings;
    public GameObject spaceShipReference;
    
    public ShapeSettings[] shapePrefabs;
    public FeatureSettings[] featurePrefabs;
    private bool _isInitialSeed = false;
    
    private readonly Dictionary<Vector3, Planet> _generated = new();

    private void Start()
    {
        ReseedGenerator();
        ResetEverything();
        GeneratePlanets();
    }

    private void Update()
    {
        var position = spaceShipReference.transform.position;
        _generated.ToList()
            .FindAll(value => Vector3.Distance(position, value.Key) <= 120f + value.Value.shapeSettings.planetRadius && !value.Value.featuresGenerated)
            .ForEach(planet => planet.Value.Populate());
        foreach (var key in _generated)
        {
            if (Vector3.Distance(position, key.Key) <= 20f + key.Value.shapeSettings.planetRadius)
            {
                key.Value.ShowFeatures();
            }
            else
            {
                key.Value.HideFeatures();
            }
        }

        if (Vector3.Distance(position, Vector3.zero) > minCoordsDistance)
        {
            GeneratePlanets();
        }
    }

    private void Reseed(int? newSeedOrEmpty = null)
    {
        _isInitialSeed = true;
        seed = newSeedOrEmpty ?? DateTime.Now.GetHashCode();
    }

    public void ReseedGenerator()
    {
        Reseed(randomSeed ? null : seed);
    }

    public void ResetEverything()
    {
        
        minCoordsDistance = 400f * coordsMultiplier;
        foreach (var key in _generated)
        {
            if(key.Value != null)
                DestroyImmediate(key.Value.transform.gameObject);
        }
        _generated.Clear();
    }

    private void OnReseed()
    {
        _nameGenerator = new PlanetNameGenerator(seed);
    }
    

    //This can also be called at a later time (if player travels deeper into space)
    public void GeneratePlanets()
    {
        if (_isInitialSeed)
        {
            Random.InitState(seed);
            OnReseed();
            _isInitialSeed = false;
        }

        if (_nameGenerator == null) return;
        for (var i = 0; i < Random.Range(5, 10); i++)
        {
            var foundPlace = false;
            Vector3? coordinates = null;
            while (!foundPlace)
            {
                coordinates = GenerateRandomCoords();
                foundPlace = isGeneratableOn(coordinates.Value, _generated);
            }
            var planet = GeneratePlanet(coordinates.Value);
            _generated.Add(coordinates.Value, planet);
        }

        minCoordsDistance += 320f * coordsMultiplier;
    }

    private bool isGeneratableOn(Vector3 vector, Dictionary<Vector3, Planet> generated)
    {
        return !generated.Any(it => it.Key == vector || Vector3.Distance(vector, it.Key) <= it.Value.shapeSettings.planetRadius + (250f * coordsMultiplier));
    }

    private Vector3 GenerateRandomCoords()
    {
        var radius = minCoordsDistance + Random.Range(0, 40);
        var angle = Random.value * 2 * Math.PI;
        var randomX = radius * Math.Cos(angle);
        var randomZ = radius * Math.Sin(angle);
        var randomY = Random.Range(-150f, 150f);
        return new Vector3((float)randomX, randomY, (float) randomZ);
    }

    private Planet GeneratePlanet(Vector3 location)
    {
        var planet = new GameObject($"planet_{location.x}_{location.y}_{location.z}");
        planet.transform.position = location;
        var component = planet.AddComponent<Planet>();
        component.resolution = 110;
        component.seed = seed;
        component.planetName = _nameGenerator.GeneratePlanetName();
        component.shapeSettings = GenerateShapeSettings(location);
        component.featureSettings = GenerateFeatureSettings(location);
        component.colorSettings = GenerateColorSettings();
        component.transform.position = location;
        component.GeneratePlanet();
        return component;
    }

    private ShapeSettings GenerateShapeSettings(Vector3 randomness)
    {
        var randomShapeSettings = Instantiate(shapePrefabs[Random.Range(0, shapePrefabs.Length)]);
        randomShapeSettings.planetRadius = Random.Range(50f, 75f);
        foreach (var noiseSettings in randomShapeSettings.noiseLayers)
        {
            noiseSettings.centre = randomness;
        }

        return randomShapeSettings;
    }

    private FeatureSettings GenerateFeatureSettings(Vector3 randomness)
    {
        var randomFeatureSettings = Instantiate(featurePrefabs[Random.Range(0, featurePrefabs.Length)]);
        randomFeatureSettings.groundNoise.centre = randomness;
        randomFeatureSettings.treeNoise.centre = randomness;
        randomFeatureSettings.seed = seed;
        return randomFeatureSettings;
    }

    private ColorSettings GenerateColorSettings()
    {
        var settings = Instantiate(materialSettings);
        settings.planetMaterial = Instantiate(settings.planetMaterial);
        settings.terrainColor = GenerateGradient();
        return settings;
    }

    private static Gradient GenerateGradient()
    {
        var colorAmount = Random.Range(2, 5);
        var gradient = new Gradient();
        var keys = new GradientColorKey[colorAmount];
        var alpha = new GradientAlphaKey[colorAmount];
        for (var i = 0; i < colorAmount; i++)
        {
            keys[i].color = new Color(Random.value, Random.value, Random.value);
            keys[i].time = (float) i / colorAmount;
            alpha[i].alpha = 1f;
            alpha[i].time = (float)i / colorAmount;
        }
        gradient.SetKeys(keys, alpha);
        return gradient;
    }
}