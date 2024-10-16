﻿using DsiVendas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfVendas.ViewModels
{
    public class ClienteViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Cliente> Clientes { get; set; }

        public ClienteViewModel()
        {
            _httpClient = new HttpClient();
            Clientes = new ObservableCollection<Cliente>();
            CarregarClientesDaAPI();
        }

        private async Task CarregarClientesDaAPI()
        {
            try
            {
                // URL da sua API MVC para listar os clientes
                var apiUrl = "https://sua-api-endereco/api/clientes";

                // Fazendo a requisição GET para buscar os clientes
                var clientesDaApi = await _httpClient.GetFromJsonAsync<Cliente[]>(apiUrl);

                if (clientesDaApi != null)
                {
                    foreach (var cliente in clientesDaApi)
                    {
                        Clientes.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar erros de conexão ou resposta da API
                Console.WriteLine($"Erro ao buscar clientes: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
