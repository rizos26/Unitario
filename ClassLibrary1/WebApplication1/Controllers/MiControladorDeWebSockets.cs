﻿using System.Net.WebSockets;
using System.Text;


namespace WebApplication1.Controllers
{
    public class MiControladorDeWebSockets
    {

        private readonly RequestDelegate _next;

        public MiControladorDeWebSockets(RequestDelegate next)
        {
            _next = next;
        }
        public bool EndSocket;
        public async Task Invoke(HttpContext context)
        {
            // Si no es una petición socket, no procesarla por este controlador
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            // Es una petición socket, ver que nos mandan
            var ct = context.RequestAborted;
            using (var socket = await context.WebSockets.AcceptWebSocketAsync()) //aceptamos la llamada de WS
            {
                while (!EndSocket)
                {
                    var mensaje = await ReceiveStringAsync(socket, ct);   //await de recepción 
                    if (mensaje == null) return; //si el mensaje esta vacio cierra conexion

                    // Vamos a inventar dos tipos de mensajes:
                    // 1. Mensajes simples: sólo llega una cadena de texto
                    // 2. Mensajes compuestos: requerimos parámetros. Separaremos el mensaje de los parámetros con #

                    // Procesado de mensajes simples
                    switch (mensaje.ToLower())
                    {
                        case "hola":
                            await SendStringAsync(socket, "Hola como estás, bienvenido", ct);
                            break;

                        case "adios":
                            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Desconectado", ct);
                            EndSocket = true;
                            break;
                        case "churro":
                            await SendchurroAsync(socket, ct);
                            break;

                        default:
                            await SendStringAsync(socket, "Lo siento, pero no entiendo ese mensaje", ct);
                            break;
                    }

                    // Procesado de mensajes con parámetros
                    if (mensaje.Contains('#'))
                    {
                        string[] mensajeCompuesto = mensaje.ToLower().Split('#');
                        switch (mensajeCompuesto[0])
                        {
                            case "hola":
                                await SendStringAsync(socket, "Hola usuario " + mensajeCompuesto[1], ct);
                                break;

                            default:
                                await SendStringAsync(socket, "Lo siento, pero no entiendo ese mensaje", ct);
                                break;
                        }

                    }


                }
            }
            return;
        }

        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default)
        {
            // Se recibe un mensaje que debe ser descodificado
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                    throw new Exception("Mensaje inesperado");

                // Codificar como UTF8
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }
        private static async Task SendchurroAsync(WebSocket socket, CancellationToken ct = default)
        {
            // monitorización de variables

            for (int i = 0; i < 100; i++)
            {
                string data = "Number: " + new Random().Next();
                var buffer = Encoding.UTF8.GetBytes(data);
                var segment = new ArraySegment<byte>(buffer);
                socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
            }



        }



    }
}

