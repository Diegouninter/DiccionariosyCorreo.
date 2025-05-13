using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DiccionariosyCorreo
{
    internal class Program
    {
        public enum Menu 
        { 
            Agregar=1,Consultar,Eliminar,Actualizar,contar, enviarcorreo
        }
        static Dictionary<int,string>AlumnoProgramacionI = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            while (true)
            {
                switch (Men())
                {
                    case Menu.Agregar:
                        AgergarAlumnos();
                        break;
                    case Menu.Consultar:
                        break;
                    case Menu.Eliminar:
                        break;
                    case Menu.Actualizar:
                        Actualizar();
                        break;
                    case Menu.contar:
                            Console.WriteLine($"EL numero de elemnetos es:{Contar()}");
                        break;
                    case Menu.enviarcorreo:
                        Enviarcorreo();
                        break;
                    default:
                        break;
                }
            }
           
        }
       
        static void AgergarAlumnos() 
        {
            Console.WriteLine("Matricula:");
            int matricula = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Nombre:");
            string nombre = Console.ReadLine();
            AlumnoProgramacionI.Add(matricula, nombre);
        }
        static Menu Men()
        {
            Console.WriteLine("1)Agergar");
            Console.WriteLine("2)Consultar");
            Console.WriteLine("3)Eliminar");
            Console.WriteLine("4)Actualizar");
            Console.WriteLine("5)Contar");
            Console.WriteLine("6)Enviarcorreo");
            Menu opc = (Menu)Convert.ToInt32(Console.ReadLine());
            return opc; 
        }
        static void Mostrar()
        {
            foreach (var d in AlumnoProgramacionI)
            {
                Console.WriteLine($"Matricula{d.Key}:");
                Console.WriteLine($"Nombre{d.Value}:");
            }
           
        }
        static void Eliminar() 
        {
            Console.WriteLine("Dame la matricula a eliminar:");
            int Matricula = Convert.ToInt32(Console.ReadLine());
            if (AlumnoProgramacionI.ContainsKey(Matricula))
            {
                AlumnoProgramacionI.Remove(Matricula);
                Console.WriteLine("Eliminado");
            }
            else
            {
                Console.WriteLine("No eliminado");
            }
           
        }
        static void Actualizar()
        {
            Console.WriteLine("Matricula del almno a actualizar:");
            int matricula = Convert.ToInt32(Console.ReadLine());
            if (AlumnoProgramacionI.ContainsKey(matricula))
            {
                Console.WriteLine("nuevo nombre: ");
                string nuevonombre = Console.ReadLine();
                AlumnoProgramacionI[matricula] = nuevonombre;
                Console.WriteLine("alumno actualizado con exito");
            }
            else
            {
                Console.WriteLine("matricula no encontrada");
            }
        }
        static double Contar() 
        {
            return AlumnoProgramacionI.Count();
        }
        static void Enviarcorreo()
        {
            string remitente = "113302@alumnouninter.mx";
            string contraseña = "DIEgmed0611";
            string destinatario = "ecorrales@uninter.edu.mx";

            StringBuilder cuerpo = new StringBuilder();
            cuerpo.AppendLine("Lista de alumnos registrados:");
            foreach (var alumno in AlumnoProgramacionI)
            {
                cuerpo.AppendLine($"Matricula:{alumno.Key}, Nombre:{alumno.Value}");
            }

            MailMessage mensaje = new MailMessage(remitente, destinatario, "Lista de alumnos", cuerpo.ToString());

            SmtpClient cliente = new SmtpClient("smtp.office365.com",587)
            {
                Credentials = new NetworkCredential(remitente,contraseña),
                EnableSsl = true
            };
            try
            {
                cliente.Send(mensaje);
                Console.WriteLine("Correo enviado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}
