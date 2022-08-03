using FireSharp.Response;
using System;
using System.Collections.Generic;


/*
 Autor: Waldir Tiago Dias
 Data: 29-01-2022
 
 Classe respponsável por administrar o listening que monitora o firebase a 
 espera que o app confere estoque salve as leituras
 */


namespace Monitor_Confere_Estoque.controlers
{
    public class AdmStreamResponse
    {

        static Dictionary<string, EventStreamResponse> listeningResponses = new Dictionary<string, EventStreamResponse>();


        public static void adicionarListening(string chavePublica, EventStreamResponse response)
        {
            try
            {
                if (!chavePublica.Equals(string.Empty))
                {
                    listeningResponses.Add(chavePublica, response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

            
        public static void removeResponseListening(string chavePublica)
        {
            try
            {
                if (!chavePublica.Equals(string.Empty))
                {                    
                    listeningResponses.Remove(chavePublica);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static void removeTodosListenings()
        {
            try
            {
                if (listeningResponses != null)
                {
                    foreach (var item in listeningResponses)
                    {
                        item.Value.Dispose();
                    }
                    listeningResponses.Clear();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
