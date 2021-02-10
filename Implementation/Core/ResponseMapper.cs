using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Core
{
    public class ResponseMapper<TSource,TDestination>
    {
        private readonly IMapper _mapper;
        public ResponseMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TDestination> Map(IEnumerable<TSource> source)
        {
            return _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }
    }
}
