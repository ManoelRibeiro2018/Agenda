using CrudDapper.Entites;
using CrudDapper.Persistence;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapper.Controllers
{
    [Route("api/taferas")]
    public class TaferaController: ControllerBase
    {
        private readonly TaferaContext _taferaContext;
        private readonly string _connectionString;
        public TaferaController(TaferaContext taferaContext, IConfiguration configuration)
        {
            _taferaContext = taferaContext;
            // Se tentar pegar a conexão pelo dbContext e utilizar o banco em memória. Vai dar erro!
            //Exemplo : _connectionString = taferaContext.Database.GetDbConnection().ConnectionString;
            _connectionString = configuration.GetConnectionString("Conexao");
        }

        [HttpGet]
        public IActionResult Get()
        {
          
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "select * from Taferas";

                var tarefas = sqlConnection.Query<Tafera>(query);

                return Ok(tarefas);
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "Select * from Taferas where id = @id";
                var tafera = sqlConnection.Query<Tafera>(query, new {id});
                return Ok(tafera);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tafera model)
        {
            
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "insert into Taferas(titulo, descricao, dataPrazo) values(@titulo, @descricao, @dataPrazo)";
                sqlConnection.Execute(query, new { titulo = model.Titulo,descricao = model.Descricao, dataPrazo = model.DataPrazo });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.Id },
                model
                );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tafera model)
        {
            var tafera = GetById(id);

            if (tafera == null)
            {
                return NotFound();
            }

            using(var sqlconnection = new SqlConnection(_connectionString))
            {
                var query = "update set Tarefas titulo = @titulo, descricao = @descricao, dataPrazo = @dataPrazo where id = @id";
                sqlconnection.Execute(query, new {titulo = model.Titulo, descricao = model.Descricao, dataPrazo = model.DataPrazo, id = model.Id});
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarefa = GetById(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "delete from tarefas where id = @id";
                sqlConnection.Execute(query, new {id});
            }

            return NoContent();
        }

    }


}
