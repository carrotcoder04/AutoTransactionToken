// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Xml.Serialization;

public class Content
{
    public string tokenId { get; set; }
    public string tokenName { get; set; }
    public string tokenAbbr { get; set; }
    public double amount { get; set; }
}

public class Data
{
    public List<Content> content { get; set; }
}

public class SmartAndUltima
{
    public string state { get; set; }
    public Data data { get; set; }
}

public class Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Assets
    {
    }

    public class Bandwidth
    {
        public double energyRemaining { get; set; }
        public double energyLimit { get; set; }
    }

    public class Data
    {
        public Bandwidth bandwidth { get; set; }
    }

    public class Energy
    {
        public string state { get; set; }
        public Data data { get; set; }
    }
}


// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Hierarchy));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Hierarchy)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName = "node")]
public class Nodes
{

    [XmlAttribute(AttributeName = "index")]
    public int Index { get; set; }

    [XmlAttribute(AttributeName = "text")]
    public string Text { get; set; }

    [XmlAttribute(AttributeName = "resource-id")]
    public string ResourceId { get; set; }

    [XmlAttribute(AttributeName = "class")]
    public string Class { get; set; }

    [XmlAttribute(AttributeName = "package")]
    public string Package { get; set; }

    [XmlAttribute(AttributeName = "content-desc")]
    public string ContentDesc { get; set; }

    [XmlAttribute(AttributeName = "checkable")]
    public bool Checkable { get; set; }

    [XmlAttribute(AttributeName = "checked")]
    public bool Checked { get; set; }

    [XmlAttribute(AttributeName = "clickable")]
    public bool Clickable { get; set; }

    [XmlAttribute(AttributeName = "enabled")]
    public bool Enabled { get; set; }

    [XmlAttribute(AttributeName = "focusable")]
    public bool Focusable { get; set; }

    [XmlAttribute(AttributeName = "focused")]
    public bool Focused { get; set; }

    [XmlAttribute(AttributeName = "scrollable")]
    public bool Scrollable { get; set; }

    [XmlAttribute(AttributeName = "long-clickable")]
    public bool LongClickable { get; set; }

    [XmlAttribute(AttributeName = "password")]
    public bool Password { get; set; }

    [XmlAttribute(AttributeName = "selected")]
    public bool Selected { get; set; }

    [XmlAttribute(AttributeName = "bounds")]
    public string Bounds { get; set; }

    [XmlElement(ElementName = "node")]
    public List<Nodes> Node { get; set; }
}

[XmlRoot(ElementName = "hierarchy")]
public class Hierarchy
{

    [XmlElement(ElementName = "node")]
    public Nodes Node { get; set; }

    [XmlAttribute(AttributeName = "rotation")]
    public int Rotation { get; set; }
}

