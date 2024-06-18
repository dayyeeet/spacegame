using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlanetGenerator))]
public class PlanetGeneratorEditor : Editor
{
    private PlanetGenerator _generator;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Planets"))
        {
            _generator.GeneratePlanets();
        }
        
        if (GUILayout.Button("Reseed"))
        {
            _generator.ReseedGenerator();
        }
        
        if (GUILayout.Button("Reset"))
        {
            _generator.ResetEverything();
        }
    }
    private void OnEnable()
    {
        _generator = (PlanetGenerator)target;
    }
}