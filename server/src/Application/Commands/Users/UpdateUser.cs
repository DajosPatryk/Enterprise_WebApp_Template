namespace server.Application.Commands.Users;

using FluentValidation;
using MediatR;
using Validators;
using Data.Entities;

public record UpdateUser(User User) : IRequestTransaction<Result>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, Result>
{
    private readonly DbSet<User> _set;

    public UpdateUserHandler(ApplicationDbContext context)
    {
        _set = context.Set<User>();
    }
    
    public async Task<Result> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        _set.Update(request.User);

        return Result.Ok();

        // TODO: Save changes if class does not implement IRequestTransaction, only IRequest.
        // await _context.SaveChangesAsync();
    }
}

public class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(e => e.User).SetValidator(new UserValidator());
    }
}