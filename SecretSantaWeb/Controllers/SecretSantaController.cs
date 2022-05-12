using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecretSanta.BLL.Interfaces;
using SecretSanta.BLL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSantaWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecretSantaController : ControllerBase
    {
        private readonly ISecretSantaServices _secretSantaServices;
        private readonly ILogger<SecretSantaController> _logger;

        public SecretSantaController(ISecretSantaServices secretSantaRepository,
            ILogger<SecretSantaController> logger)
        {
            _secretSantaServices = secretSantaRepository;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPairs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PairViewModel>>> Get()
        {
            try
            {
                var data = await _secretSantaServices.GetAllPairs();

                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PersonCreateModel person)
        {
            if (person == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                await _secretSantaServices.Create(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status201Created, person);
        }
    }
}
