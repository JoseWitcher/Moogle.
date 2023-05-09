namespace MoogleEngine;
using System.Text.RegularExpressions;


public class Query

{
    private Procesamiento cosasDelMundo;

    public Dictionary<string,float>query{get; private set;}//este diccionario me va a guardar las palabras del query con su tf idf

    public Query(string query1)//construtor
    {
        cosasDelMundo = new Procesamiento();//llama a la clase procesamiento
        string[] queryNormalizada = Procesamiento.normalizar(query1);//me normaliza la query
        query = obtenerVector(queryNormalizada);//me llena el diccionario query 
    }


    private Dictionary<string, float> obtenerVector( string[] palabras )//diccionario para obtener el vector query
    {
        Dictionary<string,float> vector = new Dictionary<string, float>();

        foreach (string palabra in palabras)
        {   
            if(cosasDelMundo.Mundo.ContainsKey(palabra))
            {
                vector.Add(palabra, cosasDelMundo.Mundo[palabra]);
            }
        }

        return vector;
    } 
}