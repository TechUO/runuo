using System;
using System.Collections.Generic;
using System.Text;
using System.Net;


namespace Customs.Rcon
{
    class RCon
    {
        private HttpListener listener = new HttpListener();
        public static void Initialize()
        {
            Console.WriteLine("Initializing RCon System");
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:2594/");
            listener.Start();
            IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
        }

        public static void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = string.Concat("<HTML><BODY> Hello world!", "</BODY></HTML>");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            IAsyncResult res = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
        }

    }
}
