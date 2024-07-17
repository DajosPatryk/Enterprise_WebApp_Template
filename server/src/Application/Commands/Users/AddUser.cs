namespace server.Application.Commands.Users;

using FluentValidation;
using MediatR;
using Validators;
using Data.Entities;

public record AddUser(User User) : IRequestTransaction<Result<User>>;

public class AddUserHandler : IRequestHandler<AddUser, Result<User>>
{
    private readonly DbSet<User> _set;

    public AddUserHandler(ApplicationDbContext context)
    {
        _set = context.Set<User>();
    }
    
    public async Task<Result<User>> Handle(AddUser request, CancellationToken cancellationToken)
    {
        var user = await _set.AsNoTracking().FirstOrDefaultAsync(u => u.Sub == request.User.Sub);
        var guard = Result.FailIf(user != null, "User already exists.");
        if (guard.IsFailed) return guard;

        var entity = _set.Add(request.User).Entity;
        return entity;
        
        // TODO: Save changes if class does not implement IRequestTransaction, only IRequest.
        // await _context.SaveChangesAsync();
    }
}

public class AddUserValidator : AbstractValidator<AddUser>
{
    public AddUserValidator()
    {
        RuleFor(e => e.User).SetValidator(new UserValidator());
    }
}