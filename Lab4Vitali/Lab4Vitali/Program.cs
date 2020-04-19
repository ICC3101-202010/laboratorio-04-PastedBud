using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Vitali
{

    public abstract class Computador
    {
        public int Hora;
        public string Interruptor;
        public bool FinDia;
        public int Memoria;
        public string Nombre;
        


        public string Encendido_Maquina(string maquina)
        {
            Interruptor = "ON";
            return "La Maquina " + maquina + " esta Encendiendose";
        }


        public string Apagado_Maquina(string maquina)
        {
            Interruptor = "OFF";
            Memoria = 0;
            return "La Maquina " + maquina + " esta Apagada";
        }


        public string Reinicio_Maquina(string maquina)
        {
            if (Memoria == 100)
            {
                Memoria = 0;
                Console.WriteLine("Reiniciando Maquina...");
                Interruptor = "ON";
                return "Maquina " + maquina + " Reiniciada";
            }
            else
            {
                string Reinicio_Manual = "Si";
                while (Reinicio_Manual != "Si")
                {
                    Console.WriteLine("Desea Reiniciar la maquina " + maquina + " ? (Si/No) --> ");
                    Reinicio_Manual = Console.ReadLine();
                }
                if (Reinicio_Manual == "Si")
                {
                    Memoria = 0;
                    Console.WriteLine("Reiniciando Maquina...");
                    Interruptor = "ON";
                    return "Maquina " + maquina + " Reiniciada";
                }
                else if (Reinicio_Manual == "No")
                {
                    return " ";
                }
                else
                {
                    return "Opcion Incorrecta, porfavor responder Si o No";

                }
            }
        }
    }

    public class Recepcion : Computador
    {
        public int Piezas_Recibidas;
        public int Piezas_Almacenamiento;

        public void Recibir_Piezas(Recepcion recepcion)
        {
            if ((recepcion.Hora >= 8) && (recepcion.Hora <= 18))
            {
                Console.WriteLine("Piezas listas para empezar procesos");
            }
            else
            {
                recepcion.Apagado_Maquina(recepcion.Nombre);
                Console.WriteLine("Piezas no han podido ser recepcionadas");
            }
        }
        public string Enviar_a_Almacenamiento(Recepcion recepcion)
        {
            if ((recepcion.Hora >= 8) && (recepcion.Hora <= 18))
            {
                if (recepcion.Interruptor == "OFF")
                {
                    recepcion.Encendido_Maquina(recepcion.Nombre);
                }
            }
            else
            {
                if (recepcion.Interruptor == "ON")
                {
                    recepcion.Apagado_Maquina(recepcion.Nombre);
                }
                Console.WriteLine("Horario NO Laboral, Maquina " + recepcion.Nombre + " esta apagada, las piezas no han podido ser recepcionadas por esta Maquina");
            }
            if (recepcion.Interruptor == "ON")
            {
                if (recepcion.Memoria + recepcion.Piezas_Almacenamiento <= 100)
                {
                    recepcion.Piezas_Almacenamiento = recepcion.Piezas_Recibidas;
                    recepcion.Memoria += recepcion.Piezas_Almacenamiento;
                    if (recepcion.Memoria == 100)
                    {
                        recepcion.Reinicio_Maquina(recepcion.Nombre);
                    }
                    Console.WriteLine("Piezas Enviadas a Almacenamiento");
                    return " ";
                }
                else
                {
                    recepcion.Piezas_Almacenamiento = recepcion.Piezas_Recibidas;
                    recepcion.Reinicio_Maquina(recepcion.Nombre);
                    Console.WriteLine("Piezas Enviadas a Almacenamiento");
                    return " ";
                }
            }
            else
            {
                Console.WriteLine("Maquina " + recepcion.Nombre + " esta Apagada, ha sido imposible enviar las piezas a Almacenamiento");
                return " ";
            }
        }
    }


    public class Almacenamiento : Computador
    {
        public int Piezas_Almacenadas;
        public int Piezas_Ensamblaje;
        public int Piezas_Recibidas;

        public void Almacenar_Piezas(Almacenamiento almacenamiento)
        {


            if ((almacenamiento.Hora >= 8) && (almacenamiento.Hora <= 18))
            {
                Console.WriteLine("Piezas listas para empezar almacenamiento");
            }
            else
            {
                if (almacenamiento.Interruptor == "ON")
                {
                    almacenamiento.Apagado_Maquina(almacenamiento.Nombre);
                }

                Console.WriteLine("Horario NO Laboral, Maquina " + almacenamiento.Nombre + " esta apagada, las piezas no han podido ser recepcionadas por esta Maquina");
            }
        }
        public string Enviar_a_Ensamblaje(Almacenamiento almacenamiento)
        {
            if ((almacenamiento.Hora >= 8) && (almacenamiento.Hora <= 18))
            {
                if (almacenamiento.Interruptor == "OFF")
                {
                    almacenamiento.Encendido_Maquina(almacenamiento.Nombre);
                }
            }
            else
            {
                if (almacenamiento.Interruptor == "ON")
                {
                    almacenamiento.Apagado_Maquina(almacenamiento.Nombre);
                }
                Console.WriteLine("Horario NO Laboral, Maquina " + almacenamiento.Nombre + " esta apagada, las piezas no han podido ser Almacenadas por esta Maquina");
            }
            if (almacenamiento.Interruptor == "ON")
            {
                Random rnd = new Random();
                
                if (almacenamiento.Memoria + Piezas_Recibidas <= 100)
                {
                    almacenamiento.Piezas_Almacenadas += almacenamiento.Piezas_Recibidas;
                    almacenamiento.Piezas_Ensamblaje = almacenamiento.Piezas_Almacenadas;
                    almacenamiento.Piezas_Almacenadas -= almacenamiento.Piezas_Ensamblaje;
                    almacenamiento.Memoria += almacenamiento.Piezas_Ensamblaje;
                    if (almacenamiento.Memoria == 100)
                    {
                        almacenamiento.Reinicio_Maquina(almacenamiento.Nombre);
                    }
                    Console.WriteLine("Piezas enviadas a Ensamblaje");
                    return " ";
                }
                else
                {
                    almacenamiento.Piezas_Almacenadas += almacenamiento.Piezas_Recibidas;
                    almacenamiento.Piezas_Ensamblaje = rnd.Next(0, almacenamiento.Piezas_Almacenadas);
                    almacenamiento.Piezas_Almacenadas -= almacenamiento.Piezas_Ensamblaje;
                    almacenamiento.Memoria += almacenamiento.Piezas_Ensamblaje;
                    Console.WriteLine("Piezas enviadas a Ensamblaje");
                    return " ";
                }
            }
            else
            {
                Console.WriteLine("Maquina " + almacenamiento.Nombre + " esta Apagada, ha sido imposible enviar las piezas a Ensamblar");
                return " ";
            }
        }

    }


    public class Ensamblaje : Computador
    {
        public int Piezas_Ensambladas;
        public int Piezas_Verificacion;
        public int Piezas_Recibidas;
        

        public void Verificar_Piezas(Ensamblaje ensamblaje)
        {


            if ((ensamblaje.Hora >= 8) && (ensamblaje.Hora <= 18))
            {
                Console.WriteLine("Piezas listas para empezar Ensamblaje");
            }
            else
            {
                if (ensamblaje.Interruptor == "ON")
                {
                    ensamblaje.Apagado_Maquina(ensamblaje.Nombre);
                }

                Console.WriteLine("Horario NO Laboral, Maquina " + ensamblaje.Nombre + " esta apagada, las piezas no han podido ser recepcionadas por esta Maquina");
            }
        }
        public string Ensamblar_Piezas(Ensamblaje ensamblaje)
        {
            if ((ensamblaje.Hora >= 8) && (ensamblaje.Hora <= 18))
            {
                if (ensamblaje.Interruptor == "OFF")
                {
                    ensamblaje.Encendido_Maquina(ensamblaje.Nombre);
                }
            }
            else
            {
                if (ensamblaje.Interruptor == "ON")
                {
                    ensamblaje.Apagado_Maquina(ensamblaje.Nombre);
                }
                Console.WriteLine("Horario NO Laboral, Maquina " + ensamblaje.Nombre + " esta apagada, las piezas no han podido ser Ensambladas por esta Maquina");
            }
            if (ensamblaje.Interruptor == "ON")
            {
                if (ensamblaje.Memoria + Piezas_Recibidas <= 100)
                {
                    ensamblaje.Piezas_Ensambladas += ensamblaje.Piezas_Recibidas;
                    ensamblaje.Piezas_Verificacion = ensamblaje.Piezas_Ensambladas;
                    ensamblaje.Piezas_Ensambladas -= ensamblaje.Piezas_Verificacion;
                    ensamblaje.Memoria += ensamblaje.Piezas_Verificacion;
                    if (ensamblaje.Memoria == 100)
                    {
                        ensamblaje.Reinicio_Maquina(ensamblaje.Nombre);
                    }
                    Console.WriteLine("Piezas enviadas a Verificacion");
                    return " ";
                }
                else
                {
                    ensamblaje.Piezas_Ensambladas += ensamblaje.Piezas_Recibidas;
                    ensamblaje.Piezas_Verificacion = (100 - ensamblaje.Memoria);
                    ensamblaje.Piezas_Ensambladas -= ensamblaje.Piezas_Verificacion;
                    ensamblaje.Reinicio_Maquina(ensamblaje.Nombre);
                    ensamblaje.Piezas_Verificacion += ensamblaje.Piezas_Ensambladas;
                    ensamblaje.Memoria += ensamblaje.Piezas_Ensambladas;
                    ensamblaje.Piezas_Ensambladas = 0;
                    Console.WriteLine("Piezas enviadas a Verificacion");
                    return " ";
                }
            }
            else
            {
                Console.WriteLine("Maquina " + ensamblaje.Nombre + " esta Apagada, ha sido imposible enviar las piezas a Verificar");
                return " ";
            }
        }
    }
    public class Verificacion: Computador
    {
        public int Piezas_Verificadas;
        public int Piezas_Empaque;
        public int Piezas_Recibidas;


        public void Verificar_Pieza(Verificacion verificacion)
        {
            if ((verificacion.Hora >= 8) && (verificacion.Hora <= 18))
            {
                Console.WriteLine("Piezas listas para empezar Verificacion");
            }
            else
            {
                if (verificacion.Interruptor == "ON")
                {
                    verificacion.Apagado_Maquina(verificacion.Nombre);
                }

                Console.WriteLine("Horario NO Laboral, Maquina " + verificacion.Nombre + " esta apagada, las piezas no han podido ser recepcionadas por esta Maquina");
            }
        }
        public string Enviar_a_Empaque(Verificacion verificacion)
        {
            if ((verificacion.Hora >= 8) && (verificacion.Hora <= 18))
            {
                if (verificacion.Interruptor == "OFF")
                {
                    verificacion.Encendido_Maquina(verificacion.Nombre);
                }
            }
            else
            {
                if (verificacion.Interruptor == "ON")
                {
                    verificacion.Apagado_Maquina(verificacion.Nombre);
                }
                Console.WriteLine("Horario NO Laboral, Maquina " + verificacion.Nombre + " esta apagada, las piezas no han podido ser Verificadas por esta Maquina");
            }
            if (verificacion.Interruptor == "ON")
            {
                if (verificacion.Memoria + Piezas_Recibidas <= 100)
                {
                    verificacion.Piezas_Verificadas += verificacion.Piezas_Recibidas;
                    verificacion.Piezas_Empaque = verificacion.Piezas_Verificadas;
                    verificacion.Piezas_Verificadas -= verificacion.Piezas_Empaque;
                    verificacion.Memoria += verificacion.Piezas_Empaque;
                    if (verificacion.Memoria == 100)
                    {
                        verificacion.Reinicio_Maquina(verificacion.Nombre);
                    }
                    Console.WriteLine("Piezas enviadas a Empaque");
                    return " ";
                }
                else
                {
                    verificacion.Piezas_Verificadas += verificacion.Piezas_Recibidas;
                    verificacion.Piezas_Empaque = (100 - verificacion.Memoria);
                    verificacion.Piezas_Verificadas -= verificacion.Piezas_Empaque;
                    verificacion.Reinicio_Maquina(verificacion.Nombre);
                    verificacion.Piezas_Empaque += verificacion.Piezas_Verificadas;
                    verificacion.Memoria += verificacion.Piezas_Verificadas;
                    verificacion.Piezas_Verificadas = 0;
                    Console.WriteLine("Piezas enviadas a Empaque");
                    return " ";
                }
            }
            else
            {
                Console.WriteLine("Maquina " + verificacion.Nombre + " esta Apagada, ha sido imposible enviar las piezas a Empaquetar");
                return " ";
            }
        }
    }
    public class Empaque: Computador
    {
        public int Piezas_Empaquetadas;
        public int Piezas_Enviadas;

        public void Empaquetar_Pieza(Empaque empaque)
        {
            if ((empaque.Hora >= 8) && (empaque.Hora <= 18))
            {
                Console.WriteLine("Piezas listas para empezar Empaquetamiento");
            }
            else
            {
                if (empaque.Interruptor == "ON")
                {
                    empaque.Apagado_Maquina(empaque.Nombre);
                }

                Console.WriteLine("Horario NO Laboral, Maquina " + empaque.Nombre + " esta apagada, las piezas no han podido ser recepcionadas por esta Maquina");
            }
        }

        public string Enviar_Empaque(Empaque empaque)
        {
            if ((empaque.Hora >= 8) && (empaque.Hora <= 18))
            {
                if (empaque.Interruptor == "OFF")
                {
                    empaque.Encendido_Maquina(empaque.Nombre);
                }
            }
            else
            {
                if (empaque.Interruptor == "ON")
                {
                    empaque.Apagado_Maquina(empaque.Nombre);
                }
                Console.WriteLine("Horario NO Laboral, Maquina " + empaque.Nombre + " esta apagada, las piezas no han podido ser Enviadas por esta Maquina");
            }
            if (empaque.Interruptor == "ON")
            {
                if (empaque.Memoria + empaque.Piezas_Enviadas <= 100)
                {
                    empaque.Piezas_Enviadas = empaque.Piezas_Empaquetadas;
                    empaque.Memoria += empaque.Piezas_Enviadas;
                    if (empaque.Memoria == 100)
                    {
                        empaque.Reinicio_Maquina(empaque.Nombre);
                    }
                    Console.WriteLine("Piezas Enviadas al Cliente!");
                    return " ";
                }
                else
                {
                    empaque.Piezas_Enviadas = empaque.Piezas_Empaquetadas;
                    empaque.Reinicio_Maquina(empaque.Nombre);
                    Console.WriteLine("Piezas Enviadas al Cliente!");
                    return " ";
                }
            }
            else
            {
                Console.WriteLine("Maquina " + empaque.Nombre + " esta Apagada, ha sido imposible enviar las piezas al Cliente, espere al inicio de la siguiente jornada laboral");
                return " ";
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
