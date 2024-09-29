using UnityEngine;

public class PrefabLODGroupCreator : MonoBehaviour
{
    public GameObject[] lodPrefabs; // Assign your LOD0 to LOD3 prefabs here in the inspector
    public float[] lodPercentages = { 0.6f, 0.3f, 0.1f, 0.01f }; // Adjust these percentages as needed

    void Start()
    {
        CreateLODGroup();
    }

    void CreateLODGroup()
    {
        // Create a new empty GameObject to hold the LOD Group
        GameObject lodGroupObject = new GameObject("LOD Group");
        LODGroup lodGroup = lodGroupObject.AddComponent<LODGroup>();

        // Prepare the LOD array
        LOD[] lods = new LOD[lodPrefabs.Length];

        for (int i = 0; i < lodPrefabs.Length; i++)
        {
            // Instantiate the prefab as a child of the LOD Group object
            GameObject lodInstance = Instantiate(lodPrefabs[i], lodGroupObject.transform);

            // Get all renderers from the instantiated prefab
            Renderer[] renderers = lodInstance.GetComponentsInChildren<Renderer>();

            // Create a new LOD with the renderers and the corresponding percentage
            lods[i] = new LOD(lodPercentages[i], renderers);
        }

        // Set the LODs for the LOD Group
        lodGroup.SetLODs(lods);
        lodGroup.RecalculateBounds();
    }
}