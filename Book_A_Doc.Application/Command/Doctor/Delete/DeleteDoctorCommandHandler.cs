using Book_A_Doc.Application.Services;
//using Book_A_Doc.Domain.ResultPattern;
//using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
//using MediatR;

//namespace Book_A_Doc.Application.Command.Doctor.Delete;

//public class DeleteDoctorCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteDoctorCommand, Result>
//{
//    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
//    {
//        var user = identityService.FindByIdAsync(request.id);
//        if(user is null)
//        {
//            return Result.Failure(UserErrors.UserNotFound);
//        }
//        user.IsDe
//    }
//}
