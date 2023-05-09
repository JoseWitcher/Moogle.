namespace MoogleEngine;
using System.Text.RegularExpressions;
public class Procesamiento
{
   public Dictionary<string,float>Mundo{get; private set;}//diccionario de idf,las palabras con la cantidad de documentos en q aparece
   
   public Dictionary<string,Dictionary<string,float>>Mundito{get; private set;}//diccionario de tf,todos los titulos de documentos con la cantidad de veces q se repite la palabra en cada documento
   
    public Procesamiento()//esto es un constructor para cuando se llame a un calse el salga
    {
        this.Mundo = new Dictionary<string, float>();
        this.Mundito = new Dictionary<string, Dictionary<string, float>>();
        lector();
    }

    string[] contenido=Directory.GetFiles("../Content");//Guarda las rutas de cada Doc

    void lector()//lee todo lo q hay en cada ruta de Doc
    {
    foreach (string doc in contenido)
    {
        string[] texto = normalizar(File.ReadAllText(doc));
        Dictionary<string, float> temporalTF = new Dictionary<string, float>();//este es el diccionario de arriba osea el dicc de dicc


        foreach (string word in texto)//esto me llena los diccionarios 
        {
            if(!Mundo.ContainsKey(word)) Mundo.Add(word,0);

            if(!temporalTF.ContainsKey(word)){
                temporalTF.Add(word, 1);
                Mundo[word]++;
            }
            else{
                temporalTF[word]++;
            }
        }    
        
        foreach (string word in temporalTF.Keys)//calcular tf d cada palabra
        {
            temporalTF[word] /= texto.Length;  
        }

        Mundito.Add(doc, temporalTF);                    
    }

        foreach (string item in Mundo.Keys)//calcular idf
        {
            Mundo[item] = (float)Math.Log10(contenido.Length/Mundo[item]);
        }
        foreach (Dictionary<string, float> dicc in Mundito.Values)//calcula el tf-idf de cada palabra
        {
            foreach (string item in dicc.Keys)
            {
                dicc[item] *= Mundo[item];
            }
        }
    }

    public static string[] normalizar(string texto)// meteodo de normalizacion de texto
    {
        string normalizado=Regex.Replace(texto.ToLower(),@"[^a-z0-9\s]+"," ");//esto vuelve las mayusculas a minusculas y limpia el texto
        return normalizado.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
}
