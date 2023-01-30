using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;
static internal class XmlTools
{
    static string dir = @"xml\";

    public static XElement LoadListFromXMLElement(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                return XElement.Load(dir + filePath);
            }
            else
            {
                XElement rootElement = new XElement(filePath);
                if (filePath == @"config.xml") { }
                //rootElem.Add(new XElement("droneRunningNum", 1)); //להוריד
                rootElement.Save(dir + filePath);
                return rootElement;
            }
        }
        catch
        {
            throw new Exception("Could not load file.");
        }
    }

    public static void SaveListToXMLElement(XElement rootElement, string filePath)
    {
        try
        {
            rootElement.Save(dir + filePath);
        }
        catch
        {
            throw new Exception("Could not save file.");
        }
    }

    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(dir + filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch
        {
            throw new Exception("Failed to load xml file.");
        }
    }

    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(dir + filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch
        {
            throw new Exception("Failed to create xml file.");
        }
    }

}