using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Doctors.GetDoctor;

public record GetDoctorQuery(Guid Id) : IRequest<Result<GetDoctorResponse>>;
