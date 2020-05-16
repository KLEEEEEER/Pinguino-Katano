using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler
{
    private readonly Map[] maps;
    private readonly int numberOfPoints;

    private int currentRound;
    private List<Map> remainingMaps;

    public MapHandler(Map[] mapSet, int numberOfPoints)
    {
        maps = mapSet;
        this.numberOfPoints = numberOfPoints;

        ResetMaps();
    }

    public bool IsComplete
    {
        get => true;
    }

    public string NextMap
    {
        get
        {
            if (IsComplete) { return null; }

            currentRound++;

            if (remainingMaps.Count == 0) { ResetMaps(); }

            Map map = remainingMaps[Random.Range(0, remainingMaps.Count)];

            remainingMaps.Remove(map);

            return map.name;
        }
    }

    private void ResetMaps() => remainingMaps = new List<Map>(maps);
}
