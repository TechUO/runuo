using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Customs.RCon
{

    class Listener
    {

        public bool StartListening()
        {
            try 
            {
                HttpListener Listener = new HttpListener();
                Listener.Start();
                Listener.Prefixes.Add(string.Concat(RCon.ListenDomain, ":", RCon.ListenPort.ToString()));
                return StartListening(Listener);
        	}
	        catch (Exception)
        	{
		        return false;
	        }
        }

        public bool StartListening(HttpListener Listener)
        {
            IAsyncResult result = Listener.BeginGetContext(new AsyncCallback(ListenerCallback), Listener);
            StartListening(Listener);
            return true;
        }

        public void ListenerCallback(IAsyncResult result)
        {
            HttpListener Listener = (HttpListener)result.AsyncState;
            HttpListenerContext Context = Listener.EndGetContext(result);
            HttpListenerRequest Request = Context.Request;
            HttpListenerResponse Response = Context.Response;

            if (Request.HttpMethod == "POST")
            {
                //Pull json list of commands
                //Decrypt Commands.
                //Run commands through CommandProcessor
                //Wait for results and respond to query.
                StartListening(Listener);
            }
            else
                StartListening(Listener);

        }
 
    }
}
