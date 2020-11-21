using Demo.APIModel;
using Demo.Model;

public interface IAuthenticationService {
    string GenerateToken (string username);
    bool IsValidUser (string username, string password);
    void SetUserPassWord (string username, string password);
    User GetADUserDetails(string userName);
}