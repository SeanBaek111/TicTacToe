using CsvHelper.Configuration;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace TicTacToe;

public static class EnumExtension
{
    /// <summary>
    /// Extension method to return an formatted string of type T for the given enum.
    /// </summary>
    public static string ToStringExt(this Enum value)
    {
        return value.ToString().Replace("_", " ");
    }

    /// <summary>
    /// Extension method to return an enum value of type string for the given int.
    /// And formatted it to string.
    /// </summary>
    public static string ToString<T>(this int value)
    {
        string? name = Enum.GetName(typeof(T), value);
        return String.IsNullOrEmpty(name) ? String.Empty : name.Replace("_", " ");
    }

    /// <summary>
    /// Extension method to return an enum value of type T for the given string.
    /// </summary>
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    /// <summary>
    /// Extension method to return an enum value of type T for the given int.
    /// </summary>
    public static T ToEnum<T>(this int value)
    {
        var name = Enum.GetName(typeof(T), value);
        return name.ToEnum<T>();
    }

    /// <summary>
    /// Allow Enum to use LINQ
    /// </summary>
    public static IEnumerable<T> Query<T>() where T : struct, Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    /// <summary>
    /// Convert Stack to List.
    /// </summary>
    public static List<T> ConvertToList<T>(this Stack<T> value)
    {
        List<T> newList = new();
        foreach (T i in value)
        {
            newList.Add(i);
        }

        newList.Reverse();
        return newList;
    }

    /// <summary>
    /// Save the Stack data into a CSV file.
    /// </summary>
    public static bool SaveToCsvExt<T>(this Stack<T> gameData, string path)
    {
        try
        {
            // Convert stack to list.
            List<T> bs = gameData.ConvertToList<T>();
            //List<string> lines = new();

            //IEnumerable<PropertyDescriptor> props = TypeDescriptor
            //    .GetProperties(typeof(T))
            //    .OfType<PropertyDescriptor>();

            //// Fillin the header.
            //string header = string.Join(",", props.ToList().Select(x => x.Name));

            //lines.Add(header);

            //// Fillin the content.
            //// Since most of the properties are not general string.
            //// This brainless code will not work.
            ////IEnumerable<string> valueLines = bs
            ////    .Select(row => string.Join(",", header.Split(',')
            ////                                       .Select(a =>
            ////                                               row.GetType()
            ////                                               .GetProperty(a)
            ////                                               .GetValue(row, null))));

            //lines.AddRange(valueLines);
            //File.WriteAllLines(path, lines.ToArray());
            ConvertToCsv(bs, path);
            return true;
        }
        // Well, incase something went wrong.
        catch (Exception e)
        {
            throw e;
        }
    }
    public static void ConvertToCsv<T>(IEnumerable<T> objects, string filePath)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header row
            List<string> header = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                if (IsSimpleType(property.PropertyType))
                {
                    header.Add(property.Name);
                }
                else
                {
                    PropertyInfo[] subProperties = property.PropertyType.GetProperties();
                    foreach (PropertyInfo subProperty in subProperties)
                    {
                        header.Add(property.Name + "_" + subProperty.Name);
                    }
                }
            }
            writer.WriteLine(string.Join(",", header));

            // Write data rows
            foreach (T obj in objects)
            {
                List<string> values = new List<string>();
                foreach (PropertyInfo property in properties)
                {
                    if (IsSimpleType(property.PropertyType))
                    {
                        object value = property.GetValue(obj);
                        if (value != null)
                        {
                            values.Add(value.ToString());
                        }
                        else
                        {
                            values.Add("");
                        }
                    }
                    else
                    {
                        object subObject = property.GetValue(obj);
                        if (subObject != null)
                        {
                            PropertyInfo[] subProperties = property.PropertyType.GetProperties();
                            foreach (PropertyInfo subProperty in subProperties)
                            {
                                object subValue = subProperty.GetValue(subObject);
                                if (subValue != null)
                                {
                                    values.Add(subValue.ToString());
                                }
                                else
                                {
                                    values.Add("");
                                }
                            }
                        }
                        else
                        {
                            PropertyInfo[] subProperties = property.PropertyType.GetProperties();
                            foreach (PropertyInfo subProperty in subProperties)
                            {
                                values.Add("");
                            }
                        }
                    }
                }
                writer.WriteLine(string.Join(",", values));
            }
        }
    }

    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }
}