using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using CsvHelper;
using static TicTacToe.Enums;

namespace TicTacToe;

public class FileManager
{
    /// <summary>
    /// It's the default file manager.
    /// </summary>
    const string DEFAULT_FILENAME = "save.csv";

    /// <summary>
    /// private static readonly Lazy<FileManager> lazy =
    ///     new Lazy<FileManager>(() => new FileManager());
    /// </summary>
    private static readonly Lazy<FileManager> lazy = new(() => new());

    public static FileManager Instance { get { return lazy.Value; } }

    private FileManager()
    {

    }

    /// <summary>
    /// Check if the file is exists.
    /// </summary>
    public bool FileExists(string fileName = DEFAULT_FILENAME)
    {
        // Suppose to return if the file exists.
        return File.Exists(fileName);
    }

    /// <summary>
    /// Create a save file.
    /// </summary>
    public bool CreateSaveFile(string fileName = DEFAULT_FILENAME)
    {
        try
        {
            File.Create(fileName).Dispose();
        }
        catch (IOException e)
        {
            Menu menu = new Menu();
            menu.SetQuestion("Save file detected... override?");
            EnumExtension.Query<ConfirmationEnum>().All(a =>
            {
                menu.AddMenuEnum(a);
                return true;
            });
            switch (menu.GetUserAnswer())
            {
                // YES
                case 1:
                    File.WriteAllText(fileName, string.Empty);
                    break;
                // NO
                case 2:
                    return false;
                // QUIT
                case 3:
                    Environment.Exit(0);
                    break;

                default:
                    break;
            };
        }
        return true;
    }

    /// <summary>
    /// Save the game progress.
    /// </summary>
    public bool SaveProgress(Stack<GameStatus> logs, string fileName = DEFAULT_FILENAME)
    {
        // Use local method to create a save file.
        bool create = this.CreateSaveFile(fileName);

        // Use extenion method to save the progress to file.
        bool saveResult = logs.SaveToCsv<GameStatus>(fileName);


        saveResult = logs.SaveToBin<GameStatus>(fileName);
         
        //LoadProgress(fileName);

        Stack<GameStatus> logs2 = (Stack<GameStatus>) EnumExtension.LoadFromBin(fileName);
        // Return true or false based on how file are saved.
        return saveResult;
    }

    /// <summary>
    /// Load the game progress from csv file and return the Stack object
    /// </summary>
    public Stack<GameStatus> LoadProgress(string fileName = DEFAULT_FILENAME)
    {
        if (!this.FileExists())
        {
            // File doesn't exists.
            throw new FileLoadException();
        }

        Stack<GameStatus> logs = ConvertToObject<GameStatus>(fileName);

        
        return logs;
    }
    
    public static Stack<T> ConvertToObject<T>(string filePath) where T : new()
    {
        Stack<T> objects = new Stack<T>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                throw new Exception("CSV file is empty");
            }
            string[] headers = headerLine.Split(',');

            while (!reader.EndOfStream)
            {
                string dataLine = reader.ReadLine();
                if (dataLine == null)
                {
                    continue;
                }
                string[] values = dataLine.Split(',');

                T obj = new T();
                SetPropertyValues(obj, headers, values);

                objects.Push(obj);
            }
        }

        return objects;
    }

    /// <summary>
    ///  Recursive-able method to set properties.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="headers"></param>
    /// <param name="values"></param>
    /// <param name="startIndex"></param>
    /// <typeparam name="T"></typeparam>
    private static void SetPropertyValues<T>(T obj, string[] headers, string[] values, int startIndex = 0) where T : new()
    {
        Type objType = obj.GetType();
        PropertyInfo[] properties = objType.GetProperties();

        for (int i = startIndex; i < headers.Length; i++)
        {
            string header = headers[i];
            string value = values[i];

            if (header.Contains("_"))
            {
                string[] subHeaders = header.Split('_');
                PropertyInfo property = properties.FirstOrDefault(p => p.Name == subHeaders[0]);
                if (property == null)
                {
                    continue;
                }
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    // If the property is a value type or string, we can set the value directly
                    object subObj = property.GetValue(obj) ?? Activator.CreateInstance(property.PropertyType);
                    SetPropertyValues(subObj, subHeaders.Skip(1).ToArray(), new[] { value }, 0);
                    property.SetValue(obj, subObj);
                }
                else
                {
                    // If the property is a complex type, we need to recursively set its sub-properties
                    object subObj = property.GetValue(obj);
                    if (subObj == null)
                    {
                        Type subType = property.PropertyType;
                        if (subType.IsAbstract)
                        {
                            // If the property type is abstract, find a concrete subclass that can be instantiated
                            Assembly assembly = Assembly.GetAssembly(objType);
                            subType = assembly.GetTypes().FirstOrDefault(t => !t.IsAbstract && subType.IsAssignableFrom(t));
                            if (subType == null)
                            {
                                // If no concrete subclass was found, skip this property
                                continue;
                            }
                        }
                        subObj = Activator.CreateInstance(subType);
                        property.SetValue(obj, subObj);
                    }
                    SetPropertyValues(subObj, subHeaders.Skip(1).ToArray(), new[] { value }, 0);
                }
            }
            else
            {
                // If the header does not contain an underscore, we can set the property value directly
                PropertyInfo property = properties.FirstOrDefault(p => p.Name == header);
                if (property == null)
                {
                    continue;
                }
                object convertedValue;
                if (property.PropertyType.IsEnum)
                {
                    // If the property is an enumeration, use the Enum.Parse method to convert the string value to the enumeration value
                    convertedValue = Enum.Parse(property.PropertyType, value);
                }
                else
                {
                    // If the property is not an enumeration, use the Convert.ChangeType method to convert the string value to the property type
                    convertedValue = Convert.ChangeType(value, property.PropertyType);
                }
                property.SetValue(obj, convertedValue);
            }
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
}
