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

        public static Dictionary<int, int> FindCountUnreadedMessageByUser(int uid) 
        {
            Dictionary<int, int> res = new Dictionary<int, int>();
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            List<Uzer> users = UserService.FindAllUser();
            foreach (Uzer u in users)
            {
                int count = ctx.Message.Count(t => t.MsgTo.Equals(uid) && t.MsgFrom.Equals(u.Id) && t.Valid.Equals(true) && t.Readed.Equals(false));
                if (count > 0)
                {
                    res.Add(u.Id, count);
                }
            }

            return res;
        }

        public static void UpdateMsgReaded(List<Message> msgs) 
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            foreach (Message m in msgs)
            {
                if (!m.Readed)
                {
                    ctx.Update(m);
                    m.Readed = true;
                    ctx.SaveChanges();
                }
            }
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
            m.Readed = false;

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
