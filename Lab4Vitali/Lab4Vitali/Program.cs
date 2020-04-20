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
        public bool Automatico;
        public int Memoria;
        public string Nombre;
        


        public string Encendido_Maquina(string maquina)
        {
            Interruptor = "ON";
            Console.WriteLine("La Maquina " + maquina + " esta Encendiendose");
            Console.WriteLine("");
            return "La Maquina " + maquina + " esta Encendiendose";
        }


        public string Apagado_Maquina(string maquina, int hora)
        {
            Interruptor = "OFF";
            Memoria = 0;
            if (hora == 19)
            {
                Console.WriteLine("Apagando la Maquina " + maquina + "...");
            }
            else
            {
                Console.WriteLine("La Maquina " + maquina + "esta Apagada");
            }
            Console.WriteLine("");
            return "La Maquina " + maquina + " esta Apagada";
        }


        public string Reinicio_Maquina(string maquina)
        {
            if (Automatico == true)
            {
                if (Memoria >= 100)
                {
                    Memoria = 0;
                    Console.WriteLine("La Memoria de la Maquina " + maquina + "se lleno");
                    Console.WriteLine("Reiniciando Maquina" + maquina + "...");
                    Interruptor = "ON";
                    //Console.WriteLine("");
                    Console.WriteLine("Maquina " + maquina + " Reiniciada");
                    return "Maquina " + maquina + " Reiniciada";
                }
                else
                {
                    Console.WriteLine("Maquina " + maquina + " aun tiene Memoria");
                    return "Maquina " + maquina + " aun tiene Memoria";
                }
            }
                
            else
            {
                string Reinicio_Manual = "No";
                while (Reinicio_Manual != "Si")
                {
                    Console.WriteLine("Desea Reiniciar la maquina " + maquina + " ? (Si/No) --> ");
                    Reinicio_Manual = Console.ReadLine();
                    if (Reinicio_Manual == "No")
                    {
                        if (Memoria >= 100)
                        {
                            Console.WriteLine("La Maquina " + maquina + " No posee Memoria Disponible");
                            Console.WriteLine("Es Necesario Reiniciar la Maquina" + maquina + "Para Poder Continuar");
                        }
                        return " ";
                    }
                    else if ((Reinicio_Manual != "No") && (Reinicio_Manual != "Si"))
                    {
                        Console.WriteLine("Opcion Incorrecta, porfavor responder Si o No");
                        return "Opcion Incorrecta, porfavor responder Si o No";

                    }
                }
                if (Reinicio_Manual == "Si")
                {
                    Memoria = 0;
                    Console.WriteLine("Reiniciando Maquina...");
                    Interruptor = "ON";
                    Console.WriteLine("");
                    Console.WriteLine("Maquina " + maquina + " Reiniciada");
                    return "Maquina " + maquina + " Reiniciada";
                }
                
                else
                {
                    return "";
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
                recepcion.Apagado_Maquina(recepcion.Nombre, recepcion.Hora);
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
                    recepcion.Apagado_Maquina(recepcion.Nombre, recepcion.Hora);
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
                        Console.WriteLine("La Maquina "+ recepcion.Nombre + "No dispone de mas Memoria");
                        recepcion.Reinicio_Maquina(recepcion.Nombre);
                    }
                    Console.WriteLine("Piezas Enviadas a Almacenamiento");
                    return " ";
                }
                else
                {
                    recepcion.Piezas_Almacenamiento = recepcion.Piezas_Recibidas;
                    Console.WriteLine("La Maquina " + recepcion.Nombre + "No dispone de mas Memoria");
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
                    almacenamiento.Apagado_Maquina(almacenamiento.Nombre,almacenamiento.Hora);
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
                    almacenamiento.Apagado_Maquina(almacenamiento.Nombre, almacenamiento.Hora);
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
                        Console.WriteLine("La Maquina " + almacenamiento.Nombre + "No dispone de mas Memoria");
                        almacenamiento.Reinicio_Maquina(almacenamiento.Nombre);
                    }
                    //Console.WriteLine("");
                    Console.WriteLine("Se han almacenado " + almacenamiento.Piezas_Ensamblaje + " Piezas");
                    //Console.WriteLine("");
                    Console.WriteLine("Piezas enviadas a Ensamblaje");
                    return " ";
                }
                else
                {
                    Console.WriteLine("La Maquina " + almacenamiento.Nombre + "No dispone de mas Memoria");
                    almacenamiento.Reinicio_Maquina(almacenamiento.Nombre);
                    almacenamiento.Piezas_Almacenadas += almacenamiento.Piezas_Recibidas;
                    almacenamiento.Piezas_Ensamblaje = rnd.Next(0, almacenamiento.Piezas_Almacenadas);
                    almacenamiento.Piezas_Almacenadas -= almacenamiento.Piezas_Ensamblaje;
                    almacenamiento.Memoria += almacenamiento.Piezas_Ensamblaje;
                    //Console.WriteLine("");
                    Console.WriteLine("Se han almacenado " + almacenamiento.Piezas_Ensamblaje + " Piezas");
                    //Console.WriteLine("");
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
                    ensamblaje.Apagado_Maquina(ensamblaje.Nombre, ensamblaje.Hora);
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
                    ensamblaje.Apagado_Maquina(ensamblaje.Nombre, ensamblaje.Hora);
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
                        Console.WriteLine("La Maquina " + ensamblaje.Nombre + "No dispone de mas Memoria");
                        ensamblaje.Reinicio_Maquina(ensamblaje.Nombre);
                    }
                    //Console.WriteLine("");
                    Console.WriteLine("Se han ensamblado " + ensamblaje.Piezas_Verificacion + " Piezas");
                    //Console.WriteLine("");
                    Console.WriteLine("Piezas enviadas a Verificacion");
                    return " ";
                }
                else
                {
                    ensamblaje.Piezas_Ensambladas += ensamblaje.Piezas_Recibidas;
                    ensamblaje.Piezas_Verificacion = (100 - ensamblaje.Memoria);
                    ensamblaje.Piezas_Ensambladas -= ensamblaje.Piezas_Verificacion;
                    Console.WriteLine("La Maquina " + ensamblaje.Nombre + "No dispone de mas Memoria");
                    ensamblaje.Reinicio_Maquina(ensamblaje.Nombre);
                    ensamblaje.Piezas_Verificacion += ensamblaje.Piezas_Ensambladas;
                    ensamblaje.Memoria += ensamblaje.Piezas_Ensambladas;
                    ensamblaje.Piezas_Ensambladas = 0;
                    //Console.WriteLine("");
                    Console.WriteLine("Se han ensamblado " + ensamblaje.Piezas_Verificacion + " Piezas");
                    //Console.WriteLine("");
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
                    verificacion.Apagado_Maquina(verificacion.Nombre, verificacion.Hora);
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
                    verificacion.Apagado_Maquina(verificacion.Nombre, verificacion.Hora);
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
                        Console.WriteLine("La Maquina " + verificacion.Nombre + "No dispone de mas Memoria");
                        verificacion.Reinicio_Maquina(verificacion.Nombre);
                    }
                    //Console.WriteLine("");
                    Console.WriteLine("Se han verificado " + verificacion.Piezas_Empaque + " Piezas");
                    //Console.WriteLine("");
                    Console.WriteLine("Piezas enviadas a Empaque");
                    return " ";
                }
                else
                {
                    verificacion.Piezas_Verificadas += verificacion.Piezas_Recibidas;
                    verificacion.Piezas_Empaque = (100 - verificacion.Memoria);
                    verificacion.Piezas_Verificadas -= verificacion.Piezas_Empaque;
                    Console.WriteLine("La Maquina " + verificacion.Nombre + "No dispone de mas Memoria");
                    verificacion.Reinicio_Maquina(verificacion.Nombre);
                    verificacion.Piezas_Empaque += verificacion.Piezas_Verificadas;
                    verificacion.Memoria += verificacion.Piezas_Verificadas;
                    verificacion.Piezas_Verificadas = 0;
                    //Console.WriteLine("");
                    Console.WriteLine("Se han verificado " + verificacion.Piezas_Empaque + " Piezas");
                    //Console.WriteLine("");
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
                    empaque.Apagado_Maquina(empaque.Nombre, empaque.Hora);
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
                    empaque.Apagado_Maquina(empaque.Nombre, empaque.Hora);
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
                        Console.WriteLine("La Maquina " + empaque.Nombre + "No dispone de mas Memoria");
                        empaque.Reinicio_Maquina(empaque.Nombre);
                    }
                    //Console.WriteLine("");
                    Console.WriteLine("Se han empaquetado " + empaque.Piezas_Enviadas + " Piezas");
                    //Console.WriteLine("");
                    Console.WriteLine("Piezas Enviadas al Cliente!");
                    return " ";
                }
                else
                {
                    empaque.Piezas_Enviadas = empaque.Piezas_Empaquetadas;
                    Console.WriteLine("La Maquina " + empaque.Nombre + "No dispone de mas Memoria");
                    empaque.Reinicio_Maquina(empaque.Nombre);
                    //Console.WriteLine("");
                    Console.WriteLine("Se han empaquetado " + empaque.Piezas_Enviadas + " Piezas");
                    //Console.WriteLine("");
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
            Recepcion recepciones = new Recepcion();
            Almacenamiento almacenamientos = new Almacenamiento();
            Ensamblaje ensamblajes = new Ensamblaje();
            Verificacion verificaciones = new Verificacion();
            Empaque empaques = new Empaque();
            recepciones.Nombre = " de Recepcion ";
            almacenamientos.Nombre = " de Almacenamiento ";
            ensamblajes.Nombre = " de Ensamblaje ";
            verificaciones.Nombre = " de Verificacion ";
            empaques.Nombre = " de Empaque ";
            Random rnd = new Random();
            bool Running= true;



            while (Running == true)
            {
                Console.WriteLine("Bienvenido!" );
                Console.WriteLine("");
                Console.WriteLine("Esta es la Interfaz del Computador Central");
                Console.WriteLine("");
                Console.WriteLine("Como te gustaria operar? ");
                Console.WriteLine("Pulsa / 1 para control manual / 2 para control automatico / 3 para apagar");
                Console.WriteLine("");
                string Opcion_Inicial = Console.ReadLine();

                if (Opcion_Inicial == "1")
                {
                    recepciones.Automatico = false;
                    almacenamientos.Automatico = false;
                    ensamblajes.Automatico = false;
                    verificaciones.Automatico = false;
                    empaques.Automatico = false;

                }

                else if (Opcion_Inicial == "2")
                {
                    recepciones.Automatico = true;
                    almacenamientos.Automatico = true;
                    ensamblajes.Automatico = true;
                    verificaciones.Automatico = true;
                    empaques.Automatico = true;

                }

                else if (Opcion_Inicial == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opcion Invalida!");
                    Console.WriteLine("");
                    Console.WriteLine("Redirigiendo...");
                }

                //Conteo Horas dia 

                for (int Contador_Hora = 0; Contador_Hora<24; Contador_Hora++)
                {
                    recepciones.Hora = Contador_Hora;
                    almacenamientos.Hora = Contador_Hora;
                    ensamblajes.Hora = Contador_Hora;
                    verificaciones.Hora = Contador_Hora;
                    empaques.Hora = Contador_Hora;

                    Console.WriteLine("Hora --> [" + Contador_Hora + ":00]");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    //llamado a metodos de Maquinas por cada hora del dia
                    
                    if ((Contador_Hora <= 8) || (Contador_Hora >= 18))
                    {
                        
                            recepciones.Interruptor = "OFF";
                            almacenamientos.Interruptor = "OFF";
                            ensamblajes.Interruptor = "OFF";
                            verificaciones.Interruptor = "OFF";
                            empaques.Interruptor = "OFF";
                            if (Contador_Hora == 19)
                            {
                                Console.WriteLine("###############################################");
                                Console.WriteLine("#                                             #");
                                Console.WriteLine("#           FIN HORARIO LABORAL               #");
                                Console.WriteLine("#                                             #");
                                Console.WriteLine("###############################################");
                                Console.WriteLine("");
                                recepciones.Apagado_Maquina(recepciones.Nombre, recepciones.Hora);
                                almacenamientos.Apagado_Maquina(almacenamientos.Nombre,almacenamientos.Hora );
                                ensamblajes.Apagado_Maquina(ensamblajes.Nombre,ensamblajes.Hora );
                                verificaciones.Apagado_Maquina(verificaciones.Nombre,verificaciones.Hora );
                                empaques.Apagado_Maquina(empaques.Nombre, empaques.Hora);
                                Console.WriteLine("");
                                Console.WriteLine("###############################################");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                            }
                        
                    }
                    if (Contador_Hora >= 8)
                    {
                        if (Contador_Hora <= 18)
                        {
                            recepciones.Interruptor = "ON";
                            almacenamientos.Interruptor = "ON";
                            ensamblajes.Interruptor = "ON";
                            verificaciones.Interruptor = "ON";
                            empaques.Interruptor = "ON";
                            if (Contador_Hora == 8)
                            {
                                Console.WriteLine("###############################################");
                                Console.WriteLine("#                                             #");
                                Console.WriteLine("#           INICIO HORARIO LABORAL            #");
                                Console.WriteLine("#                                             #");
                                Console.WriteLine("###############################################");
                                Console.WriteLine("");
                                recepciones.Encendido_Maquina(recepciones.Nombre);
                                almacenamientos.Encendido_Maquina(almacenamientos.Nombre);
                                ensamblajes.Encendido_Maquina(ensamblajes.Nombre);
                                verificaciones.Encendido_Maquina(verificaciones.Nombre);
                                empaques.Encendido_Maquina(empaques.Nombre);
                                Console.WriteLine("");
                            }
                        }
                    }
                    
                    recepciones.Piezas_Recibidas = rnd.Next(0, 200);
                    recepciones.Recibir_Piezas(recepciones);
                    recepciones.Enviar_a_Almacenamiento(recepciones);
                    Console.WriteLine("");
                    almacenamientos.Piezas_Recibidas = recepciones.Piezas_Almacenamiento;
                    almacenamientos.Almacenar_Piezas(almacenamientos);
                    almacenamientos.Enviar_a_Ensamblaje(almacenamientos);
                    Console.WriteLine("");
                    ensamblajes.Piezas_Recibidas = almacenamientos.Piezas_Ensamblaje;
                    ensamblajes.Verificar_Piezas(ensamblajes);
                    ensamblajes.Ensamblar_Piezas(ensamblajes);
                    Console.WriteLine("");
                    verificaciones.Piezas_Recibidas = ensamblajes.Piezas_Verificacion;
                    verificaciones.Verificar_Pieza(verificaciones);
                    verificaciones.Enviar_a_Empaque(verificaciones);
                    Console.WriteLine("");
                    empaques.Piezas_Empaquetadas = verificaciones.Piezas_Empaque;
                    empaques.Empaquetar_Pieza(empaques);
                    empaques.Enviar_Empaque(empaques);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");

                }

            
            }
        }
    }
}
