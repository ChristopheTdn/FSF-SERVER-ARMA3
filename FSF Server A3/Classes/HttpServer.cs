using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSF_Server_A3.Classes
{
    class HttpServer
    {

        public HttpServer(string ip, int port)
        {
            new Thread(delegate() { startServer(ip, port); }).Start();
        }

        public void startServer(string ip, int port)
        {
            // Create a listener.
            HttpListener listener = new HttpListener();
            string[] prefixes = { "http://" + ip + ":" + port + "/" };
            // Add the prefixes. 
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            while (true)
            {
                listener.Start();
                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                new Thread(delegate() { processHtml(request, response); }).Start();
            }
        }

        public void processHtml(HttpListenerRequest request, HttpListenerResponse response) 
        {
            string message = "";
            if (request.RawUrl.Equals("/SYNCHRONIZE"))
            {
                //Mettre le code de l'appel de la synchronisation ici
                message = "Synchronisation en cours ...<br/>";
            }
            else if (request.RawUrl.Equals("/RESTART"))
            {
                //Mettre le code de l'appel pour relancer le serveur ici
                message = "Relance du serveur en cours ...<br/>";
            }

            writeDataResponse(response, "<html><body><h1>FSF Launcher</h1>" + message + "<input type='button' value='Synchroniser' onclick='window.location=\"/SYNCHRONIZE\"'/><br/><input type='button' value='Relancer' onclick='window.location=\"/RESTART\"'/></html>");

        }

        public void writeDataResponse(HttpListenerResponse response, string responseString)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

    }
}

