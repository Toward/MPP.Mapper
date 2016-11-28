using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    public class MapperWithCaching : IMapper
    {
        #region Private Members

        private readonly IMapper _mapper;
        private readonly IFunctionCache _functionCache = new FunctionCache();

        #endregion

        #region Ctor

        public MapperWithCaching(IMapper mapper = null)
        {
            _mapper = mapper ?? new AutoMapper();
        }

        #endregion

        #region Public Methods

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        #endregion
    }
}
