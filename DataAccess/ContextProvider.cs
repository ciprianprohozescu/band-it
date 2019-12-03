using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ContextProvider
    {
        private static ContextProvider instance;

        public static ContextProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContextProvider();
                }
                return instance;
            }
        }
        public BandItEntities DB { get; set; }

        private ContextProvider()
        {
            DB = new BandItEntities();
        }
    }
}
