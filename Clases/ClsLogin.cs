using AppSerWebParcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AppSerWebParcial3.Models.LibLogin;

namespace AppSerWebParcial3.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        public DBExamenEntities dbExamen = new DBExamenEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
                Estudiante estudiante = dbExamen.Estudiante.FirstOrDefault(e => e.Usuario == login.Usuario);
                if (estudiante == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                Estudiante estudiante = dbExamen.Estudiante.FirstOrDefault(e => e.Usuario == login.Usuario && e.Clave == login.Clave);
                if (estudiante == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Contraseña incorrecta";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                Estudiante estudiante = dbExamen.Estudiante.FirstOrDefault(e => e.Usuario == login.Usuario && e.Clave == login.Clave);
                bool estaMatriculado = dbExamen.Matricula.Any(m => m.idEstudiante == estudiante.idEstudiante);
                if (estaMatriculado)
                {
                    string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                    return from E in dbExamen.Set<Estudiante>()
                           join M in dbExamen.Set<Matricula>() on E.idEstudiante equals M.idEstudiante
                           where E.Usuario == login.Usuario && E.Clave == login.Clave
                           select new LoginRespuesta
                           {
                               Usuario = E.Usuario,
                               Documento = E.Documento,
                               Autenticado = true,
                               Token = token,
                               Mensaje = "Ingreso exitoso"
                           };
                }
            }
            List<LoginRespuesta> List = new List<LoginRespuesta>();
            List.Add(loginRespuesta);
            return List.AsQueryable();


        }
    }
}
