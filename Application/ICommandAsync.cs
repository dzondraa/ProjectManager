using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface ICommandAsync<TRequest> : IUseCase
    {
        public Task Execute(TRequest request);
    }
}
