using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3library
{
    public class CommonLogger: ILogger
    {
        private ILogger[] loggers;

        public CommonLogger(ILogger[] loggers)
        {
            this.loggers = loggers;
        }

        public void Dispose()
        {

        }
        

        public void Log(params string[] messages)
        {
            foreach (var logger in loggers)
            {              
                logger.Log(messages);
            }
        }
    }
}
