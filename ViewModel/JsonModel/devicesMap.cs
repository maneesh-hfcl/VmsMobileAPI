using System.Xml.Serialization;

namespace Vms.JsonModel
{
    [XmlRoot("map")]
    public class JDeviceMap
    {
        [XmlElement("dev")]
        public List<string> ? devices{get; set;} 
    }

    public class JDevice
    {
        public string? dev{get ;set;}
    }

 }