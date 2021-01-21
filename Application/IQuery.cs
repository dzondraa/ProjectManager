using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    // Actions that requesting the representation of data from our system
    public interface IQuery<TSearch, TResult> : IUseCase
    {
        TResult Execute(TSearch search);
    }
}
