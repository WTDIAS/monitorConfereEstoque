using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor_Confere_Estoque
{
    public partial class formSenha : Form
    {
        public formSenha()
        {
            InitializeComponent();
        }

        public string senhaDigitada { get; set; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            
            senhaDigitada = textBoxsSenha.Text.Trim();
            
        }
    }
}
