using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Tombolini.Entidades;

namespace Tombolini.Datos
{
    public class Usuario: Base
    {
        public List<Tombolini.Entidades.Usuario> RecuperarTodos() {

            List<Tombolini.Entidades.Usuario> usuarios = new List<Tombolini.Entidades.Usuario>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUSu = new SqlCommand("select * from Usuarios", xxxConnection);
                SqlDataReader drUsu = cmdUSu.ExecuteReader();
                while (drUsu.Read())
                {
                    Entidades.Usuario usu = new Entidades.Usuario();
                    usu.NombreUsuario = (String)drUsu["NombreUsuario"];
                    usu.Clave = (String)drUsu["Clave"];
                    usu.TipoUsuario = (int)drUsu["TipoUsuario"];
                    usu.Email = (String)drUsu["Email"];
                    usu.UltimoIngreso = (DateTime)drUsu["UltimoIngreso"];
                    usuarios.Add(usu);
                }
                drUsu.Close();
                return usuarios;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally 
            {
                this.CloseConnection();
            }           
        }

        public Entidades.Usuario RecuperarUno(String nombreUsu)
        {
            try
            {
                List<Entidades.Usuario> usuarios = this.RecuperarTodos();
                var miUsuario = (from Entidades.Usuario u in usuarios where u.NombreUsuario == nombreUsu select u).First();
                return miUsuario;
            } catch (Exception ex){
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de usuarios", ex);
                throw ExcepcionManejada;
            }
        }

        public void Agregar(Entidades.Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT INTO Usuarios(NombreUsuario,Clave,TipoUsuario,Email,UltimoIngreso) " +
                    "VALUES(@NomberUsuario,@Clave,@TipoUsuario,@Email,@UltimoIngreso)SELECT @@identity ", xxxConnection);
                //esta linea es para recuperar el ID que asignó el sql automaticamente

                cmdSave.Parameters.Add("@NomberUsuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@Clave", SqlDbType.VarChar, 10).Value = usuario.Clave;
                cmdSave.Parameters.Add("@TipoUsuario", SqlDbType.Int).Value = usuario.TipoUsuario;
                cmdSave.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = usuario.Email;
                cmdSave.Parameters.Add("@UltimoIngreso", SqlDbType.DateTime).Value = DateTime.Now;
                cmdSave.ExecuteScalar();//asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear un usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
