using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client;
using System.Net;
using System;
using DarkRift;

namespace Scripts.Networking
{
    public class NetworkingManager
    {
        private static NetworkingManager instance;
        private DarkRiftClient client;
        public static NetworkingManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkingManager();
                }
                return instance;
            }
        }

        private NetworkingManager()
        {
            client = new DarkRiftClient();
        }

        public ConnectionState ConnectionState
        {
            get
            {
                return client.ConnectionState;
            }
        }

        public bool IsConnected
        {
            get
            {
                return client.ConnectionState == ConnectionState.Connected;
            }
        }

        public bool Connect()
        {
            if (client.ConnectionState == DarkRift.ConnectionState.Connecting)
            {
                return false;
            }
            if (client.ConnectionState == DarkRift.ConnectionState.Connected)
            {
                return true;
            }

            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), 4296, false);
            }
            catch (Exception)
            {

                
            }
            return false;
        }
        public void MessageNameToServer(string name)
        {
            if (IsConnected)
            {
                using(DarkRiftWriter writer = DarkRiftWriter.Create())
                {
                    writer.Write(name);

                    using (Message message = Message.Create((ushort)Tags.Tag.SET_NAME,writer))
                    {
                        client.SendMessage(message, SendMode.Reliable);
                    }
                }
            }
        }








    }  
}

