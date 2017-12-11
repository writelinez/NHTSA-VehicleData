using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Contracts
{
    public interface IRestApi : IDisposable
    {
        TModelOut Get<TModelOut>(params string[] parameters) where TModelOut : class;
    }
}
