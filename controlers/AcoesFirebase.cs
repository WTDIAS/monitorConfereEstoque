using FireSharp;
using FireSharp.Response;
using System;
using System.Threading.Tasks;

/*
 Autor: Waldir Tiago Dias
 Data: 29-01-2022
 
 Classe respponsável por fazer algumas leituras 
 comuns do firebase 
 */


namespace Monitor_Confere_Estoque.controlers
{
    public class AcoesFirebase
    {
        //Leitura da pasta backup do firebase e retorna um array com os dados
        public static async ValueTask<Leitura[]> leituraFirebase(FirebaseClient client, string nomeUsuario, string caminhoPastaLeituraFirebase)
        {
            //Leitura da pasta backup do firebase
            FirebaseResponse respLeitura = await client.GetAsync(caminhoPastaLeituraFirebase + nomeUsuario);
            //Adicionando o retorno do firebase ao array
            return respLeitura.ResultAs<Leitura[]>();
        }


        public static async ValueTask<string> leituraNomeUsuarioFirebase(FirebaseClient client, string caminhoNomeUsuarioFirebase)
        {
            FirebaseResponse response = await client.GetAsync(caminhoNomeUsuarioFirebase);
            return response.Body.Replace("\"", "").Replace("\\", "");
        }



        public static async ValueTask<FirebaseResponse> apagaTabelaFirebase(FirebaseClient client, string caminhoTabela)
        {
            return await client.DeleteAsync(caminhoTabela);
        }

    }
}
