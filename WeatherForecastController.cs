using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourApiNamespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("AuthGet")]
        [Authorize]
        public IActionResult GetWithAuth()
        {
            return Ok("Auth");
        }

        [HttpGet("OpenGet")]
        public IActionResult GetOpen()
        {
            return Ok("Open");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            // Tarkista tässä credentials-olion arvot, esimerkiksi tietokantahakujen kautta
            if (credentials.Username == "testuser" && credentials.Password == "testpassword")
            {
                // Jos tunnistetiedot ovat oikein, generoi JWT-token ja palauta se
                var tokenService = new TokenService(); // Oletetaan, että TokenService on luokka, joka generoi tokenin
                var token = tokenService.GenerateToken();
                return Ok(new { Token = token });
            }
            else
            {
                // Jos tunnistetiedot ovat väärin, palauta virheilmoitus
                return Unauthorized("Käyttäjätunnus tai salasana on väärin.");
            }
        }
    }
}
