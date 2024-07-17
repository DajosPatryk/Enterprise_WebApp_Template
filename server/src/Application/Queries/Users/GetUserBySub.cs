namespace server.Application.Queries.Users;

using FluentValidation;
using MediatR;
using Validators;
using Data.Entities;

public record GetUserBySub(string Sub) : IRequest<Result<User>>;

public class GetUserBySubHandler : IRequestHandler<GetUserBySub, Result<User>>
{
    private readonly DbSet<User> _set;

    public GetUserBySubHandler(ApplicationDbContext context)
    {
        _set = context.Set<User>();
    }
    
    public async Task<Result<User>> Handle(GetUserBySub request, CancellationToken cancellationToken)
    {
        var user = await _set.AsNoTracking().FirstOrDefaultAsync(u => u.Sub == request.Sub);

        var result = Result.FailIf(user == null, "User not found.");
        if (result.IsFailed) return result;
        
        return user;
    }
}

public class GetUserBySubValidator : AbstractValidator<GetUserBySub>
{
    public GetUserBySubValidator()
    {
        RuleFor(e => e.Sub).SetValidator(new UserSubValidator());
    }
}