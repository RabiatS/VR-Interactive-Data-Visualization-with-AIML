using UnityEngine;
using System.Collections.Generic;

public class DataLoader : MonoBehaviour
{
    public GameObject dataPrefab; // Assign your interactable prefab (grabbable sphere) here
    public TextAsset csvFile;
    public GameObject graphContainer; // Drag your “GraphContainer” object here in Inspector
    public float scale = 1f;

    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        if (csvFile == null || graphContainer == null || dataPrefab == null)
        {
            Debug.LogWarning("Missing reference(s) on DataLoader!");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || !char.IsDigit(line.Trim()[0]) && line.Trim()[0] != '-') // skip header or empty
                continue;

            string[] values = line.Split(',');

            if (values.Length >= 3 && float.TryParse(values[0], out float x)
                                  && float.TryParse(values[1], out float y)
                                  && float.TryParse(values[2], out float z))
            {
                Vector3 pos = new Vector3(x, y, z) * scale;
                var pointObj = Instantiate(dataPrefab, pos, Quaternion.identity, graphContainer.transform);

                // OPTIONAL: Assign name/index for debugging
                pointObj.name = $"DataPoint ({x}, {y}, {z})";
            }
        }
    }
}
