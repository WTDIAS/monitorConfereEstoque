using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;
using System.Diagnostics;
using System.IO;
using Monitor_Confere_Estoque.controlers;



/*
 Autor: Waldir Tiago Dias
 Data: 29-01-2022
 
 Software responsável por monitorar a pasta backup no Firebase que é gerada e alimentada
 pelo APP android Confere Estoque que após coleta de codigos de barras e quantidades
 permite usuário clicar em backup carregando os dados para Firebase. Automaticamente 
 este monitor faz a leitura dos dados gerando um arquivo .txt na pasta escolhida no 
 windows e apaga a pasta backup do Firebase.

 */



namespace Monitor_Confere_Estoque
{
    public partial class Form1 : Form
    {
        public static string nomeEcaminhoArquivoCofiguracaoInicial = @"C:\MCE-TEMP\";
        //Nome do arquivo txt contendo as chaves cadastradas
        private const string nomeArquivoTxtQueContemChavesCadastradas = "chavesCadastradas.txt";
        //Nome do arquivo txt contendo caminho escolhido para salvar os arquivos de leituras recebidos
        private const string nomeArquivoTxtQuecontemCaminhoSalvarLeituras = "caminhoPastaLeituras.txt";
        // Senha liberar edição dos campos
        private const string senhaAlterarConfig = "MONITOR32152";
        private static string chavePublica { get; set; }
        private static string firebaseTabelaLeituras { get; set; }
        private static string firebaseTabelaAtualizapc { get; set; }
        private static string firebaseTabelaNomeUsuario { get; set; }
        private static string nomeUsuario { get; set; }
        private static string caminhoWindowsGerarTxt { get; set; }
        //Nome arquivo .txt com as leituras (data, hora, min, seg)
        private static string nomeArquivoTxt { get; set; }


       


        FirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "Rms1Fvf9nB9hQWeHEoaVj5s0cXHw3AHdLtl4f8Ta",
            BasePath = "https://conferestoque.firebaseio.com/"
        };

        FirebaseClient client;



        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
                client = new FirebaseClient(config); 

                if (client == null)
                {
                    adicionarNotificacaoAoListbox("Não foi possível estabelecer conexão...(cliente == null) " + DateTime.Now.ToString());
                }
                else
                {
                    atualizaFormEiniciaListenings();
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("private void Form1_Load: " + ex.Message + " " + DateTime.Now.ToString());
            }

        }
             
        

        private async void atualizaFormEiniciaListenings()
        {
            try
            {
                //Pegando o nome do caminho para salvar os arquivos de leituras
                string[] caminho = leituraConfigInicial(nomeArquivoTxtQuecontemCaminhoSalvarLeituras);
                if (caminho != null)
                {
                    //colocando no textbox caminho arquivos txt o que foi salvo
                    textBoxCaminhoTxt.Text = caminho[0];
                    //Remove todos listenings caso ja exista
                    AdmStreamResponse.removeTodosListenings();
                    //pegando as chaves cadastradas salvas no arquivo txt
                    string[] chaves = leituraConfigInicial(nomeArquivoTxtQueContemChavesCadastradas);
                    if (chaves != null)
                    {
                        dataGridViewChaves.Rows.Clear();
                        foreach (var item in chaves)
                        {
                            if (!item.Equals(string.Empty))
                            {
                                //A variavel atualizado não é usada pois serve apenas para que o metodo espere o retorno antes de proceguir
                                bool atualizado = await atualizaVariaveis(item);                                
                                EventStreamResponse retorno = await monitorandoPastaAtualizapcFirebase();
                                AdmStreamResponse.adicionarListening(item,retorno);
                                dataGridViewChaves.Rows.Add(item,nomeUsuario == "null" ? "" : nomeUsuario);
                            }

                        }
                    }
                    else
                    {
                        adicionarNotificacaoAoListbox("Não encontrado configuração salva com chave publica. " + DateTime.Now.ToString());
                    }
                }
                else
                {
                    adicionarNotificacaoAoListbox("Não encontrado configuração inicial salva com caminho para salvar arquivos. " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("private void atualizaForm1IniciaListenings: " + ex.Message + " " + DateTime.Now.ToString());
            }

        }




        //Método abaixo monitora(Listening) a pasta atualizapc do Firebase, qualquer alteração
        //para gerar um novo arquivo .txt com os dados da pasta backup/nomeusuario para ser importado pelo software de automação.
        private async ValueTask<EventStreamResponse> monitorandoPastaAtualizapcFirebase()
        {
            EventStreamResponse retorno = null;
            try
            {                
                string chave = "";
                EventStreamResponse response = await client.OnAsync(firebaseTabelaAtualizapc,
                added: (sender, args, context) =>
                {
                },
                changed: async (s, args, context) =>
                {
                    //****ESTA AÇÃO SÓ EXECUTA QUANDO É ALTERADO O NÓ atualizapc NO FIREBASE E ISSO ACONTECE QUANDO USUÁRIO CLICA EM BACKU NO APP****
                    //A informação do nó monitorado(atualizapc) do firebase é: dataHoraMinSeg + / + chave pública conforme ex: 29012022153718/3942dde8-711b-4f57-8f88-f0aa396f0b55
                    //abaixo estou separando com split e salvando as informações separadas
                    string[] datetimeChave = args.Data.Split("/");
                    chave = datetimeChave[1].ToString();
                    if (!chave.Equals(string.Empty))
                    {   
                        //A variavel atualizado não é usada pois serve apenas para que o metodo espere o retorno
                        //antes de chamar o tratar dados
                        bool atualizado = await atualizaVariaveis(chave);
                        tratarDados();
                    }
                    
                },
                removed: (s, args, context) =>
                {
                });

                retorno = response;
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("monitorandoPastaAtualizapcFirebase: " + ex.Message + " " + DateTime.Now.ToString());
            }
            return retorno;
        }



        private async ValueTask<bool> atualizaVariaveis(string chavePublicaArgumento)
        {
            bool retorno = false;
            try
            {
                chavePublica = chavePublicaArgumento;
                //Nó do firebase a qual é monitorado por alterações para iniciar a leitura
                firebaseTabelaAtualizapc = $"usuarios/{chavePublicaArgumento}/atualizapc";
                //Local onde será salvo as leituras obtidas
                caminhoWindowsGerarTxt = $@"{textBoxCaminhoTxt.Text}/";
                //Local, nó no firebase onde tem o nome do usuário
                firebaseTabelaNomeUsuario = $"usuarios/{chavePublicaArgumento}/nomeUsuario";
                //Obtendo o nome do usuário atualizado no firebase
                nomeUsuario = await AcoesFirebase.leituraNomeUsuarioFirebase(client, firebaseTabelaNomeUsuario);
                //Local, nó no firebase de onde será obtido as leituras salvas pelo App
                firebaseTabelaLeituras = $"usuarios/{chavePublicaArgumento}/backup/";
                retorno = true;
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("atualizaVariaveis: " + ex.Message + " " + DateTime.Now.ToString());
            }
            return retorno;
        }


        //O metodo abaixo faz a leitura dos dados coletados pelo app no firebase
        //em seguida gera arquivo txt na pasta do windows
        //confirma se gerou com sucesso em seguida apaga as leituras do firebase
        private async void tratarDados()
        {
            try
            {
                //Obtendo leituras salvas no firebase
                Leitura[] leituras = await controlers.AcoesFirebase.leituraFirebase(client, nomeUsuario, firebaseTabelaLeituras);
                if (leituras != null && leituras.Length > 0)
                {
                    bool arquivosGerados = await geraTxtLeiturasNoWindows(leituras);
                    if (arquivosGerados)
                    {
                        //Apagando a pasta backup do firebase
                        FirebaseResponse respApaga = await AcoesFirebase.apagaTabelaFirebase(client, firebaseTabelaLeituras + nomeUsuario);
                        if (respApaga.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            adicionarNotificacaoAoListbox($"Erro ao apagar as leituras do arquivo {nomeArquivoTxt}. " + DateTime.Now.ToString());
                            File.Delete(caminhoWindowsGerarTxt + nomeArquivoTxt);
                        }
                        else
                        {
                            //Exibe notificação windows
                            exibirNotificacaoArquivoRecebido(nomeUsuario + "-" + nomeArquivoTxt);
                            //Adiciona notificação no log
                            adicionarNotificacaoAoListbox($"Recebido arquivo: {nomeUsuario}-{nomeArquivoTxt} salvo em: {caminhoWindowsGerarTxt}");
                            //Atualiza nome usuario no datagridview
                            alteraNomeUsuarioDatagrid();
                        }
                    }
                    else
                    {
                        adicionarNotificacaoAoListbox($"Erro ao gerar arquivo .txt com as leituras em: {caminhoWindowsGerarTxt}. " + DateTime.Now.ToString());
                    }
                }
                else
                {
                    adicionarNotificacaoAoListbox("Não encontrado dados para importação." + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("tratarDados: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }
             

        //Gera arquivo txt baseado na leitura dos dados no firebase e salva na pasta do windows,
        //após gerar arquivo confirma o mesmo existe como forma de ter certeza que foi gerado
        private async ValueTask<bool> geraTxtLeiturasNoWindows(Leitura[] leituras)
        {
            bool retorno = false;
            //Pegando a data atual removendo espaço em branco, dois pontos e barras para gerar nome do arquivo
            nomeArquivoTxt = DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + ".txt";

            try
            {
                //Criando o diretório para salvar o txt se não existir
                if (!Directory.Exists(caminhoWindowsGerarTxt))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(caminhoWindowsGerarTxt);
                }

                foreach (var leitura in leituras)
                {
                    await System.IO.File.AppendAllTextAsync(caminhoWindowsGerarTxt + nomeUsuario + "-" + nomeArquivoTxt, leitura.codigoBarras + ";" + leitura.quantidade + Environment.NewLine);
                }
                //Após salvar arquivo verifico se ele realmente existe na pasta
                if (File.Exists(caminhoWindowsGerarTxt + nomeUsuario + "-" + nomeArquivoTxt))
                {
                    retorno = true;
                }
            }
            catch (Exception e)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    adicionarNotificacaoAoListbox("gerarArquivoTxt: " + e.Message + " " + DateTime.Now.ToString());
                });
            }

            return retorno;
        }


        //Gera arquivo txt com as chaves chaves publicas cadastradas
        //O arquivo estará em: F:\COBIAN\Monitor Confere Estoque\bin\Debug\netcoreapp3.1   
        //Se não existir ele cria
        private void SalvaConfigInicial(string nomeArquivo, string conteudo)
        {
            try
            {
                //Gerando as chaves para encriptar e decriptar o conteúdo
                EncryptDecrypt.gerarChavesEncrytDecript();
                //Encriptando conteúdo antes de salvar
                string conteudoEncriptado = EncryptDecrypt.encriptar(conteudo);
                //Gravando o conteúdo no disco                
                if (!Directory.Exists(nomeEcaminhoArquivoCofiguracaoInicial))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(nomeEcaminhoArquivoCofiguracaoInicial);
                }
                //StreamWriter sw = new StreamWriter(nomeArquivo);
                StreamWriter sw = File.CreateText($@"{nomeEcaminhoArquivoCofiguracaoInicial}{nomeArquivo}");
                sw.WriteLine(conteudoEncriptado);
                sw.Close();

            }
            catch (Exception e)
            {
                adicionarNotificacaoAoListbox("SalvaConfigInicial: " + e.Message + " " + DateTime.Now.ToString());
            }
        }



        //Ler arquivo .TXT com dados de configuração inicial: Chave pública e local para salvar o
        //arquivo .TXT com as leituras (codbarras e quantidade)
        private string[] leituraConfigInicial(string nomeArquivo)
        {
            string[] retorno = null;
            if (File.Exists($@"{nomeEcaminhoArquivoCofiguracaoInicial}{nomeArquivo}"))
            {
                //StreamReader sr = new StreamReader(nomeArquivoConfigInicial);
                using (StreamReader sr = new StreamReader($@"{nomeEcaminhoArquivoCofiguracaoInicial}{nomeArquivo}"))
                {
                    try
                    {
                        string linha = "";
                        string textoEncriptado = "";
                        string textoDecriptado = "";
                        while ((linha = sr.ReadLine()) != null)
                        {
                            textoEncriptado += linha;
                        }

                        textoDecriptado = controlers.EncryptDecrypt.decriptar(textoEncriptado);

                        retorno = textoDecriptado.Split("\n");                       

                    }
                    catch (Exception e)
                    {
                        adicionarNotificacaoAoListbox(" private string[] leituraConfigInicial: " + e.Message + " " + DateTime.Now.ToString());
                    }
                    
                }
            }
            return retorno;
        }




        //Exibe formSenha valida senha para liberação dos campos(Chave pública, caminho arquivo .txt)
        private void btnHabilitaAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                using (formSenha fSenha = new formSenha())
                {
                    if (fSenha.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (fSenha.senhaDigitada.Trim() == string.Empty)
                        {
                            MessageBox.Show("Campo senha é obrigatório!!");
                        }
                        else if (fSenha.senhaDigitada.ToUpper() == senhaAlterarConfig)
                        {
                            textBoxCaminhoTxt.ReadOnly = false;
                            textBoxChavePublica.ReadOnly = false;
                        }
                        else
                        {
                            MessageBox.Show("Senha incorreta!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("btnHabilitaAlterar_Click: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }


        //Exibe notificação no tray proximo ao relogio do windows de recebimento de arquivo
        private void exibirNotificacaoArquivoRecebido(string nomeDoArquivo)
        {
            try
            {
                notifyIcon1.Icon = new Icon(AppContext.BaseDirectory +"CE64X64.ico");
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(3000, "Monitor Confere Estoque", $"Arquivo {nomeDoArquivo} recebido e salvo em: {caminhoWindowsGerarTxt}", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("exibirNotificacaoArquivoRecebido: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }


        //Adiciona na listbox recebimento de arquivo
        public void adicionarNotificacaoAoListbox(string notificacao)
        {
            try
            {
                //trecho abaixo é para solucionar exception de threads
                Invoke((MethodInvoker)delegate
                {
                   listBoxLog.Items.Add(notificacao);
                });
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("adicionarNotificacaoAoListbox: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }


        //Quando minimizar, não mostrar icone na barra de tarefa e mostra icone na tray
        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    //desabilita o campo textbox caminho txt
                    textBoxCaminhoTxt.ReadOnly = true;
                    //desabilita o campo textbox chave publica
                    textBoxChavePublica.ReadOnly = true;

                    this.ShowInTaskbar = false;
                    notifyIcon1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("Form1_Resize: " + ex.Message + " " + DateTime.Now.ToString());
            }

        }

        //Quando der duplo click mostrar a janela e o ícone na barra de tarefas e remover da tray
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Normal;
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.ShowInTaskbar = true;
                    notifyIcon1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("notifyIcon1_MouseDoubleClick: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }


        //Ao clicar no botão reiniciar a aplicação
        private void btnReiniciarAplicacao_Click(object sender, EventArgs e)
        {
            try
            {
                if (exibirCaixaDialogo("Deseja realmente reiniciar a aplicação?", "confirmação"))
                {
                    Process.Start(Application.StartupPath + "Monitor Confere Estoque.exe");
                    Process.GetCurrentProcess().Kill();
                }                
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("btnReiniciarAplicacao_Click: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }


        //Caixa de dialogo (sim, cancela) 
        private bool exibirCaixaDialogo(string mensagem, string textoCaixa)
        {
            bool retorno = false;
            try
            {                
                DialogResult res = MessageBox.Show(mensagem, textoCaixa, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    retorno = true;
                }
                return retorno;
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("exibirCaixaDialogo: " + ex.Message + " " + DateTime.Now.ToString());
            }
            return retorno;
        }

        //Ao clicar no "x" do formulario para fechar pede confirmação
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Verifica se foi é o windows que está sendo desligado/reiniciado e fechou a aplicacao ou o usuário
                if (e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    bool fechar = exibirCaixaDialogo("Deseja realmente sair da aplicação? As leituras de código de barras e quantidade no APP " +
                        "Confere Estoque não serão mais sincronizadas com este computador!", "Confirmação");

                    if (!fechar)
                    {
                        e.Cancel = true;
                    }
                }
                else {
                    e.Cancel = false;
                }
                
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("Form1_FormClosing: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }

        //Função executada ao clicar no botão excluir linha do datagridview
        private void btnExcluirLinhaDatagrid_Click(object sender, EventArgs e)
        {
            try
            {
                bool confirmacao = exibirCaixaDialogo("Confirma a exclusão da chave selecionada?", "Confirma exclusão");
                if (confirmacao)
                {
                    int indiceLinhaSelecionada = -1;
                    indiceLinhaSelecionada = dataGridViewChaves.CurrentCell.RowIndex;
                    string chave = dataGridViewChaves.Rows[indiceLinhaSelecionada].Cells["chavePublicaDataGrid"].Value.ToString();
                    AdmStreamResponse.removeResponseListening(chave);
                    if (indiceLinhaSelecionada > -1)
                    {
                        string chavesTemp = "";
                        //Excluido chave selecionada
                        dataGridViewChaves.Rows.RemoveAt(indiceLinhaSelecionada);
                        //Lendo as demais para salvar e atualizar
                        foreach (DataGridViewRow row in dataGridViewChaves.Rows)
                        {
                            chavesTemp += row.Cells["chavePublicaDataGrid"].Value.ToString() + "\n";
                        }
                        SalvaConfigInicial(nomeArquivoTxtQueContemChavesCadastradas, chavesTemp);
                        atualizaFormEiniciaListenings();
                    }
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("private void btnExcluirLinhaDatagrid_Click: " + ex.Message + " " + DateTime.Now.ToString());
            }
        }

        //Função executada ao clicar no botão salvar cadastro da chave
        private void btnSalvarConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (!textBoxChavePublica.ReadOnly && !textBoxChavePublica.Text.Equals(string.Empty))
                { 
                    if(!(textBoxCaminhoTxt.Text == string.Empty))
                    {
                        //fazendo a leitura das chaves ja cadastradas para depois adicionar a nova chave
                        //em seguida salvar tudo novamente                    
                        string[] arrayDeChaves = leituraConfigInicial(nomeArquivoTxtQueContemChavesCadastradas);
                        string strChaves = "";
                        string novaChave = textBoxChavePublica.Text;
                        if (arrayDeChaves != null)
                        {
                            //Verificando se a chave a ser cadastrada já existe para evitar duplicidade
                            int indexChaveExistente = Array.IndexOf(arrayDeChaves, novaChave);
                            //Se encontrar a chave cadastrada retorna > -1
                            if (!(indexChaveExistente > -1))
                            {
                                foreach (var chave in arrayDeChaves)
                                {
                                    if (!chave.Equals(string.Empty))
                                    {
                                        strChaves += chave + "\n";
                                    }
                                }
                            }
                            else
                            {
                                adicionarNotificacaoAoListbox("Chave já cadastrada " + DateTime.Now.ToString());
                            }
                        }
                        strChaves += novaChave;
                        SalvaConfigInicial(nomeArquivoTxtQueContemChavesCadastradas, strChaves);
                        textBoxChavePublica.Text = string.Empty;
                        atualizaFormEiniciaListenings();
                    }
                    else
                    {
                        adicionarNotificacaoAoListbox("Informe local para salvar .txt. " + DateTime.Now.ToString());

                    }
                    
                }
            }
            catch (Exception ex)
            {
                adicionarNotificacaoAoListbox("btnSalvarConfig_Click: " + ex.Message + " " + DateTime.Now.ToString());
            }

        }



        private void brnSalvarCaminhoTxt_Click(object sender, EventArgs e)
        {            
            if (!textBoxCaminhoTxt.ReadOnly && !textBoxCaminhoTxt.Text.Equals(string.Empty))
            {
                //salvando o caminho escolhido no textbox
                SalvaConfigInicial(nomeArquivoTxtQuecontemCaminhoSalvarLeituras, textBoxCaminhoTxt.Text);
                adicionarNotificacaoAoListbox("Caminho salvo." + " " + DateTime.Now.ToString());
            }   
        }

        //Este método é executado a cada novo arquivo recebido permitindo atualização
        //do nome do usuário no datagridview 
        private void alteraNomeUsuarioDatagrid()
        {
            foreach (DataGridViewRow dtgRow in dataGridViewChaves.Rows)
            {
                if (dtgRow.Cells[0].Value.ToString().Equals(chavePublica))
                {
                    dtgRow.Cells[1].Value = nomeUsuario;
                }
            }
        }


    }   

}
