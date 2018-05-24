/**
 * DebugExtensions.cs
 * Created by: Joao Borks [joao.borks@gmail.com]
 * Created on: 18/03/18 (dd/mm//yy)
 */

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public static class DebugExtensions 
{
    const int IdentSpaces = 2;

    public static void LogCollection<T>(IEnumerable<T> collection)
    {
        Debug.Log(CollectionToString(collection));
    }

    public static void LogJaggedArray<T>(T[][] jaggedArray)
    {
        var sb = new StringBuilder();
        var typeName = typeof(T).Name;
        sb.AppendFormat("{0} jagged array with {1} arrays:\n", typeName, jaggedArray.Length);
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            sb.AppendFormat("- [{0}] {1} array with {2} elements:\n", i, typeName, jaggedArray[i].Length);
            for (int j = 0; j < jaggedArray[i].Length; j++)
                sb.AppendFormat("  - [{0}] {1}\n", j, jaggedArray[i][j].ToString());
        }
        Debug.Log(sb);
    }

    static string CollectionToString<T>(IEnumerable<T> collection, int identLevel = 0)
    {
        var sb = new StringBuilder();
        int count = collection.Count();
        sb.AppendFormat("{0}{1} collection with {2} elements{3}\n", identLevel > 0 ? new string(' ', IdentSpaces * identLevel) : "", typeof(T).Name, count, count > 0 ? ":" : "");
        for (int i = 0; i < count; i++)
            sb.AppendFormat("{0}[{1}] {2}\n", new string(' ', IdentSpaces * (identLevel + 1)), i, collection.ElementAt(i).ToString());
        return sb.ToString();
    }
}