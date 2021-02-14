using Application.DataTransfer;
using Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public interface IUpdateProjectCommandAsync : ICommandAsync<ProjectDto>
    {
    }
}
