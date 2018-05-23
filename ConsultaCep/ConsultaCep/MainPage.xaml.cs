using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsultaCep.Service.Model;
using ConsultaCep.Service;

namespace ConsultaCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnBusca.Clicked += BuscaCep;
        }

        private void BuscaCep(object sender, EventArgs args)
        {
            string cep = txtCep.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    
                    Endereco endereco = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (endereco != null)
                    {
                        lblResultado.Text = string.Format("Endereço: {2}, {3} ,{0}, {1} ", endereco.localidade, endereco.uf, endereco.logradouro, endereco.bairro);
                    }else{
                        DisplayAlert("Erro","O Endereço não foi encontrado para o CEP informado " + cep,"OK");
                    }

                }catch(Exception e)
                {
                    DisplayAlert("Erro Crítico",e.Message , "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            //bool valido = true;
            if(cep.Length != 8)
            {
                DisplayAlert("Erro","CEP inválido! O CEP deve conter 8 caracteres","OK");
                return false;
            }
            int novoCep = 0;
            if(!int.TryParse(cep,out novoCep))
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve composto apenas por números", "OK");
                return false;
            }

            return true;
        }
    }
}
