using Application;
using System;

namespace Implementation.Implementation
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase useCase, object useCaseData, IApplicationActor actor)
        {
            Console.WriteLine($"[{DateTime.Now}] [Actor: {actor?.Email}] Running action {useCase.Name}");
        }
    }
}
