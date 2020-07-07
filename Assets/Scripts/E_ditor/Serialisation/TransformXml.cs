using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TransformXml {
    [XmlAttribute]
    public float x, y, z;
    [XmlAttribute]
    public float rx, ry, rz;

    [XmlElement("name")]
    public string prefabName;

    public TransformXml() { }
    public TransformXml(Transform t) {
        x = t.position.x;
        y = t.position.y;
        z = t.position.z;
        rx = t.rotation.eulerAngles.x;
        ry = t.rotation.eulerAngles.y;
        rz = t.rotation.eulerAngles.z;
        this.prefabName = t.name;
    }
}
