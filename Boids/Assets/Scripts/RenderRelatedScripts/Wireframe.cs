using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Wireframe : MonoBehaviour
{

    public Color LineColor;
    public Color BackgroundColor;
    public bool ZWrite = true;
    public bool AWrite = true;
    public bool Blend = true;

    private Vector3[] _lines;
    private List<Vector3> _linesArray;
    private Material _lineMaterial;
    private MeshRenderer _meshRenderer; 

	void Start ()
	{
	    renderer.enabled = false;
	    _meshRenderer = (MeshRenderer)GetComponent(typeof(MeshRenderer));
	    if (!_meshRenderer)
	    {
            Debug.LogWarning("Warning: Found no MeshRenderer! Will try to create a new MeshRenderer.");
	        _meshRenderer = gameObject.AddComponent<MeshRenderer>();
	    }
	    _meshRenderer.material = new Material("Shader \"Lines/Background\" { Properties { _Color (\"Main Color\", Color) = (1,1,1,1) } SubShader { Pass {" + 
            (ZWrite ? " ZWrite on " : " ZWrite off ") + (Blend ? " Blend SrcAlpha OneMinusSrcAlpha" : " ") +
            (AWrite ? " Colormask RGBA " : " ") + "Lighting Off Offset 1, 1 Color[_Color] }}}");

        _lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha BindChannels { Bind \"Color\",color } ZWrite On Cull Front Fog { Mode Off } } } }")
        {
            hideFlags = HideFlags.HideAndDontSave,
            shader = {hideFlags = HideFlags.HideAndDontSave}
        };

	    _linesArray = new List<Vector3>();
	    var filter = GetComponent<MeshFilter>();
	    var mesh = filter.sharedMesh;
	    var vertices = mesh.vertices;
	    var triangles = mesh.triangles;

	    for (int i = 0; i < triangles.Length/ 3; i++)
	    {
            _linesArray.Add(vertices[triangles[i * 3]]);
            _linesArray.Add(vertices[triangles[i * 3 + 1]]);
            _linesArray.Add(vertices[triangles[i * 3 + 2]]);
	    }
	    _lines = _linesArray.ToArray();
	}

    void OnRenderObject()
    {
        _meshRenderer.sharedMaterial.color = BackgroundColor;
        _lineMaterial.SetPass(0);

        GL.PushMatrix();
        GL.MultMatrix((transform.localToWorldMatrix));
        GL.Begin(GL.LINES);
        GL.Color(LineColor);

        for (int i = 0; i < _lines.Length/3; i++)
        {
            GL.Vertex(_lines[i * 3]);
            GL.Vertex(_lines[i * 3 + 1]);

            GL.Vertex(_lines[i * 3 + 1]);
            GL.Vertex(_lines[i * 3 + 2]);

            GL.Vertex(_lines[i * 3 + 2]);
            GL.Vertex(_lines[i * 3]);
        }
        GL.End();
        GL.PopMatrix();
    }
}
