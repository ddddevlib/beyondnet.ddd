using AutoMapper;

namespace BeyondNet.Ddd.AutoMapper.Impl
{
    public class EnumerationConverter<TEnum> : ITypeConverter<int, TEnum> where TEnum : Enumeration
    {
        public TEnum Convert(int source, TEnum destination, ResolutionContext context)
        {
            return Enumeration.FromValue<TEnum>(source)!;
        }
    }
}
