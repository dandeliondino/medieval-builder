using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("Hexmap")]
public class HexMap
{
    [XmlAttribute("xsize")]
    public int xsize;

    [XmlAttribute("zsize")]
    public int zsize;

    [XmlArray("hexes"), XmlArrayItem("hex")]
    public HexData[] hexes;

}

public class HexData
{
    [XmlAttribute("x")]
    public int x;

    [XmlAttribute("z")]
    public int z;

    [XmlAttribute("height")]
    public int height;

    [XmlElement("tileType")]
    public string tileType;
}
