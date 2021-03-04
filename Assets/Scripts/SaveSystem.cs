using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem 
{
   public static void SaveScore(int highScore)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/FlappyBirdSave.Lumpy"; //Cambiar Nombre para Score por Escenario
        var stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, highScore);
        stream.Close();
    }

    public static int LoadScore()
    {
        var path = Application.persistentDataPath + "/FlappyBirdSave.Lumpy"; //Cambiar Nombre para Score por Escenario

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var highscore = (int)formatter.Deserialize(stream);

            stream.Close();
            return highscore;
        }
        else
        {
            return 0;
        }

    }
}
