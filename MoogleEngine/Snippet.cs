namespace MoogleEngine;
using System.Text.RegularExpressions;




public class Snippet
{

    public Dictionary<string, string> SnippetDocument{get; private set;}

    public Snippet(Dictionary<string, float> query, Dictionary<string , float> Score)
    {
        this.SnippetDocument = new Dictionary<string, string>();
        Main(query, Score);     
    }

    private void Main(Dictionary<string, float> query, Dictionary<string, float> Score){
        
        foreach (string doc in Score.Keys)
        {
            string result = "";
            string docSinNormalizar = File.ReadAllText(doc);
            string[] docNormalizado = Procesamiento.normalizar(docSinNormalizar);
            int position = Posicion(queryMayor(query), docNormalizado);
            string[] docArray = docSinNormalizar.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = position; i < position + 25; i++)
            {
                if(position+25 >= docArray.Length - 1) break;
                result += docArray[i] + " ";
            }
            this.SnippetDocument.Add(doc, result);
        }
    }
    public string queryMayor(Dictionary<string, float> query)//sacar la mayor palabra con tf de la query
    {
        string initialword = query.Keys.ElementAt(0);

        foreach (string palabra in query.Keys)
        {
            if (query[initialword] < query[palabra])
            {
                initialword = palabra;
            }

        }

        return initialword;
    }

    public int Posicion(string mayorquery, string[] todoDocumento)
    {
        
        int position = 0;
        for (int i = 0; i < todoDocumento.Length; i++)
        {
            if (todoDocumento[i] == mayorquery)
            {
                position = i;
                break;
            }
        }
        return position;

    }










}
