/* 
 * ******************************************************** *
 * Name: XML File Configuration class                       *
 * Version: 1.2                                             *
 * Version date: 3 March 2012                               *
 * Description: Created because I missed TIniFile from      *
 *              ObjectPascal :D                             *
 * Author: saper_2                                          *
 * ******************************************************** *
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

public class XMLConfigClass
{
    private string path;
    private XmlElement root_node;
    private XmlDocument xmlDoc;

    private bool docValid = false;

    public string Path { get { return path; } }
    public bool ValidFile { get { return docValid; } }

    private XmlNode FindSection(string secName)
    {
        for (int i = 0; i < root_node.ChildNodes.Count; i++)
        {
            if (root_node.ChildNodes[i].Name == secName)
            {
                return root_node.ChildNodes[i];
            }
        }
        return null;
    }

    private XmlNode FindName(string name, XmlNode n)
    {
        for (int i = 0; i < n.ChildNodes.Count; i++)
        {
            if (n.ChildNodes[i].Name == name)
            {
                return n.ChildNodes[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Create new config or load existing config
    /// </summary>
    /// <param name="file_path">If null ar file not exists create empty config, if file exists load it.</param>
    public XMLConfigClass(string file_path)
	{
        path = file_path;
        XmlDocument doc = new XmlDocument();

        docValid = false;

        if (File.Exists(path))
        {
            // open current xml settings file
            doc.Load(path);
            root_node = doc.DocumentElement;
            if (root_node.Attributes.Count == 1)
            {
                try
                {
                    if (Convert.ToInt16(root_node.Attributes["version"].Value) == 1)
                    {
                        docValid = true;
                    }
                }
                catch
                {
                    docValid = false;
                }
            }
            else
            {
                //MessageBox.Show("Invalid configuration file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                root_node = null;
                docValid = false;
                throw new Exception("File exists but have invalid format!");
            }

        }
        else
        {
            // create new one xml settings file
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><settings version=\"1\"></settings>");
            root_node = doc.DocumentElement;
        }
        xmlDoc = doc;
	}
    /// <summary>
    /// Load configuration file.
    /// </summary>
    /// <param name="file_name">Configuration file path, if null then try load with file path of class constructor parameter</param>
    public void Load(string file_name=null)
    {
        if (file_name != null) path = file_name;

        xmlDoc.Load(path);

    }
    /// <summary>
    /// Save configuration to file.
    /// </summary>
    /// <param name="file_name">If null then try save to file in class constructor parameter</param>
    public void Save(string file_name=null)
    {
        if (file_name != null) path = file_name;

        xmlDoc.Save(path);
    }
    
    public void RemoveName(string section, string name)
    {
        XmlNode n, n2;

        if (root_node == null) return;
        if ((n = FindSection(section)) == null) return;
        if ((n2 = FindName(name, n)) == null) return;

        n.RemoveChild(n2);
    }

    public void RemoveSection(string section)
    {
        XmlNode n;

        if (root_node == null) return;
        if ((n = FindSection(section)) == null) return;

        root_node.RemoveChild(n);
    }

    public int ReadInt(string section, string name, int Default=0) 
    {
        XmlNode n, n2;
        int i=Default;

        if (root_node == null) return Default;
        if ((n = FindSection(section)) == null) return Default;
        if ((n2 = FindName(name, n)) == null) return Default;

        try
        {
            i = Convert.ToInt32(n2.InnerText);
        }
        catch
        {
            throw new Exception("Section: " + section + ", Ident.: " + name + " - Is not a integer type");
        }

        return i;
    }

    public string ReadString(string section, string name, string Default = "")
    {
        XmlNode n, n2;
        string i = Default;

        if (root_node == null) return Default;
        if ((n = FindSection(section)) == null) return Default;
        if ((n2 = FindName(name, n)) == null) return Default;

        try
        {
            i = Convert.ToString(n2.InnerText);
        }
        catch
        {
            throw new Exception("Section: " + section + ", Ident.: " + name + " - Is not a string type");
        }

        return i;
    }

    public Int64 ReadInt64(string section, string name, Int64 Default = 0)
    {
        XmlNode n, n2;
        Int64 i = Default;

        if (root_node == null) return Default;
        if ((n = FindSection(section)) == null) return Default;
        if ((n2 = FindName(name, n)) == null) return Default;

        try
        {
            i = Convert.ToInt64(n2.InnerText);
        }
        catch
        {
            throw new Exception("Section: " + section + ", Ident.: " + name + " - Is not a integer 64bit type");
        }

        return i;
    }

    public bool ReadBool(string section, string name, bool Default = false)
    {
        XmlNode n, n2;
        bool i = Default;

        if (root_node == null) return Default;
        if ((n = FindSection(section)) == null) return Default;
        if ((n2 = FindName(name, n)) == null) return Default;

        try
        {
            i = Convert.ToBoolean(n2.InnerText);
        }
        catch
        {
            throw new Exception("Section: " + section + ", Ident.: " + name + " - Is not a boolean type");
        }

        return i;
    }

    public double ReadDouble(string section, string name, double Default = 0.0)
    {
        XmlNode n, n2;
        double i = Default;

        if (root_node == null) return Default;
        if ((n = FindSection(section)) == null) return Default;
        if ((n2 = FindName(name, n)) == null) return Default;

        try
        {
            i = Convert.ToDouble(n2.InnerText);
        }
        catch
        {
            throw new Exception("Section: " + section + ", Ident.: " + name + " - Is not a double type");
        }

        return i;
    }


    public void WriteInt(string section, string name, int value)
    {
        XmlNode n, n2;

        if (root_node == null) throw new Exception("Configuration not created.");
        if ((n = FindSection(section)) == null)
        {
            // create section
            n = xmlDoc.CreateNode(XmlNodeType.Element,section, null);
            root_node.AppendChild(n);
        }
        if ((n2 = FindName(name, n)) == null)
        {
            n2 = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            n.AppendChild(n2);
        }

        n2.InnerText = Convert.ToString(value);

    }

    public void WriteString(string section, string name, string value)
    {
        XmlNode n, n2;

        if (root_node == null) throw new Exception("Configuration not created.");
        if ((n = FindSection(section)) == null)
        {
            // create section
            n = xmlDoc.CreateNode(XmlNodeType.Element, section, null);
            root_node.AppendChild(n);
        }
        if ((n2 = FindName(name, n)) == null)
        {
            n2 = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            n.AppendChild(n2);
        }

        n2.InnerText = Convert.ToString(value);
    }

    public void WriteBool(string section, string name, bool value)
    {
        XmlNode n, n2;

        if (root_node == null) throw new Exception("Configuration not created.");
        if ((n = FindSection(section)) == null)
        {
            // create section
            n = xmlDoc.CreateNode(XmlNodeType.Element, section, null);
            root_node.AppendChild(n);
        }
        if ((n2 = FindName(name, n)) == null)
        {
            n2 = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            n.AppendChild(n2);
        }

        n2.InnerText = Convert.ToString(value);
    }

    public void WriteDouble(string section, string name, double value)
    {
        XmlNode n, n2;

        if (root_node == null) throw new Exception("Configuration not created.");
        if ((n = FindSection(section)) == null)
        {
            // create section
            n = xmlDoc.CreateNode(XmlNodeType.Element, section, null);
            root_node.AppendChild(n);
        }
        if ((n2 = FindName(name, n)) == null)
        {
            n2 = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            n.AppendChild(n2);
        }

        n2.InnerText = Convert.ToString(value);
    }

    public List<string> SectionList()
    {
        List<string> lst = new List<string>();

        foreach (XmlNode n in root_node.ChildNodes)
        {
            string s = n.Name;
            lst.Add(s);
        }
        return lst;
    }

    public List<string> NameList(string section)
    {
        List<string> lst = new List<string>();
        XmlNode nn;
        if ((nn = FindSection(section)) == null)
        {
            throw new Exception("Section: " + section + " does not exists!");
        }
        foreach (XmlNode n in nn.ChildNodes)
        {
            string s = n.Name;
            lst.Add(s);
        }
        return lst;
    }
}
