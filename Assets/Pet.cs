using UnityEngine;

public class PetDetails 
{
    public float Strength = 1f;
    public int Rarity = 1;
    public string Material;

    public PetDetails(float strength, int rarity, string material) 
    {
        Strength = strength;
        Rarity = rarity;
        Material = material;
    }
}

public class Pet : MonoBehaviour
{
    public PetDetails details;
    public Transform followPoint;

    private bool materialSet = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!materialSet) 
        {
            LoadMaterial();
        }

        transform.position = followPoint.position;
    }

    public void SetDetails(float strength, int rarity, string material) 
    {
        details.Material = material;
        details.Strength = strength;
        details.Rarity = rarity;
    }

    public void LoadMaterial() 
    {
        Material loadedMaterial = Resources.Load<Material>(details.Material);

        if (loadedMaterial != null)
        {
            Debug.Log($"Loaded material from Resources: {loadedMaterial.name}");

            Renderer renderer = GetComponent<Renderer>();

            renderer.material = loadedMaterial;
            materialSet = true;
        }
        else
        {
            Debug.LogError("Material not found in Resources.");
        }
    }
}
