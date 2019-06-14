using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json; 



namespace App
{
	public class CadastroDeClientesVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		private string nomeCliente;
		public string NomeCliente
		{
			get
			{
				return nomeCliente;
			}
			set
			{
				nomeCliente = value;
				OnPropertyChanged("NomeCliente");
			}
		}

		private string endereco;
		public string Endereco
		{
			get => endereco;
			set
			{
				endereco = value;
				OnPropertyChanged("Endereco");
			}
		}

		private int filhos;
		public int Filhos
		{
			get => filhos;
			set
			{
				filhos = value;
				OnPropertyChanged("Filhos");
			}
		}

		private DateTime dataNascimento;
		public DateTime DataNascimento
		{
			get => dataNascimento;
			set
			{
				dataNascimento = value;
				OnPropertyChanged("DataNascimento");
			}
		}

		public ObservableCollection<Cliente> Clientes { get; set; } = new ObservableCollection<Cliente>();

		public ICommand CadastrarCliente { get; set; }

		public CadastroDeClientesVM()
		{
			this.CadastrarCliente = new Command(async () =>
			{
				var clienteNovo = new Cliente()
				{
					Nome = this.NomeCliente,
					Endereco = this.Endereco,
					Filhos = this.Filhos,
					DataNascimento = this.DataNascimento
				};

				await CadastrarClienteNaAPIAsync(clienteNovo);
				this.Clientes.Add(clienteNovo);

				this.NomeCliente = this.Endereco = string.Empty;
				this.DataNascimento = new DateTime();
				this.Filhos = 0;
			});
		}

		public async System.Threading.Tasks.Task CadastrarClienteNaAPIAsync(Cliente clienteNovo)
		{
			using (var c = new HttpClient())
			{
				var jsonRequest = new
				{
					nome = clienteNovo.Nome,
					filhos = clienteNovo.Filhos,
					endereco = clienteNovo.Endereco,
					dataNascimento = clienteNovo.DataNascimento.ToString("yyyy-MM-dd")
				};

				var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
				HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

				var response = await c.PostAsync(new Uri("https://first-app-senac.herokuapp.com/v1/clientes"), content);
				if (response.IsSuccessStatusCode)
				{
					string respostaDaAPI = response.Content.ReadAsStringAsync().Result;

					var result = (Cliente)JsonConvert.DeserializeObject(
						respostaDaAPI,
						clienteNovo.GetType());

					clienteNovo.ID = result.ID;
				}
			}
		}
	}
}
