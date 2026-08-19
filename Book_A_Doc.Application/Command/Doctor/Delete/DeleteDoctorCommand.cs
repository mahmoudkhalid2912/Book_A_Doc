using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.Doctor.Delete;

public record DeleteDoctorCommand(Guid id) : IRequest<Result>;

