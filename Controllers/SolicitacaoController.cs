using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApiWithDoc.Entities;
using MyApiWithDoc.MockData;
using MyApiWithDoc.Requests;
using MyApiWithDoc.Responses;
using static Bogus.DataSets.Name;

namespace MyApiWithDoc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SorteioController : ControllerBase
    {
        private readonly ILogger<SorteioController> _logger;
        private static SorteioMock mockSorteio;

        public SorteioController(ILogger<SorteioController> logger)
        {
            _logger = logger;
            if (mockSorteio == null)
            {
                _logger.LogInformation("Creating mock data");
                mockSorteio = new SorteioMock();
            }
        }

        /// <summary>
        /// Sorteio get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<SorteioResponse>> getIndex()
        {
            var response = 1;

            return Ok(response);
        }

        /// <summary>
        /// Get sorteio por id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<SorteioResponse>> getById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var sorteioResponse = FilterById(id);
            if (sorteioResponse == null)
                return NotFound();

            return Ok(sorteioResponse);
        }

        /// <summary>
        /// Adiciona novo sorteio
        /// </summary>
        /// <param name="sorteioRequest">sorteio data</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> Post(
            [FromBody] CreateSorteioRequest sorteioRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var nextId = mockSorteio.Data.Max(c => c.Id) + 1;
            var sorteio = new Sorteio(
                id: nextId,
                name : sorteioRequest.Name,
                descricao : sorteioRequest.Descricao,

                data : sorteioRequest.Data
            );

            mockSorteio.Add(sorteio);

            return CreatedAtAction(nameof(Post), new { Id = sorteio.Id });
        }

        /// <summary>
        /// Atualiza sorteio por id
        /// </summary>
        /// <param name="id">sorteio id</param>
        /// <param name="sorteioRequest">Sorteio data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> Put(
            [FromRoute] int id,
            [FromBody] UpdateSorteioRequest sorteioRequest)
        {
            if (!ModelState.IsValid || id != sorteioRequest.Id)
                return BadRequest();

            var sorteio = mockSorteio.GetById(id);
            if (sorteio == null)
                return NotFound();

            sorteio.Name = sorteioRequest.Name;
            sorteio.Descricao = sorteioRequest.Descricao;
  
            sorteio.Data = sorteioRequest.Data;

            mockSorteio.Update(sorteio);

            return Ok(new { Id = sorteio.Id });
        }

        /// <summary>
        /// Deleta sorteio por id
        /// </summary>
        /// <param name="id">Sorteio id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sorteio = mockSorteio.GetById(id);
            if (sorteio == null)
                return NotFound();

            mockSorteio.Remove(id);

            return NoContent();
        }

        private SorteioResponse FilterById(int id)
        {
            return (from sorteio in mockSorteio.Data
                    where sorteio.Id.Equals(id)
                    select new SorteioResponse(
                        id: sorteio.Id,
                        name: sorteio.Name,
                        descricao: sorteio.Descricao,
                        data: sorteio.Data
                   
                    )).FirstOrDefault();
        }
        private IEnumerable<SorteioResponse> FilterByName(string name)
        {
            return from sorteio in mockSorteio.Data
                   where sorteio.Name.Contains(name)
                   select new SorteioResponse(
                        id: sorteio.Id,
                        name: sorteio.Name,
                        descricao: sorteio.Descricao,
                        data: sorteio.Data
                 
                   );
        }



         private IEnumerable<SorteioResponse> NoFilter()
        {
            return from sorteio in mockSorteio.Data
                   select new SorteioResponse(
                    id: sorteio.Id,
                    name: sorteio.Name,
                    descricao: sorteio.Descricao,
                    data: sorteio.Data
                 
                   );
        }
    }
}