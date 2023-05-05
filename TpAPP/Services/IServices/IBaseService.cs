using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Models;

namespace TpAPP.Services.IServices
{
    public interface IBaseService: IDisposable 
    {
        ResponseDto responseModel { set; get; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);

    }
}
