namespace Valkyrie.Models;

public record CreateUserModel(
    string Username, 
    string Email, 
    string PhoneNumber,
    string Password);

public record ChangePasswordModel(
    string CurrentPassword,
    string NewPassword,
    string ConfirmedNewPassword);
