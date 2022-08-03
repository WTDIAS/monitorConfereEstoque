using System;
using System.IO;

/*
 Autor: Waldir Tiago Dias
 Data: 16-01-2022
 
 Classe respponsável por encriptar os dados da configuração inicial 
 (caminho chave .txt e chave pública)
 */



namespace Monitor_Confere_Estoque.controlers
{
    
    static class EncryptDecrypt
    {
        //Chaves necessarias para encriptar e decriptar os dados
        private static string publicKey = @$"{Form1.nomeEcaminhoArquivoCofiguracaoInicial}public.key";
        private static string privateKey = @$"{Form1.nomeEcaminhoArquivoCofiguracaoInicial}private.key";

        //Criptografa o conteudo do texto
        public static string encriptar(string texto)
        {
            return ExpressEncription.RSAEncription.EncryptString(texto, publicKey);
        }

        //Descriptografa o conteúdo do texto
        public static string decriptar(string texto)
        {
            return ExpressEncription.RSAEncription.DecryptString(texto, privateKey);
        }

        //Gera as chaves 
        public static void gerarChavesEncrytDecript()
        {
            if (!Directory.Exists(@$"{Form1.nomeEcaminhoArquivoCofiguracaoInicial}"))
            {
                DirectoryInfo dir = Directory.CreateDirectory(@$"{Form1.nomeEcaminhoArquivoCofiguracaoInicial}");
            }
            if (!File.Exists(publicKey))
            {
                ExpressEncription.RSAEncription.MakeKey(publicKey, privateKey);
            }
        }

    }
}
