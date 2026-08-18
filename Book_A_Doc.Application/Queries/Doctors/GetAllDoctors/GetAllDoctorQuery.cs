using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Doctors.GetAllDoctors;

public record GetAllDoctorsQuery
    : IRequest<Result<List<GetAllDoctorsResponse>>>;
