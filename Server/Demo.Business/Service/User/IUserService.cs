using System.Collections.Generic;
using Demo.APIModel;

public interface IUserService
{
    UserAPIModel Authenticate(string username, string password);
    void Register(UserAPIModel user);
    void ActivateUser(long id);
    void DeativateUser(long id);
    IEnumerable<UserRegistrationAPIModel> GetAllUserRegistrations();
}