using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;

namespace rest_sharp_demo;

class Program
{
    static void Main()
    {
        // Create a RestClient instance
        var baseUrl = "https://github.com/santoshkaranam/rest-sharp-demo/raw/main/images.zip";
        RestClientOptions options = new RestClientOptions(baseUrl) { };
        var client = new RestClient(options);

        // Create a RestRequest for the GET method
        var request = new RestRequest();


        var file = "data.zip";
        if (File.Exists(file))
        {
            File.Delete(file);
        }

        // Execute the request and get the response
        byte[]? response = client.DownloadData(request);
        if (response != null) File.WriteAllBytes(file, response);
        
        //unzip
        IronZip.IronZipArchive.ExtractArchiveToDirectory(file,"output");
    }

    private static void PostMethodDemo()
    {
        var client = new RestClient("https://jsonplaceholder.typicode.com/users");

        // Create a new user object
        var newUser = new User
        {
            Name = "John Doe",
            Email = "john.doe@example.com"
        };

        // Serialize the user object to JSON
        var jsonBody = JsonSerializer.Serialize(newUser);

        // Create a RestRequest for the POST method with the JSON body
        var request = new RestRequest().AddJsonBody(jsonBody);

        var response = client.ExecutePost(request);

        if (response.IsSuccessful)
        {
            // Output the response content
            Console.WriteLine(response.Content);
        }
        else
        {
            Console.WriteLine($"Error: {response.ErrorMessage}");
        }
    }

    private static void GetMethod()
    {
        // Create a RestClient instance
        var baseUrl = "https://jsonplaceholder.typicode.com/users";
        RestClientOptions options = new RestClientOptions(baseUrl) { UseDefaultCredentials = true };
        var client = new RestClient(options);

        // Create a RestRequest for the GET method
        var request = new RestRequest();

        // Execute the request and get the response
        var response = client.Get(request);

        // Check if the request was successful
        if (response.IsSuccessful)
        {
            // Deserialize JSON response into a list of User objects
            var users = JsonSerializer.Deserialize<List<User>>(response.Content);

            // Output user information
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
            }
        }
        else
        {
            // Handle the error
            Console.WriteLine($"Error: {response.ErrorMessage}");
        }
    }
}

public class Address
{
    [JsonPropertyName("street")] public string Street { get; set; }

    [JsonPropertyName("suite")] public string Suite { get; set; }

    [JsonPropertyName("city")] public string City { get; set; }

    [JsonPropertyName("zipcode")] public string Zipcode { get; set; }

    [JsonPropertyName("geo")] public Geo Geo { get; set; }
}

public class Company
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("catchPhrase")] public string CatchPhrase { get; set; }

    [JsonPropertyName("bs")] public string Bs { get; set; }
}

public class Geo
{
    [JsonPropertyName("lat")] public string Lat { get; set; }

    [JsonPropertyName("lng")] public string Lng { get; set; }
}

public class User
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("username")] public string Username { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonPropertyName("address")] public Address Address { get; set; }

    [JsonPropertyName("phone")] public string Phone { get; set; }

    [JsonPropertyName("website")] public string Website { get; set; }

    [JsonPropertyName("company")] public Company Company { get; set; }
}