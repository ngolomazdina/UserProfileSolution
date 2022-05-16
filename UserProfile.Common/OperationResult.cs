using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Common.Extentions;

namespace UserProfile.Common
{
    public class OperationResult
    {        
        public CodeResult Code { get; set; }
        public string Message { get; set; }

    }
    /// <summary>
    /// Результат выполнения OperationContract
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

    }

    public enum CodeResult
    {
        [EnumDisplay("Системные ошибки")]
        SystemError = -1,

        [EnumDisplay("Успешно")]
        Success = 0,

        [EnumDisplay("Ошибка")]
        Error = 1
    }

    
}
