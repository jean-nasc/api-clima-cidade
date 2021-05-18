using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Clima.Cidade.Api.Data;
using Clima.Cidade.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Clima.Cidade.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClimaController : ControllerBase
    {
        private readonly MeuDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client = new HttpClient();

        public ClimaController(MeuDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        /// <summary>
        /// Buscar as temperaturas atual, mínima e máxima de uma cidade passada na url.
        /// </summary>
        /// <remarks>
        /// Não é possível retornar as temperaturas sem informar a cidade.
        /// </remarks>
        /// <param name="cidade"> Recebe a cidade da qual deseja saber a temperatura. </param>
        /// <response code="200"> Retorna a cidade e suas temperaturas. </response>

        [HttpGet("{cidade}")]
        public async Task<ActionResult<ClimaCidade>> ObterClimaPorCidade(string cidade)
        {
            var query = GetCidadeDb(cidade);
            TimeSpan diferenca = new TimeSpan();

            foreach (var item in query)
            {
                var dataBanco = item.DataRegistro;
                var dataAtual = DateTime.UtcNow;
                diferenca = dataBanco.AddMinutes(20) - dataAtual;
            }

            if (query.Count() == 0)
            {
                var objetoClima = await MontaObjetoClima(cidade);

                _context.ClimaCidades.Add(objetoClima);
                _context.SaveChanges();

                return Ok(objetoClima);
            }

            if (diferenca.TotalSeconds < 0)
            {
                var objetoClima = await MontaObjetoClima(cidade);

                ClimaCidade atualizaTemperatura = _context.ClimaCidades.First(c => c.Cidade == cidade);
                atualizaTemperatura.Atualizar(objetoClima.TemperaturaAtual, objetoClima.TemperaturaMin, objetoClima.TemperaturaMax);
                _context.SaveChanges();

                return Ok(objetoClima);
            }

            return Ok(query);
        }

        [NonAction]
        public IQueryable<ClimaCidade> GetCidadeDb(string cidade)
        {
            IQueryable<ClimaCidade> query =
                                    from clima in _context.ClimaCidades
                                    where clima.Cidade == cidade
                                    select clima;

            return query;
        }


        [NonAction]
        public async Task<HttpResponseMessage> GetApi(string cidade)
        {
            var path = ($"http://api.openweathermap.org/data/2.5/weather?q={cidade}&APPID={_configuration.GetValue<string>("APPID")}");

            var response = await _client.GetAsync(path);

            return response;
        }


        [NonAction]
        public async Task<ClimaCidade> MontaObjetoClima(string cidade)
        {
            var response = await GetApi(cidade);
            ClimaCidade objetoClima = null;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                Root objetoApi = JsonConvert.DeserializeObject<Root>(result);

                objetoClima = new ClimaCidade(
                    cidade: objetoApi.name,
                    temperaturaAtual: objetoApi.main.temp,
                    temperaturaMin: objetoApi.main.temp_min,
                    temperaturaMax: objetoApi.main.temp_max
                );
            }

            return objetoClima;
        }
    }

}