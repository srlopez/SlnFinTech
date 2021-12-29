using System;
using System.Diagnostics;
namespace FinTech
{
   public interface ILog
    {
        void Write(Object o);
    }
    public class ConsoleLog : ILog
    {
        public void Write(Object o)
        {
            Debug.WriteLine(o.ToString());
        }
    }
}
