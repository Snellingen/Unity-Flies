using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public struct Tuple<T1, T2>
{
    public readonly T1 item1;
    public readonly T2 item2;
    public Tuple(T1 item1, T2 item2)
    {
        this.item1 = item1;
        this.item2 = item2;
    } 
}

public static class Tuple
{
    public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
        return new Tuple<T1, T2>(item1, item2);
    }
}
public class TerrainGenerator : MonoBehaviour
{
    public Dictionary<Tuple<int, int>, Terrain> TerrainDictionary;
    private Vector3 PlayerPosition;
    private Vector3 viewVector; 

	void Start () {
	    TerrainDictionary = new Dictionary<Tuple<int, int>, Terrain>();
	}

    void Update()
    {
        PlayerPosition = Camera.main.transform.position;
	    viewVector = Camera.main.transform.forward;
        GetTerrainUnder(PlayerPosition + viewVector*500f);
        GetTerrainUnder(PlayerPosition + (viewVector + Camera.main.transform.right) * m * 500f);
        GetTerrainUnder(PlayerPosition + (viewVector - Camera.main.transform.right) * m * 500f);

    }
}
