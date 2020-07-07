using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Serializer : MonoBehaviour{
    [SerializeField] Transform parent;
    List<TransformXml> pref = new List<TransformXml>();



    public void  Serialize(string path) {
        //Get all the prefabs to save.
        foreach (Transform child in parent) {
            Debug.Log(child.name);
            pref.Add(new TransformXml(child));
        }


        Level l = new Level("Level", pref.ToArray());
        
        // Write to xml 
        XmlSerializer serializer = new XmlSerializer(typeof(Level));
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, l);
        writer.Close();
    }
    public static T Deserialize<T>(string path) {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StreamReader reader = new StreamReader(path);
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return deserialized;
    }
}
