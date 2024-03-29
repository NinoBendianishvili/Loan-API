namespace capstone.Interfaces;

public interface ITokenService
{
    public int? GetUserIdFromToken(string token);
    public string GenerateToken(Iunifier user);
    public string GetRoleFromToken(string token);
}