using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
			this.CadastrarCliente = new Command(() =>
			{
				var clienteNovo = new Cliente()
				{
					Nome = this.NomeCliente,
					Endereco = this.Endereco,
					Filhos = this.Filhos,
					DataNascimento = this.DataNascimento
				};

				this.Clientes.Add(clienteNovo);

				this.NomeCliente = this.Endereco = string.Empty;
				this.DataNascimento = new DateTime();
				this.Filhos = 0;
			});
		}
	}
}
