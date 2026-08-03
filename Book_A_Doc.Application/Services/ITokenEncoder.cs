namespace Book_A_Doc.Application.Services;

public interface ITokenEncoder
{
    string Encode(string token);
    string Decode(string token);
}
