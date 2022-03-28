using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem 
{
    public static void Save(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();


       
        //saveData.path = Application.persistentDataPath + "/save.fun";

        //MemoryStream ms = new MemoryStream();
        //using (BsonWriter writer = new BsonWriter(ms))
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.Serialize(writer, saveData);
        //}
        //string data = Convert.ToBase64String(ms.ToArray());

        //using (StreamWriter file = File.CreateText(newPath))
        //{
        //    file.WriteLine(data);
        //}

        //using (StreamWriter file = File.AppendText(PathSaveFileList))
        //{
        //    file.WriteLine(newPath);
        //}
    }

    public static void Load()
    {
        //string path = Application.persistentDataPath + "/player.fun";
        //if (File.Exists(path))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);
        //    if (stream.Length == 0)
        //    {
        //        stream.Close();
        //        return null;
        //    }

        //    PlayerData data = formatter.Deserialize(stream) as PlayerData;
        //    stream.Close();

        //    return data;
        //}
        //else
        //{
        //    //Debug.LogError("Save file not found in " + path);
        //    return null;
        //}
    }


    public static void SavePlayer(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0)
            {
                stream.Close();
                return null;
            }

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else 
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void NewStartPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            File.Delete(path);
            //UnityEditor.AssetDatabase.Refresh();
            //UnityEditor.FileUtil.DeleteFileOrDirectory(path);
            //UnityEditor.AssetDatabase.Refresh();

            //FileStream stream = new FileStream(path, FileMode.Create);
            //stream.Close();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
