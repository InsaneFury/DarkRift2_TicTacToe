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
                client.MessageReceived += OnMessageReceived;
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

        public void MessageSlateTaken(ushort slateIndex,ushort matchID)
        {
            using(DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(matchID);
                writer.Write(slateIndex);
                using(Message message = Message.Create((ushort)Tags.Tag.SLATE_TAKEN, writer))
                {
                    client.SendMessage(message, SendMode.Reliable);
                }
            }
        }

        // GETTING MESSAGES FROM SERVER
        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            switch ((Tags.Tag)e.Tag)
            {
                case Tags.Tag.GOT_MATCH:
                    //TODO: start a new match - move the match scene
                    using (Message msg = e.GetMessage())
                    {
                        using (DarkRiftReader reader = msg.GetReader())
                        {
                            ushort matchID = reader.ReadUInt16();
                            MatchModel.currentMatch = new MatchModel(matchID);
                            Debug.Log(MatchModel.currentMatch.id);
                        }
                    }
                    break;
            }
        }





    }  
}

