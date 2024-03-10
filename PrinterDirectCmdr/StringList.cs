/* ************************************************************************************************
 * Author : saper_2
 * Version: 0.2 (20230126)
 * Description: Class that act similiar to Pascal TStringList
 * Changelog:
 *      - 0.1 - initial version
 *      - 0.2 - added more features (mainly routines for handling Names(keys)+Value)
 * ************************************************************************************************ */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public enum StringListMatchOptions { slmCaseSensitive = 0, slmCaseInsensitive };

public class StringNameValuePair
{
    public string Name { get; set; }
    public string Value { get; set; }

    /// <summary>
    /// Default empty constructor
    /// </summary>
    public StringNameValuePair()
    {
        // empty constructor
    }

    public StringNameValuePair(string name)
    {
        Name = name;
    }

    public StringNameValuePair(string name, string value)
    {
        Name = name;
        Value = value;
    }


}

public class StringList : CollectionBase
{
    /// <summary>
    /// Specify delimieter for DelimitedText
    /// </summary>
    public string Delimieter { get; set; } = ",";

    /// <summary>
    /// Specify character used to separate Key from value part
    /// </summary>
    public string NameValueSeparator { get; set; } = "=";

    /// <summary>
    /// Separator for New Line
    /// </summary>
    public string NewLineSeparator { get; set; } = Environment.NewLine;
    
    /// <summary>
    /// StringList as string where each line is separated by Delimieter
    /// </summary>
    public string DelimietedText
    {
        get
        {
            string res = "";
            for (int i = 0; i < List.Count; i++) {
                res += (string)List[i];
                if ((i + 1) < List.Count) {
                    res += Delimieter;
                }
            }
            return res;
        }
        set
        {
            string[] si = value.Split(new string[] { Delimieter }, StringSplitOptions.None);
            List.Clear();
            foreach (string s in si)
            {
                List.Add(s);
            }
        }
    }

    /// <summary>
    /// Lists the StringList as string where each line id delimited by NewLineSeparator (default: Environment.NewLine).
    /// </summary>
    public string Text
    {
        get
        {
            string res = "";
            for (int i = 0; i < List.Count; i++)
            {
                res += (string)List[i];
                if ((i + 1) < List.Count)
                {
                    res += NewLineSeparator;
                }
            }
            return res;
        }
        set
        {
            string[] si = value.Split(new string[] { NewLineSeparator }, StringSplitOptions.None);
            List.Clear();
            foreach (string s in si)
            {
                List.Add(s);
            }
        }
    }

    /// <summary>
    /// Add new string to StringList
    /// </summary>
    /// <param name="item"></param>
    public void Add(string item)
    {
        List.Add(item);
    }


    public int Add(string[] stringArray)
    {
        if (stringArray.Length > 0)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                string s = stringArray[i];
                List.Add(s);
            }
            return stringArray.Length;
        }
        return 0;
    }

    /// <summary>
    /// Add items from straing array , but ony unique items are added.
    /// Returns added items count.
    /// </summary>
    /// <param name="stringArray"></param>
    /// <returns></returns>
    public int AddUnique(string[] stringArray)
    {
        int cnt = 0;
        if (stringArray.Length > 0)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                string s = stringArray[i];
                if (AddUnique(s)) cnt++;
            }
        }
        return cnt;
    }



    /// <summary>
    /// Add Name-Value pair.
    /// If name already exists ArgumentException is thrown.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AddNameValuePair(string name, string value)
    {
        int idx = IndexOfName(name);
        if (idx == -1)
        {
            string s = name + NameValueSeparator + value;
            List.Add(s);
        }
        // if idx <> -1 , then name exists, so throw error
        throw new ArgumentException("Can not add NameValuePair with name `" + name + "` - name already exists, name must be unique!");
    }

    /// <summary>
    /// Add new item to list only if it's unique.
    /// Return true if added, false if not.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddUnique(string item)
    {
        // check if not exists
        int i = Exists(item);
        if (i == 0)
        {
            Add(item);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Remove item from stringList
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void Remove(int index)
    {
        if (index >= List.Count || index < 0) throw new IndexOutOfRangeException("Index (" + index.ToString() + ") out of bonds (" + index.ToString() + ")");
        List.RemoveAt(index);
    }

    /// <summary>
    /// Get StringList item by item index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public string this[int index]
    {
        get
        {
            if (index >= List.Count || index < 0) throw new IndexOutOfRangeException("Index (" + index.ToString() + ") out of bonds (" + index.ToString() + ")");
            return (string)List[index];
        }
        set
        {
            if (index >= List.Count || index < 0) throw new IndexOutOfRangeException("Index (" + index.ToString() + ") out of bonds (" + index.ToString() + ")");
            List[index] = value;
        }
    }

    /// <summary>
    /// Get names in array
    /// </summary>
    public string[] Names
    {
        get
        {
            return GetNames();
        }
    }

    /// <summary>
    /// Get Values in array
    /// </summary>
    public string[] Values
    {
        get
        {
            return GetValues();
        }
    }

    //public int Count { get { return List.Count; } }
    /// <summary>
    /// Get Value from StringList by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public string this[string name]
    {
        get
        {
            int idx = IndexOfName(name);
            if (idx<0)
            {
                throw new KeyNotFoundException("Name `" + name + "` not exists in this list.");
            }
            return GetValue(idx);
        } 
        set
        {
            int idx = IndexOfName(name);
            if (idx < 0)
            {
                throw new KeyNotFoundException("Name `" + name + "` not exists in this list.");
            }
            //return GetValue(idx);
            SetValue(idx, value);
        }
    }
    
    /// <summary>
    /// Return all List in one string separated by ","
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string s = "";

        for (int i = 0; i < List.Count; i++)
        {
            s += ((string)List[i]);
            // check if this is not last item, then add ", " at end of string
            if (i < (List.Count - 1))
            {
                s += ", ";
            }
        }


        return s;
    }

    /// <summary>
    /// Return StringNameValuePair for item index. If item is not formated in Name-Value pair then StringNameValuePair.Name contains item text.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if index is out of range</exception>
    public StringNameValuePair GetNameValuePair(int index)
    {
        if (index < 0 || index >= List.Count) throw new IndexOutOfRangeException("Index `" + index.ToString() + "` out of range [0.." + List.Count.ToString() + "].");
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length > 1)
        {
            return new StringNameValuePair(ls[0], ls[1]);
        }
        return new StringNameValuePair(l);
    }


    /// <summary>
    /// Will return line Name (Key), or whole line if it's not in Key(Name)=Value notation
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException">If line index is out of range</exception>
    public string GetName(int index)
    {
        if (index < 0 || index >= List.Count) throw new IndexOutOfRangeException("Index `" + index.ToString() + "` out of range [0.." + List.Count.ToString() + "].");
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length>1)
        {
            return ls[0];
        } else
        {
            return l;
        }
    }

    /// <summary>
    /// Will return line Value, or whole line if it's not in Key(Name)=Value notation
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public string GetValue(int index)
    {
        if (index < 0 || index >= List.Count) throw new IndexOutOfRangeException("Index `" + index.ToString() + "` out of range [0.." + List.Count.ToString() + "].");
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length > 1)
        {
            return ls[1];
        }
        else
        {
            return l;
        }
    }

    /// <summary>
    /// Set item value (or line text otherwise) if it's formated in Name-Value pair by item index
    /// </summary>
    /// <param name="index">StringList item index</param>
    /// <param name="value">Item new value</param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void SetValue(int index, string value)
    {
        if (index < 0 || index >= List.Count) throw new IndexOutOfRangeException("Index `" + index.ToString() + "` out of range [0.." + List.Count.ToString() + "].");
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length>1)
        {
            l = ls[0] + NameValueSeparator + value;
        } else
        {
            l = value;
        }
        List[index] = l;
    }

    /// <summary>
    /// Set item value (or line text otherwise) if it's formated in Name-Value pair by Name
    /// </summary>
    /// <param name="name">Name-Value pair name</param>
    /// <param name="value">New value</param>
    /// <exception cref="KeyNotFoundException"></exception>
    public void SetValue(string name, string value)
    {
        int index = IndexOfName(name);
        if (index == -1)
        {
            throw new KeyNotFoundException("Name `" + name + "` not exists in this list.");
        }
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length > 1)
        {
            l = ls[0] + NameValueSeparator + value;
        }
        else
        {
            l = value;
        }
        List[index] = l;
    }

    /// <summary>
    /// Set new name (Name-Value pair) for StringList item at index.
    /// If item don't have Name, then item is formated into Name-Value pair, and item content is used as value
    /// </summary>
    /// <param name="index">Item index</param>
    /// <param name="name">new name</param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void SetName(int index, string name)
    {
        if (index < 0 || index >= List.Count) throw new IndexOutOfRangeException("Index `" + index.ToString() + "` out of range [0.." + List.Count.ToString() + "].");
        //int index = IndexOfName(name);
        //if (index == -1)
        //{
        //    throw new KeyNotFoundException("Name `" + name + "` not exists in this list.");
        //}
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length > 1)
        {
            l = name + NameValueSeparator + ls[1];
        }
        else
        {
            l = name + NameValueSeparator + l;
        }
        List[index] = l;
    }

    /// <summary>
    /// Set new Name for existing Name-Value pair.
    /// If StringList item don't have Name (so param[name] is the item text), item will be formated into Name-Value pair, and existing item text will be used as Value.
    /// </summary>
    /// <param name="name">Existing Name-Value pair Name (or item content)</param>
    /// <param name="new_name">New Name of Name-Value pair</param>
    /// <exception cref="KeyNotFoundException"></exception>
    public void SetName(string name, string new_name)
    {
        int index = IndexOfName(name);
        if (index == -1)
        {
            throw new KeyNotFoundException("Name `" + name + "` not exists in this list.");
        }
        string l = (string)List[index];
        string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
        if (ls.Length > 1)
        {
            l = new_name + NameValueSeparator + ls[1];
        }
        else
        {
            l = new_name + NameValueSeparator + l;
        }
        List[index] = l;
    }


    /// <summary>
    /// Return item index of given Name.
    /// Return -1 if not found.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int IndexOfName(string name)
    {
        for (int i = 0; i < List.Count; i++)
        {
            string l = (string)List[i];
            string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
            if (ls.Length>1)
            {
                if (ls[0] == name) return i;
            } else
            {
                if (l == name) return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Return Value from given Name, or return String.EMpty if not exists
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetValue(string name)
    {
        for (int i=0;i<List.Count;i++)
        {
            string l = (string)List[i];
            string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
            if (ls.Length > 1)
            {
                if (ls[0] == name)
                {
                    return ls[1];
                }
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// Get array of names
    /// </summary>
    /// <returns></returns>
    public string[] GetNames()
    {
        string[] ls = new string[List.Count];
        for(int i=0;i<List.Count;i++)
        {
            ls[i] = GetName(i);
        }
        return ls;
    }

    /// <summary>
    /// Get Array of values
    /// </summary>
    /// <returns></returns>
    public string[] GetValues()
    {
        string[] ls = new string[List.Count];
        for (int i = 0; i < List.Count; i++)
        {
            ls[i] = GetValue(i);
        }
        return ls;
    }

    /// <summary>
    /// Return StringList as array of strings
    /// </summary>
    /// <returns></returns>
    public string[] GetArray()
    {
        string[] strings = new string[List.Count];
        for (int i = 0; i < List.Count; i++) strings[i] = (string)List[i];
        return strings;
    }

    /// <summary>
    /// Return stringlist in list format with NewLineSeparator at end of each line
    /// </summary>
    /// <returns></returns>
    public string ToStringNL()
    {
        string s = "";

        for (int i = 0; i < List.Count; i++)
        {
            s += ((string)List[i]);
            // check if this is not last item, then add ", " at end of string
            if (i < (List.Count - 1))
            {
                s += NewLineSeparator;
            }
        }


        return s;
    }

    /// <summary>
    /// Find str and count it's occurence in string list
    /// </summary>
    /// <param name="str">String to be matched</param>
    /// <param name="matchOpt">Match options - default: case insensitive</param>
    /// <returns>match count</returns>
    public int Exists(string str, StringListMatchOptions matchOpt = StringListMatchOptions.slmCaseInsensitive)
    {
        int res = 0;

        if (Count > 0)
        {
            for (int i = 0; i < Count; i++)
            {
                if (matchOpt == StringListMatchOptions.slmCaseInsensitive)
                {
                    if (((string)List[i]).ToLower() == str.ToLower()) res++;
                }
                else
                {
                    if (((string)List[i]) == str) res++;
                }
            }
        }
        return res;
    }
    /// <summary>
    /// Wipe clean...
    /// </summary>
    /*public void Clear()
    {
        List.Clear();
    }*/

    public Dictionary<string,string> GetNameValuePairsDictionary()
    {
        Dictionary<string,string> pairs = new Dictionary<string,string>();
        for(int i=0;i<List.Count;i++)
        {
            string l = (string)List[i];
            string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
            if (ls.Length > 1)
            {
                pairs.Add(ls[0], ls[1]);
            } else
            {
                pairs.Add(l, string.Empty);
            }
        }
        return pairs;
    }

    public List<StringNameValuePair> GetNameValuePairsList()
    {
        List<StringNameValuePair> pairs = new List<StringNameValuePair>();
        for (int i = 0; i < List.Count; i++)
        {
            string l = (string)List[i];
            string[] ls = l.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
            if (ls.Length > 1)
            {
                pairs.Add(new StringNameValuePair(ls[0], ls[1]));
            }
            else
            {
                pairs.Add(new StringNameValuePair(l));
            }
        }
        return pairs;
    }

/*
    public List<KeyValuePair<string, string>> GetStringsKVP()
    {
        List<KeyValuePair<string, string>> skvp = new List<KeyValuePair<string, string>>();
        foreach(string s in List)
        {
            string[] vn = s.Split(new string[] { NameValueSeparator }, 2, StringSplitOptions.None);
            if (vn.Length > 0)
            {
                string key = vn[0];

                if (skvp.Exists()
                if (vn.Length == 1)
                {
                    skvp.Add(new KeyValuePair<string, string>(vn[0], ""));
                } else
                {
                    skvp.Add(new KeyValuePair<string, string>(vn[0], vn[1]));
                }
            }

        }
    }

    public string Values(string Name)
    {
        // T O D O
        


    }*/
}
