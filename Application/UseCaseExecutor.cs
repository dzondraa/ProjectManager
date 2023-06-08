using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IUseCaseLogger _logger;
        private readonly IApplicationActor _actor;


        public UseCaseExecutor(IUseCaseLogger logger, IApplicationActor actor)
        {
            _logger = logger;
            _actor = actor;
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            _logger.Log(query, search, _actor);

            return query.Execute(search);

        }

        public async Task<TResult> ExecuteQueryAsync<TSearch, TResult>(IQueryAsync<TSearch, TResult> query, TSearch search)
        {
            _logger.Log(query, search, _actor);
            return await query.Execute(search);

        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            _logger.Log(command, request, _actor);

            command.Execute(request);
        }

        public async Task ExecuteCommandAsync<TRequest>(ICommandAsync<TRequest> command, TRequest request)
        {
            _logger.Log(command, request, _actor);

            await command.Execute(request);
        }


    }
}
