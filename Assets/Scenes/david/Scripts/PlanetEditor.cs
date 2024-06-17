using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    private Planet _planet;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                _planet.GeneratePlanet();
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            _planet.GeneratePlanet();
        }

        if (GUILayout.Button("Populate Features"))
        {
            _planet.Populate();
        }

        DrawSettingsEditor(_planet.shapeSettings, _planet.OnShapeSettingsUpdated);
        DrawSettingsEditor(_planet.colorSettings, _planet.OnColorSettingsUpdated);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.InspectorTitlebar(true, settings);
            Editor editor = CreateEditor(settings);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                onSettingsUpdated?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        _planet = (Planet)target;
    }
}