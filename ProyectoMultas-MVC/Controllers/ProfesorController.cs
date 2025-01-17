﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProyectoMultas.Models;
using ProyectoMultas.Services;

namespace ProyectoMultas.Controllers;

public class ProfesorController : Controller
{
    private readonly IApiService _api;

    public ProfesorController(IApiService api)
    {
        _api = api;
    }

    [HttpPost]
    public IActionResult AbrirPerfil(Profesor profesor)
    {
        return View("Perfil",profesor);
    }

    public IActionResult InicioSesion()
    {
        return View();
    }

    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> InicioSesion(ProfesorLoginDto profesor)
    {
        var profesorRes = await _api.IniciarSesion(profesor);

        if (profesorRes is null) return View();

        return View("Index", profesorRes);
    }

    [HttpPost]
    public async Task<IActionResult> Registro(Profesor profesor)
    {
        var profesorRes = await _api.Registrar(profesor);

        if (profesorRes is null) return View();
        
        return View("Index", profesorRes);
    }

    [HttpPost]
    public async Task<IActionResult> Perfil(Profesor profesor)
    {
        var profesorActualizado = await _api.ActualizarProfesor(profesor.IdBanner, profesor);
        
        if (profesorActualizado is null) RedirectToAction("Error","Home");
        
        return View("Index", profesorActualizado);
    }

    public async Task<IActionResult> EliminarPerfil(string idBanner)
    {
        await _api.EliminarProfesor(idBanner);
        
        return RedirectToAction("InicioSesion");
    }
}