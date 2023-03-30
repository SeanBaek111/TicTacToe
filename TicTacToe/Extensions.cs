using System;

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

}
