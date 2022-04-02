﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3library
{
    public class SocketLogger : ILogger
    {
        private ClientSocket clientSocket;        
        public SocketLogger(string host, int port)
        {
            this.clientSocket = new ClientSocket(host, port);
        }

        ~SocketLogger()
        {
            this.Dispose(false);
        }

       
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.clientSocket.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }
        public void Log(params string[] messages)
        {

            
            DateTime dateTime = DateTime.Now;
            String currentLog = dateTime.ToString() + " ";
            foreach (var message in messages)
            {
                currentLog += message + " ";


                /* using (var writer = new System.IO.StreamWriter(this.clientSocket.GetStream(), Encoding.UTF8, 1024, true))
                {
                     writer.WriteLine(currentLog);
                }*/

                using (clientSocket)
                {
                    byte[] requestBytes = Encoding.UTF8.GetBytes(currentLog);
                    clientSocket.Send(requestBytes);

                    byte[] responseBuffer = new byte[1024];
                    int responseSize = clientSocket.Receive(responseBuffer);
                    String responseText = Encoding.UTF8.GetString(responseBuffer, 0, responseSize);

                    Console.WriteLine(responseText);
                    Console.WriteLine(dateTime + " " + currentLog + " socket");
                    clientSocket.Close();
                }
            }
            

        }

    }
}
