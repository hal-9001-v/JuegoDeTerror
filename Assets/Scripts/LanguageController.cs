using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LanguageController
{
    public static int language = 1; //0 = inglés, 1 = español

    private static Hashtable languageHashTable;
    private static int totalLanguages;

    public static void LoadLanguagesFile(string fileName)
    {
        TextAsset textFile = (TextAsset)Resources.Load("Languages/" + fileName, typeof(TextAsset));
        
        if(textFile == null)
        {
            Debug.LogWarning("LanguageManager : el archivo " + fileName + " no se encontró");
            return;
        }

        languageHashTable = new Hashtable();
        StringReader reader = new StringReader(textFile.text);

        string line = reader.ReadLine();
        totalLanguages = int.Parse(line);

        string key;
        string[] values;

        line = reader.ReadLine();
        while (line != null)
        {
            if(line == ";")
            {
                key = reader.ReadLine();
                values = new string[totalLanguages];
                for (int i = 0; i < totalLanguages; i++)
                {
                    line = reader.ReadLine();
                    values[i] = line;
                }

                languageHashTable.Add(key, values);
                Debug.Log(languageHashTable[key]);
            }
            line = reader.ReadLine();
        }
        reader.Close();
    }

    public static string GetTextInLanguage(string key, int language)
    {
        if(languageHashTable == null)
        {
            Debug.LogWarning("LanguageController : la HashTable de idiomas está vacía");
            return key;
        }
        else
        {
            if(languageHashTable.ContainsKey(key))
            {
                string[] values = (string[])languageHashTable[key];
                return values[language];
            }
            else
            {
                Debug.LogWarning("LanguageController : La palabra " + key + " no se encontró en la tabla");
                return key;
            }
        }
    }
}
