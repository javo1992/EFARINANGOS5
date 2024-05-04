using EFARINANGOS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFARINANGOS5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }


        private void Init()
        {
            if (conn is not null)
                return;
            conn = new (_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;            
        }

        public void AddNewPersona(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");
                Persona persona = new (){ Name = nombre };
                result = conn.Insert(persona);

                StatusMessage = string.Format("Nombre ingresado",result,nombre);

            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Nombre no ingresado", result, ex.Message);
            }
        }
        public List<Persona> GetAllPersona()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("No se puedo listar", ex.Message);
                throw;
            }
            return new List<Persona>();
        }

        public Persona GetPersona(int Id)
        {
            try
            {
                Init();
                return conn.Table<Persona>().FirstOrDefault(p => p.Id == Id);
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("No se pudo encontrar a la persona", ex.Message);
                throw;
            }
            return new Persona();
        }

        public void UpdatePerson(Persona UPdpersona)
        {
            try
            {
                Init();
                var persona = conn.Table<Persona>().FirstOrDefault(p => p.Id == UPdpersona.Id);
                if (persona != null)
                {
                    persona.Name = UPdpersona.Name;
                    conn.Update(persona);
                    StatusMessage = string.Format("Persona actualizada");
                }
                else
                {
                    StatusMessage = string.Format("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona: {0}", ex.Message);
                throw;
            }
        }

        public void DeletePerson(int Id)
        {
            try
            {
                Init();
                var persona = conn.Table<Persona>().FirstOrDefault(p => p.Id == Id);
                if (persona != null)
                {

                    conn.Delete(persona);
                    StatusMessage = string.Format("Persona Eliminada");
                }
                else
                {
                    StatusMessage = string.Format("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona: {0}", ex.Message);
                throw;
            }

        }

    }
}
