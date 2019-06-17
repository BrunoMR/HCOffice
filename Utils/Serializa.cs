using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Utils
{
    public static class Serializa
    {
        public static T Deserializar<T>(this XElement xElement)
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xElement.ToString())))
            {
                var xmlSerializar = new XmlSerializer(typeof(T));
                return (T)xmlSerializar.Deserialize(memoryStream);
            }
        }

        public static XElement Serializar<T>(this object o)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializar = new XmlSerializer(typeof(T));
                    xmlSerializar.Serialize(streamWriter, o);
                    return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }
        }
    }
}
