using UnityEngine;

public class TerrainFace
{
    private readonly Mesh _mesh;
    private readonly int _resolution;
    private readonly Vector3 _localUp;
    private readonly Vector3 _axisA;
    private readonly Vector3 _axisB;
    private readonly ShapeGenerator _generator;

    public TerrainFace(ShapeGenerator generator, Mesh mesh, int resolution, Vector3 localUp)
    {
        _generator = generator;
        _mesh = mesh;
        _resolution = resolution;
        _localUp = localUp;
        _axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        _axisB = Vector3.Cross(localUp, _axisA);
    }

    public void ConstructMesh()
    {
        var vertices = new Vector3[_resolution * _resolution];
        var triangles = new int[(_resolution - 1) * (_resolution - 1) * 6];
        var triIndex = 0;
        //Preparation for advanced terrain coloring
        var uv = (_mesh.uv.Length == vertices.Length) ? _mesh.uv : new Vector2[vertices.Length];

        for (var y = 0; y < _resolution; y++)
        {
            for (var x = 0; x < _resolution; x++)
            {
                var i = x + y * _resolution;
                var percent = new Vector2(x, y) / (_resolution - 1);
                var pointOnUnitCube = _localUp + (percent.x - .5f) * 2 * _axisA + (percent.y - .5f) * 2 * _axisB;
                var pointOnUnitSphere = pointOnUnitCube.normalized;
                var unscaledElevation = _generator.CalculateUnscaledElevation(pointOnUnitSphere);
                vertices[i] = pointOnUnitSphere * _generator.GetScaledElevation(unscaledElevation);
                //Preparation for advanced terrain coloring
                uv[i].y = unscaledElevation;
                if (x == _resolution - 1 || y == _resolution - 1)
                    continue;

                triangles[triIndex] = i;
                triangles[triIndex + 1] = i + _resolution + 1;
                triangles[triIndex + 2] = i + _resolution;

                triangles[triIndex + 3] = i;
                triangles[triIndex + 4] = i + 1;
                triangles[triIndex + 5] = i + _resolution + 1;
                triIndex += 6;
            }
        }

        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
        _mesh.uv = uv;
    }
}