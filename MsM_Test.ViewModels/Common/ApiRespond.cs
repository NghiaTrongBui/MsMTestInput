using System;
using System.Collections.Generic;
using System.Text;

namespace MsM_Test.ViewModels.Common
{
    public class ApiRespond<T>
    {
        public bool IsSucceed { get; set; }

        public string Messeage { get; set; }

        public T RespondObj { get; set; }
    }
}
