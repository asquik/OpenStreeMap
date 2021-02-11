using System.Xml;
using UnityEngine;

class OsmBounds:BaseOsm
{

    public float MinLat { get; private set; }
    public float MaxLat { get; private set; }
    public float MinLon { get; private set; }
    public float Maxlon { get; private set; }
    public Vector3 Center { get; private set; }
    public OsmBounds(XmlNode node )
    {
        MinLat = GetAttribute<float>("minlat", node.Attributes);
        MaxLat = GetAttribute<float>("maxlat", node.Attributes);
        MinLon = GetAttribute<float>("minlon", node.Attributes);
        Maxlon = GetAttribute<float>("maxlon", node.Attributes);

        float x = (float)((MercatorProjection.lonToX(Maxlon) + MercatorProjection.lonToX(MinLon)) / 2);
        float y = (float)((MercatorProjection.latToY(MaxLat) + MercatorProjection.latToY(MinLat)) / 2);

        Center = new Vector3(x, 0, y);
    }
}
