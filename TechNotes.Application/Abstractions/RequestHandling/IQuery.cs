
using MediatR;

namespace TechNotes.Application.Abstractions.RequestHandling;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
