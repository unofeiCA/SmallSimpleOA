using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmallSimpleOA.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using SmallSimpleOA.Models;


namespace SmallSimpleOA.Hubs
{
    public class MessageHub : Hub
    {
        private static MessageHubStore _store = new MessageHubStore();
        public MessageHub()
        {
        }

        public async Task SendMessage(string to, string message)
        {
            int? uid = Context.GetHttpContext().Session.GetInt32("uid");
            uid = 1;
            if (uid != null)
            {
                Uzer u = UserService.FindUserByID((int)uid);
                int t;
                if (Int32.TryParse(to, out t))
                {
                    MessageService.AddMessage((int)uid, t, message, DateTime.Now);
                }

                string connId = _store.GetConnectionIdByUserId(to);
                if (connId != null)
                {
                    await Clients.Client(connId).SendAsync("ReceiveMessage", uid.ToString(),u.FirstName + " " + u.FirstName, message);
                }
            }
        }

            
    public override async Task OnConnectedAsync()
        {
            int? uid = Context.GetHttpContext().Session.GetInt32("uid");
            if (uid != null)
            {
                _store.SetConnectionIdWithUserId(uid.ToString(), Context.ConnectionId);

            }
            Console.WriteLine("connected ===========");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            _store.RemoveUserByConnectionId(Context.ConnectionId);
            Console.WriteLine("disconnected ===========");

            await base.OnDisconnectedAsync(exception);
        }

        private class MessageHubStore
        {
            private Dictionary<string, string> _connectionMap; // <userId, connectionId>

            public MessageHubStore()
            {
                _connectionMap = new Dictionary<string, string>();
            }

            public string GetConnectionIdByUserId(string uid)
            {
                lock (_connectionMap){
                    if (_connectionMap.ContainsKey(uid))
                    {
                        return _connectionMap.GetValueOrDefault(uid);
                    }
                    else
                    {
                        return null;
                    }
                }

            }

            public void SetConnectionIdWithUserId(string uid, string connId)
            {
                lock (_connectionMap)
                {
                    if (_connectionMap.ContainsKey(uid))
                    {
                        _connectionMap[uid] = connId;
                    }
                    else
                    {
                        _connectionMap.Add(uid, connId);
                    }
                }
            }

            public void RemoveUserByConnectionId(string connId)
            {
                lock (_connectionMap)
                {
                    foreach (KeyValuePair<string, string> kv in _connectionMap)
                    {
                        if (kv.Value.Equals(connId))
                        {
                            _connectionMap.Remove(kv.Key);
                            break;
                        }
                    }
                }
            }
        }
    }
}
