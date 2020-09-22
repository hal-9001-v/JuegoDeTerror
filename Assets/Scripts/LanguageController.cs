using System.Collections;
using UnityEngine;
using System.IO;

public static class LanguageController
{
    public static int language = 1;             //0 = inglés, 1 = español

    private static Hashtable languageHashTable; //HashTable de tipo key(string nombre) -> value(atring[] con el valor en multiples idiomas)
    private static int totalLanguages;          //Número total de idiomas

    //Método que crea y rellena la HashTable con todo el contenido del fichero de texto
    public static void LoadLanguagesFile(string fileName)
    {
        try {
            //1. Encontramos el fichero de texto y le decimos en qué codificación de caracteres queremos que nos lo lea
            using (StreamReader reader = new StreamReader(Application.dataPath + @"\Scripts\Languages\" + fileName, System.Text.Encoding.GetEncoding(1252)))
            {
                //2. Inicializamos la HashTable
                languageHashTable = new Hashtable();

                //3. Detectamos el número total de idiomas que contiene el fichero de texto (este número siempre estará en la primera línea)
                string line = reader.ReadLine();
                totalLanguages = int.Parse(line);

                string key;     //Variable que guarda la clave
                string[] values;//Variable que guarda los valores de una clave en todos los idiomas

                //4. Bucle encargado de rellenar toda la HashTable
                line = reader.ReadLine();
                while (line != null)
                {
                    if (line == ";")
                    {
                        key = reader.ReadLine();
                        values = new string[totalLanguages];
                        for (int i = 0; i < totalLanguages; i++)
                        {
                            line = reader.ReadLine();
                            values[i] = line;
                        }

                        languageHashTable.Add(key, values);
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
            }
        }
        catch(DirectoryNotFoundException e)
        {
            Debug.LogWarning("LanguageController : Ruta del fichero de texto no encontrada\n" + e);
        }
    }

    //Método que devuelve el trozo de texto especificado a partir de una clave y el idioma
    public static string GetTextInLanguage(string key)
    {
        //1. Comprobamos que la HashTable no esté vacía
        if(languageHashTable == null)
        {
            Debug.LogWarning("LanguageController : la HashTable de idiomas está vacía");
            return key;
        }
        else
        {
            if(languageHashTable.ContainsKey(key))
            {
                //2. Extraemos el valor de la clave "key" y lo guardamos en values. Posteriormente devolvemos la posición del array que tiene el idioma "language"
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
