using AppSerWebParcial3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AppSerWebParcial3.Clases
{
    public class ClsMatricula
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public Matricula matricula { get; set; }

        //CONSULTAR POR SEMESTRE Y DOCUMENTO
        public List<object> ConsultarPorSD(string documento, string semestre)
        {
            return (from m in dbExamen.Matricula
                    join e in dbExamen.Estudiante on m.idEstudiante equals e.idEstudiante
                    where e.Documento == documento && m.SemestreMatricula == semestre
                    select new
                    {
                        e.NombreCompleto,
                        e.Documento,
                        m.NumeroCreditos,
                        m.ValorCredito,
                        m.TotalMatricula,
                        m.FechaMatricula,
                        m.SemestreMatricula,
                        m.MateriasMatriculadas
                    }).ToList<object>();
        }

        //CONSULTAR MATRICULA POR ID
        public Matricula ConsultarPorId(int idMatricula)
        {
            return dbExamen.Matricula.FirstOrDefault(m => m.idMatricula == idMatricula);
        }


        //INSERTAR UNA MATRICULA
        public string Insertar()
        {
            try
            {
                Estudiante estudiante = dbExamen.Estudiante.FirstOrDefault(e => e.idEstudiante == matricula.idEstudiante);
                if (estudiante == null)
                {
                    return "El estudiante con ese código no existe";
                }
                matricula.TotalMatricula = matricula.NumeroCreditos * matricula.ValorCredito;
                dbExamen.Matricula.Add(matricula);
                dbExamen.SaveChanges();
                return "Matricula registrada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar la matricula: " + ex.Message;
            }
        }

        //ACTUALIZAR UNA MATRICULA
        public string Actualizar()
        {
            Matricula _matricula = ConsultarPorId(matricula.idMatricula);
            if (_matricula == null)
            {
                return "Esta matricula no existe";
            }
            matricula.TotalMatricula = matricula.NumeroCreditos * matricula.ValorCredito;
            dbExamen.Matricula.AddOrUpdate(matricula);
            dbExamen.SaveChanges();
            return "Matricula actualizada correctamente";
        }

        //ELIMINAR UNA MATRICULA
        public string Eliminar(int idMatricula)
        {
            Matricula _matricula = ConsultarPorId(idMatricula);
            if (_matricula == null)
            {
                return "Esta matricula no existe";
            }
            dbExamen.Matricula.Remove(_matricula);
            dbExamen.SaveChanges();
            return "Matricula eliminada correctamente";
        }
    }
}