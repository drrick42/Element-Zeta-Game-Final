using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ItemAttributes {

    public static ItemAttributes Default { get; private set; }
    static Dictionary<string, ItemAttributes> allData;

    static ItemAttributes()
    {
        Default = new ItemAttributes(
            null,//text
            false,//textApparent
            false,//grabbable
            false//mem
            );
        InitializeData();
    }

    public static ItemAttributes AttOrDefault(Transform transform)
    {
        var x = transform.GetComponent<PropertyAccessor>();
        if (x == null)
            return Default;
        return x.Attributes;
    }

    public ItemAttributes(string text, bool textApparent, bool grabbable, bool memorable)
    {
        Text = text;
        TextApparent = textApparent;
        Grabbable = grabbable;
        Memorable = memorable;
    }

    public ItemAttributes(ItemAttributes clone)
    {
        Text = clone.Text;
        TextApparent = clone.TextApparent;
        Grabbable = clone.Grabbable;
        Memorable = clone.Memorable;
    }

    //reads everything from file at once, populating dictionary
    public static void InitializeData()
    {
        allData = new Dictionary<string, ItemAttributes>();
        //split into items
        string[] data = File.ReadAllText("Assets/Scripts/ScriptResources/itemdata.txt").Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
        
        foreach (string bigstr in data)
        {
            ItemAttributes newAtt = null;
            //split item into lines
            string[] args = bigstr.Split('\n');
            string[] arg0 = args[0].Split('~');
            if (arg0.Length > 1)
                newAtt = new ItemAttributes(allData[arg0[1].Trim()]);//inheritance
            else
                newAtt = new ItemAttributes(Default);//inherit from default
            for (int i = 1; i < args.Length; i++)
            {
                ParseField(args[i], newAtt);
            }

            allData.Add(arg0[0].Trim(), newAtt);
        }
    }

    static void ParseField(string listItem, ItemAttributes atts)
    {
        string[] parts = System.Text.RegularExpressions.Regex.Split(listItem, ":\\s+");
        if (parts[0] == "Text")
            atts.Text = ParseText(parts[1]);
        else if (parts[0] == "Grabbable")
            atts.Grabbable = bool.Parse(parts[1]);
        else if (parts[0] == "TextApparent")
            atts.TextApparent = bool.Parse(parts[1]);
        else if (parts[0] == "Memorable")
            atts.Memorable = bool.Parse(parts[1]);
        else
            throw new Exception("Error in ItemAttribute parse: " + listItem);
    }

    static string ParseText(string str){
        return str.Replace("\\n", "\n");
    }

    public static ItemAttributes GetAttributes(string id)
    {
        try
        {
            return allData[id];
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("Property Accessor - Key not found: " + id);
            return null;
        }
    }

    public string Text { get; private set; }
    public bool TextApparent { get; private set; }
    public bool Grabbable { get; private set; }
    public bool Memorable { get; private set; }
}
