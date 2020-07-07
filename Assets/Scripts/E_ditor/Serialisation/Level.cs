using System.Xml.Serialization;

[XmlRoot]
public class Level {
    [XmlElement]
    public string name;
    [XmlArray("prefabs"), XmlArrayItem("prefab")]
    public TransformXml[] transforms;

    public Level() { }
    public Level(string name, TransformXml[] transforms) {
        this.name = name;
        this.transforms = transforms;
    }
}
