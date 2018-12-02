using System;
using SmallSimpleOA.Models;
using System.Collections.Generic;
using System.Linq;
namespace SmallSimpleOA.Services
{
    public class MessageService
    {
        public MessageService()
        {
        }


        public static List<Message> FindMessageByUserAndTargetAndAmount(int uid, int tid, int amount)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            List<Message> to = ctx.Message.Where(t => t.MsgFrom.Equals(uid) && t.MsgTo.Equals(tid) && t.Valid == true).OrderByDescending(t => t.MsgTime).Take(amount).ToList();
            List<Message> from = ctx.Message.Where(t => t.MsgFrom.Equals(tid) && t.MsgTo.Equals(uid) && t.Valid == true).OrderByDescending(t => t.MsgTime).Take(amount).ToList();
            List<Message> res = MixAndSortMsg(to, from, amount);
            return res;
        }


        public static List<Message> FindMessageByUserAndTargetAndOffsetAndAmount(int uid, int tid, int offset, int amount)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            List<Message> to = ctx.Message.Where(t => t.MsgFrom.Equals(uid) && t.MsgTo.Equals(tid) && t.Id < offset && t.Valid == true).OrderByDescending(t => t.MsgTime).Take(amount).ToList();
            List<Message> from = ctx.Message.Where(t => t.MsgFrom.Equals(tid) && t.MsgTo.Equals(uid) && t.Id < offset && t.Valid == true).OrderByDescending(t => t.MsgTime).Take(amount).ToList();
            List<Message> res = MixAndSortMsg(to, from, amount);
            return res;
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

        private static List<Message> MixAndSortMsg(List<Message> to, List<Message> from, int amount) 
        {
            List<Message> combine = new List<Message>();
            combine.AddRange(to);
            combine.AddRange(from);
            combine.Sort((a, b) => ((DateTime)b.MsgTime).CompareTo(a.MsgTime));
            List<Message> res = new List<Message>();
            for (int i = 0; i < combine.Count; i++)
            {
                if (i >= amount)
                {
                    break;
                }
                res.Add(combine[i]);
            }

            return res;
        }
    }
}
