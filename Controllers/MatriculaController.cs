using AppSerWebParcial3.Clases;
using AppSerWebParcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppSerWebParcial3.Controllers
{
    [RoutePrefix("api/Matricula")]
    //[Authorize]
    public class MatriculaController : ApiController
    {
        [HttpPost]
        [Route("InsertarMatricula")]
        public string InsertarMatricula([FromBody] Matricula matricula)
        {
            ClsMatricula _matricula = new ClsMatricula();
            _matricula.matricula = matricula;
            return _matricula.Insertar();
        }

        [HttpGet]
        [Route("ConsultarMatricula")]
        public List<Object> ConsultarMatricula(string documento, string semestre)
        {
            ClsMatricula _matricula = new ClsMatricula();
            return _matricula.ConsultarPorSD(documento, semestre);
        }

        [HttpPut]
        [Route("ActualizarMatricula")]
        public string ActualizarMatricula([FromBody] Matricula matricula)
        {
            ClsMatricula _matricula = new ClsMatricula();
            _matricula.matricula = matricula;
            return _matricula.Actualizar();
        }

        [HttpDelete]
        [Route("EliminarMatricula")]
        public string EliminarMatricula(int idMatricula)
        {
            ClsMatricula _matricula = new ClsMatricula();
            return _matricula.Eliminar(idMatricula);
        }
    }
}