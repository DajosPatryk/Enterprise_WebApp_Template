namespace server.Application.Commands;

using MediatR;

public interface IRequestTransaction<TResponse> : IRequest<TResponse>;