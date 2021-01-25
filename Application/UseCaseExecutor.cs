using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<TResult> ExecuteQueryAsync<TSearch, TResult>(IQueryAsync<TSearch, TResult> query, TSearch search)
        {
            logger.Log(query, search);
            return await query.Execute(search);

        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            logger.Log(command, request);

            command.Execute(request);
        }

        public async Task ExecuteCommandAsync<TRequest>(ICommandAsync<TRequest> command, TRequest request)
        {
            logger.Log(command, request);

            await command.Execute(request);
        }


    }
}
