using UnityEngine;

public class ColorGenerator
{
    private ColorSettings _settings;
    private Texture2D _texture;
    private const int TextureResolution = 50;

    public void UpdateSettings(ColorSettings settings)
    {
        _settings = settings;
        if (_texture == null)
        {
            _texture = new Texture2D(TextureResolution, 1);
        }
    }

    public void UpdateElevation(MinMax minMax)
    {
        _settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(minMax.Min, minMax.Max));
    }

    public void UpdateColors()
    {
        var colors = new Color[TextureResolution];
        for (var i = 0; i < TextureResolution; i++)
        {
            colors[i] = _settings.terrainColor.Evaluate(i / (TextureResolution - 1f));
        }

        _texture.SetPixels(colors);
        _texture.Apply();
        _settings.planetMaterial.SetTexture("_parentTexture", _texture);
    }
}