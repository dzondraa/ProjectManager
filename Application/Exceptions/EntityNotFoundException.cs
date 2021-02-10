using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int id, Type type)
            : base($"Entity of type {type.Name} with an id of {id} was not found.")
        {
            
        }

        public EntityNotFoundException(string name)
            : base($"Entity with name $'{name}' could not be found")
        {

        }
    }
}
