using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IUseCaseLogger logger;


        public UseCaseExecutor(IUseCaseLogger logger)
        {
            this.logger = logger;
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            logger.Log(query, search);

            return query.Execute(search);

        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            logger.Log(command, request);

            command.Execute(request);
        }
    }
}
