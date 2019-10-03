using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Services.Ultility
{
    public static class XMLUltil
    {

        #region XDocument Ultilities
        public static Dictionary<string, string> XMLToDict(this XDocument xdoc)
        {
            try
            {
                Dictionary<string, string> allNodes = new Dictionary<string, string>();
                if (xdoc != null)
                {
                    foreach (XElement element in xdoc.Descendants().Where(p => p.HasElements == false))
                    {

                        allNodes.Add(element.Name.LocalName, element.Value);
                    }
                }
                return allNodes;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw;
            }

        }

        public static void UpdateXMLByName(this XDocument doc, string tag_name, string value)
        {
            try
            {
                bool isdone = false;
                foreach (XElement element in doc.Descendants())
                {
                    updateXMLElementValue(element, tag_name, value, isdone);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw;
            }

        }

        private static void updateXMLElementValue(XElement element, string tag_name, string value, bool isdone)
        {
            // update value with encrypted value
            if (isdone)
                return;
            if (element.HasElements)
            {
                foreach (XElement child in element.Elements())
                {
                    if (child is XElement)
                    {
                        if (!isdone) updateXMLElementValue(child as XElement, tag_name, value, isdone);
                    }
                }
            }
            else
            {
                if (element.Name != null && element.Name == tag_name)
                {
                    element.Value = value;
                    isdone = true;
                }
            }
        }

        public static string GetXMLByName(this XDocument doc, string tag_name)
        {
            try
            {
                var element = doc.Descendants(tag_name).FirstOrDefault();

                if (element == null)
                    return string.Empty;

                return element.Value;

            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw;
            }

        }

        public static XDocument LoadXMLXDoc(string path)
        {
            XDocument doc = null;
            if (string.IsNullOrWhiteSpace(path) | !File.Exists(path))
                return doc;
            doc = XDocument.Load(path);
            return doc;
        }

        #endregion




        public static XmlDocument LoadXML(string path)
        {
            XmlDocument doc = null;
            if (string.IsNullOrWhiteSpace(path) | !File.Exists(path))
                return doc;
            doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }


        public static T parsetoObject<T>(this XmlDocument xmldoc, T obj)
        {
            try
            {
                T response;
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (XmlReader reader = new XmlNodeReader(xmldoc))
                {
                    response = (T)serializer.Deserialize(reader);
                }

                return response;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw;
            }

        }

        public static string XMLToString(this XDocument xmlDoc)
        {
            return xmlDoc.ToString();
        }

        public static string ConvertToXMLString(this object obj)
        {

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            StringWriter textWriter = null;
            try
            {
                textWriter = new StringWriter(CultureInfo.InvariantCulture);
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, obj, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                }
                return textWriter.ToString(); //This is the output as a string
            }
            finally
            {
                //fix CA2022
                if (textWriter != null)
                    textWriter.Dispose();
            }
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null) return "";

            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}