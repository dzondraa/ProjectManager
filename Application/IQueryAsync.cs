using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IQueryAsync<TSearch, TResult> : IUseCase
    {
        Task<TResult> Execute(TSearch search);
    }
}
