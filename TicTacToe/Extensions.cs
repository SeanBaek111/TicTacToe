using CsvHelper.Configuration;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Runtime.Serialization.Formatters.Binary;

namespace TicTacToe;

public static class Extensions
{
    /// <summary>
    /// Set the text position at the very center of the terminal.
    /// </summary>
    public static void PrintCenter(this string value, int ms = 0)
    {
        int position = (Console.WindowWidth - value.Length) / 2;

        if (position > 0)
            Console.SetCursorPosition(position, Console.CursorTop);

        foreach (var i in value.Replace(Environment.NewLine, ""))
        {
            Console.Write(i);
            Thread.Sleep(ms);
        }
        Console.WriteLine(Environment.NewLine);
    }

    public static void WriteLineCenter(string format, params object[] args)
    {
        string value = string.Format(format, args);
        int padding = (Console.WindowWidth - value.Length) / 2;
        Console.WriteLine("{0," + padding + "}{1}", "", value);
    }

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
    public static bool SaveToCsv<T>(this Stack<T> gameData, string path)
    {
        try
        {
            // Convert stack to list.
            List<T> bs = gameData.ConvertToList<T>();

            // Define general type
            Type type = typeof(T);
            // Get the properties of general type
            PropertyInfo[] properties = type.GetProperties();

            using (StreamWriter writer = new StreamWriter(path))
            {
                // Write header row
                List<string> header = new List<string>();
                // loop through all the properties.
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsSubclassOf(typeof(Player)))
                    {
                        //
                        int a = 0;
                        a++;
                    }
                    else if (property.PropertyType.IsSubclassOf(typeof(Player)))
                    {

                        int a = 0;
                        a++;
                    }
                    // Check if the property are nest-class
                    if (IsSimpleType(property.PropertyType))
                    {
                        header.Add(property.Name);
                    }
                    else
                    {
                        // add the header with nest-class prefix
                        PropertyInfo[] subProperties = property.PropertyType.GetProperties();
                        foreach (PropertyInfo subProperty in subProperties)
                        {
                            header.Add(property.Name + "_" + subProperty.Name);
                        }
                    }
                }

                writer.WriteLine(string.Join(",", header));

                // Write data rows
                foreach (T obj in bs)
                {
                    // Define empty string list.
                    List<string> values = new List<string>();
                    // Loop through all the properties
                    foreach (PropertyInfo property in properties)
                    {
                        // Check if the property are nest-class
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
                return true;
            }
        }
        // Well, incase something went wrong.
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// check if the property is simple type. (nested)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }

    public static bool SaveToBin<T>(this Stack<T> gameData, string path)
    {
        bool bRes = false;
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(fileStream, gameData);
            fileStream.Close();
            bRes = true;
        }
        catch (SystemException e)
        {
            throw e;
        }
        return bRes;
    }



    public static object LoadFromBin(string path)
    {
        object obj = (object)null;
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            obj = new BinaryFormatter().Deserialize((Stream)fileStream);
            fileStream.Close();
        }
        catch (SystemException e)
        {
            throw e;
        }
        return obj;
    }

}
