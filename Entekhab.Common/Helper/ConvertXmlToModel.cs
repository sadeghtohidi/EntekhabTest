
using System;
using System.IO;
using System.Text;
//using System.Web.Script.Serialization; // Add reference: System.Web.Extensions
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Helpers
{
    public static class ConvertXmlToModel
    {



        public static T ParseXML<T>(this string @this) where T : class
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(@this.Trim());
            MemoryStream stream = new MemoryStream(byteArray);

            var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

    }
}