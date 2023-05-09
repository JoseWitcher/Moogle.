namespace MoogleEngine;
using System.IO;


public static class Moogle
{
    public static SearchResult Query(string query, Dictionary<string, Dictionary<string, float>> Mundito, Dictionary<string, float> Mundo)
    {
        // Modifique este método para responder a la búsqueda

        Query input = new Query(query);
        SimilitudDeCoseno simlC = new SimilitudDeCoseno(input.query, Mundito);
        Snippet snip = new Snippet(input.query, simlC.Score);

        SearchItem[] items = new SearchItem[snip.SnippetDocument.Count];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SearchItem(simlC.Score.Keys.ElementAt(i).Split(new []{'/','\\'}).Last().Replace(".txt", ""), snip.SnippetDocument.Values.ElementAt(i), simlC.Score.Values.ElementAt(i));

        }




        return new SearchResult(items, query);
    }

}


