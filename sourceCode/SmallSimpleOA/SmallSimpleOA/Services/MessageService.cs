using System;
using SmallSimpleOA.Models;
namespace SmallSimpleOA.Services
{
    public class MessageService
    {
        public MessageService()
        {
        }
        public static void AddMessage(int from, int to, string content, DateTime time)
        {
            Message m = new Message();
            m.Valid = true;
            m.MsgFrom = from;
            m.MsgTo = to;
            m.Content = content;
            m.MsgTime = time;

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Add(m);
            ctx.SaveChanges();
        }
    }
}
