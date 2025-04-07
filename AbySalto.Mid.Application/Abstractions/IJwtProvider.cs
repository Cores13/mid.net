using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
