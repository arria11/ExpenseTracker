using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Model;
using static MudBlazor.CategoryTypes;

namespace ExpenseTracker.Services
{
    public class UserService: IUserService
    {
        // Path to the JSON file that stores user details.
        private readonly string usersFilePath = Path.Combine(AppContext.BaseDirectory, "UserDetails.json");

        //Save a new user to the system and add them to the list.
        public async Task SaveUserAsync(User user)
        {
            try
            {
                var users = await LoadUserAsync();

                // Hash the user's password
                user.Password = HashPassword(user.Password);

                users.Add(user);
                await SaveUserAsync(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
                throw;
            }
        }

        //Load the list of users from the JSON file
        public async Task<List<User>> LoadUserAsync()
        {
            try
            {
                if (!File.Exists(usersFilePath))
                {
                    return new List<User>();
                }

                var json = await File.ReadAllTextAsync(usersFilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<User>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading users: {ioEx.Message}");
                return new List<User>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading users: {ex.Message}");
                return new List<User>(); 
            }
        }

        private async Task SaveUserAsync(List<User> users)
        {
            try
            {
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                //Saves the updated list of users to the JSON file.
                await File.WriteAllTextAsync(usersFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading users: {ioEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving users: {ex.Message}");
                throw; 
            }
        }

        // Method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        
    }
}
