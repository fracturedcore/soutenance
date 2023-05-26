using UnityEngine;

public class ConvertDataBaseToMultiPlatform : MonoBehaviour
{
    public string DataBaseName;

    public void Awake()
    {
        GenerateConnectionString(DataBaseName+".db");
        
    }
    public void GenerateConnectionString(string DatabaseName)
    {
#if UNITY_EDITOR
        string dbPath = Application.dataPath + "/StreamingAssets/" + DatabaseName;
        
#endif
        


    }



 
}