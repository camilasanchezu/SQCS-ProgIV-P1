﻿using System.Text.Json;
using ProyectoMultas.Models;

namespace ProyectoMultas.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _client;

    public ApiService(IConfiguration configuration)
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri(configuration["Api:Url"])
        };
    }

    public async Task<Profesor?> IniciarSesion(ProfesorLoginDto? profesor)
    {
        var response = await _client.PostAsJsonAsync("/api/Profesor/inicio", profesor);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Profesor>();
        }
        
        return null;
    }

    public async Task<Profesor?> Registrar(Profesor? profesor)
    {
        var response = await _client.PostAsJsonAsync("/api/Profesor/registro", profesor);

        if (response.IsSuccessStatusCode)
        {
            profesor = await response.Content.ReadFromJsonAsync<Profesor>();

            return profesor;
        }

        return null;
    }

    public async Task<List<Ayudante>?> ObtenerAyudantes()
    {
        var response = await _client.GetFromJsonAsync<List<Ayudante>>("api/Ayudante");

        if (response != null)
        {
            return response;
        }

        return null;
    }

    public async Task<List<Multa>?> ObtenerMultasPorId(string idBanner)
    {
        var response = await _client.GetFromJsonAsync<List<Multa>>($"api/Multa/{idBanner}");

        if (response != null)
        {
            return response;
        }

        return null;
    }

    public async Task<Profesor?> ActualizarProfesor(string idBanner, Profesor? profesor)
    {
        var response = await _client.PutAsJsonAsync($"api/Profesor/{idBanner}", profesor);
        
        profesor = await response.Content.ReadFromJsonAsync<Profesor>();
        
        if (profesor != null) return profesor;
        
        return null;
    }

    public async Task EliminarProfesor(string idBanner)
    {
        await _client.DeleteAsync($"api/Profesor/{idBanner}");
    }
}