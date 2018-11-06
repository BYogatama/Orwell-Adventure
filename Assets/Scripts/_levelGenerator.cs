using UnityEngine;

[System.Serializable]
public class ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}

public class _levelGenerator : MonoBehaviour {

    public Texture2D LevelMap;
    public ColorToPrefab[] colorMaps;

	// Use this for initialization
	void Start () {
        GenerateLevel();
	}

    void ClearMap()
    {
        while (transform.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
    }

    void GenerateLevel()
    {
        ClearMap();

        Color32[] allPixels = LevelMap.GetPixels32();
        int width = LevelMap.width;
        int height = LevelMap.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GenerateTile(allPixels[(y * width) + x], x, y);
            }
        }
    }

    void GenerateTile(Color32 c, int x, int y)
    {
        Color pixelColor = LevelMap.GetPixel(x, y);
        
        if (pixelColor.a <= 0)
        {
            //Jika Pixel Transparan
            return;
        }

        foreach (ColorToPrefab cTP in colorMaps) 
        {
            if (cTP.color.Equals(c))
            {
                Vector3 position = new Vector3(x-8.38f,y-4.5f,0);
                Instantiate(cTP.prefab,position,Quaternion.identity,transform);
            }
        }
    }
    
}
