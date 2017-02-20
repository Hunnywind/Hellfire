using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoadManager : MonoBehaviour
{

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
//        data.gunheeHoneyPot = gunheeHoneyPot;
//        data.gunheeWeight = gunheeWeight;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

//            gunheeHoneyPot = data.gunheeHoneyPot;
//            gunheeWeight = data.gunheeWeight;
        }
        else
        {
            Debug.Log("No Save File");
        }
    }        
}


[Serializable] //can save this file
class PlayerData
{
    //data
//    public bool gunheeHoneyPot;
//    public float gunheeWeight;
}

