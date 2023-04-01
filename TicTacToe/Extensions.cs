using System;
using System.ComponentModel;

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

    public static bool SaveToCsv<T>(this Stack<T> gameData, string path)
    {
        try
        {
            // Reverse it because the list coming from Stack
            List<T> bs = gameData.ConvertToList<T>();
            List<string> lines = new();

            IEnumerable<PropertyDescriptor> props = TypeDescriptor
                .GetProperties(typeof(T))
                .OfType<PropertyDescriptor>();

            string header = string.Join(",", props.ToList().Select(x => x.Name));

            lines.Add(header);

            IEnumerable<string> valueLines = bs
                .Select(row => string.Join(",", header.Split(',')
                                                   .Select(a =>
                                                           row.GetType()
                                                           .GetProperty(a)
                                                           .GetValue(row, null))));

            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
