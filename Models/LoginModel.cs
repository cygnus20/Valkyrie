using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace Valkyrie.Models;

public class LoginModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    /*public static ValueTask<LoginModel?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        LoginModel result;

        const string userNameKey = "username";
        const string passwordKey = "password";

        result = new LoginModel
        {
            Username = context.Request.Form[userNameKey],
            Password = context.Request.Form[passwordKey],
        };

        return ValueTask.FromResult<LoginModel?>(result);

    }*/
}
