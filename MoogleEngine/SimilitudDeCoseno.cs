namespace MoogleEngine;




public class SimilitudDeCoseno
{

    public Dictionary<string, float> Score{get; private set;}

    public SimilitudDeCoseno(Dictionary<string, float> Query, Dictionary<string, Dictionary<string, float>> Mundito)
    {
    
        this.Score = ordenarDiccionarios(obtenerScore(Mundito, Query));
    }

    private Dictionary<string, float> obtenerScore(Dictionary<string, Dictionary<string, float>> Mundito, Dictionary<string, float> Q)
    {
        Dictionary<string, float> resultDoc = new Dictionary<string, float>();        

        float den1 = 0;

        foreach (string word in Q.Keys)//me calcula el vector query y ya me devulve su raiz cuadrada
        {
            den1 += (float)Math.Pow(Q[word], 2);
        }
        den1 = (float)Math.Sqrt(den1);

        foreach (string doc in Mundito.Keys)
        {
            float num = 0;
            float den2 = 0;

            foreach (string texto in Mundito[doc].Keys)//me calcula el vector de todas las palabras y me devuelve el cuadrado
            {
                den2 += (float)Math.Pow(Mundito[doc][texto], 2);
            }
            den2 = (float)Math.Sqrt(den2);

            foreach (string word in Q.Keys)//me calcula el numerador
            {
                if (Mundito[doc].ContainsKey(word))
                {
                    num += Mundito[doc][word] * Q[word];
                }
            }
            float result = num / (den1 * den2);
            resultDoc.Add(doc, result);
        }
        return resultDoc;
    }

    private Dictionary<string, float> ordenarDiccionarios(Dictionary<string, float> Score)//metodo para ordenar diccionarios
    {
        Dictionary<string, float> Ordenado = new Dictionary<string, float>();
        int count = (10 > Score.Keys.Count?Score.Keys.Count:10);
        
        for (int i = 0; i < count; i++)//para ordenar los primeros 10 diccionarios
        {
            string initialDoc = Score.Keys.ElementAt(0);//accede al elemento 0 de cada diccionario

            foreach (string doc in Score.Keys)
            {
                if (Score[initialDoc] < Score[doc])
                {
                    initialDoc = doc;
                }
            }
            Ordenado.Add(initialDoc, Score[initialDoc]);
            Score.Remove(initialDoc);
        }
        return Ordenado;
    }

}




