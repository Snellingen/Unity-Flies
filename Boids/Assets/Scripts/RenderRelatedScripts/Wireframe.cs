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

    private Vector3[] lines;
    private List<Vector3> linesArray;
    private Material lineMaterial;
    private MeshRenderer meshRenderer; 

	void Start ()
	{
	    renderer.enabled = false;
	    meshRenderer = (MeshRenderer)GetComponent(typeof(MeshRenderer));
	    if (!meshRenderer)
	    {
            Debug.LogWarning("Warning: Found no MeshRenderer! Will try to create a new MeshRenderer.");
	        meshRenderer = gameObject.AddComponent<MeshRenderer>();
	    }
	    meshRenderer.material = new Material("Shader \"Lines/Background\" { Properties { _Color (\"Main Color\", Color) = (1,1,1,1) } SubShader { Pass {" + 
            (ZWrite ? " ZWrite on " : " ZWrite off ") + (Blend ? " Blend SrcAlpha OneMinusSrcAlpha" : " ") +
            (AWrite ? " Colormask RGBA " : " ") + "Lighting Off Offset 1, 1 Color[_Color] }}}");

        lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha BindChannels { Bind \"Color\",color } ZWrite On Cull Front Fog { Mode Off } } } }")
        {
            hideFlags = HideFlags.HideAndDontSave,
            shader = {hideFlags = HideFlags.HideAndDontSave}
        };

	    linesArray = new List<Vector3>();
	    MeshFilter filter = GetComponent<MeshFilter>();
	    Mesh mesh = filter.sharedMesh;
	    Vector3[] vertices = mesh.vertices;
	    int[] triangles = mesh.triangles;

	    for (int i = 0; i < triangles.Length/ 3; i++)
	    {
            linesArray.Add(vertices[triangles[i * 3]]);
            linesArray.Add(vertices[triangles[i * 3 + 1]]);
            linesArray.Add(vertices[triangles[i * 3 + 2]]);
	    }
	    lines = linesArray.ToArray();
	}

    void OnRenderObject()
    {
        meshRenderer.sharedMaterial.color = BackgroundColor;
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        GL.MultMatrix((transform.localToWorldMatrix));
        GL.Begin(GL.LINES);
        GL.Color(LineColor);

        for (int i = 0; i < lines.Length/3; i++)
        {
            GL.Vertex(lines[i * 3]);
            GL.Vertex(lines[i * 3 + 1]);

            GL.Vertex(lines[i * 3 + 1]);
            GL.Vertex(lines[i * 3 + 2]);

            GL.Vertex(lines[i * 3 + 2]);
            GL.Vertex(lines[i * 3]);
        }
        GL.End();
        GL.PopMatrix();
    }
}
