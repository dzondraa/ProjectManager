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
            logger.Log(query, new { });

            if (!actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizeUseCaseException(query, actor);
            }

            return query.Execute(search);

        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            logger.Log(command, actor, request);
            //Console.Write($"{DateTime.Now}: {actor.Identity} is trying to execute {command.Name}");

            if (!actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizeUseCaseException(command, actor);
            }

            command.Execute(request);
        }
    }
}
