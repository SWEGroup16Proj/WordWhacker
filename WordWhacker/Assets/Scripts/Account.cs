using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Account
{
    public string Username { get; set; }
    public byte[] HashedPassword { get; set; }
    public byte[] Salt { get; set; }
    public int HighScore { get; set; }
    private string currentAccount; // Reference to the currently logged-in account

    public bool isAdmin {  get; set; }

    [JsonConstructor]
    public Account()
    {
    }
    public Account(string username, string password, bool isA = false)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }
        Username = username;
        Salt = GenerateSalt();
        HashedPassword = string.IsNullOrEmpty(password) ? new byte[0] : HashPassword(password, Salt);
        HighScore = 0;
        isAdmin = isA;
    }

    private byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }

    private byte[] HashPassword(string password, byte[] salt)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));
        }

        if (salt == null || salt.Length == 0)
        {
            throw new ArgumentException("Salt cannot be null or empty.", nameof(salt));
        }

        using (var sha256 = SHA256.Create())
        {
            byte[] combinedBytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
            return sha256.ComputeHash(combinedBytes);
        }
    }

    public bool VerifyPassword(string password)
    {
        // Hash the password with the loaded salt
        byte[] testHash = HashPassword(password, this.Salt);

        // Convert both hashes and salt to string representations for comparison and debug
        string testHashString = BitConverter.ToString(testHash).Replace("-", "").ToLowerInvariant();
        string expectedHashString = BitConverter.ToString(this.HashedPassword).Replace("-", "").ToLowerInvariant();
        string saltString = BitConverter.ToString(this.Salt).Replace("-", "").ToLowerInvariant();

        // Debug the salt and expected vs actual hash
        Debug.Log($"Salt used: {saltString}");
        Debug.Log($"Expected Hash: {expectedHashString}");
        Debug.Log($"Generated Hash for comparison: {testHashString}");

        // Compare byte arrays
        bool isPasswordCorrect = testHash.SequenceEqual(this.HashedPassword);

        // Debug the result of the password verification
        Debug.Log($"Password verification result: {isPasswordCorrect}");

        return isPasswordCorrect;
    }



    private bool CompareByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (int i = 0; i < array1.Length; i++)
            if (array1[i] != array2[i])
                return false;

        return true;
    }
}

public class AccountManager
{
    private List<Account> accounts;

    public AccountManager()
    {
        accounts = new List<Account>();
    }

    public void LoadAccountsFromFile(string filePath)
    {
        Debug.Log("Attempting to load accounts from: " + filePath);
        if (File.Exists(filePath))
        {
            try
            {
                string json;
                using (var reader = new StreamReader(filePath))
                {
                    json = reader.ReadToEnd();
                }

                Debug.Log("Read json: " + json.Substring(0, Math.Min(json.Length, 200)));

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogWarning("Account file is empty or null, initializing new list.");
                    accounts = new List<Account>();
                }
                else
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new ByteArrayConverter() }
                    };

                    accounts = JsonConvert.DeserializeObject<List<Account>>(json, settings);
                    if (accounts == null)
                    {
                        Debug.LogWarning("Deserialization resulted in null, initializing new list.");
                        accounts = new List<Account>();
                    }
                    else
                    {
                        Debug.Log("Deserialization successful. Account count: " + accounts.Count);

                        // Debugging each account's salt
                        foreach (var account in accounts)
                        {
                            string saltString = BitConverter.ToString(account.Salt).Replace("-", "").ToLowerInvariant();
                            Debug.Log($"Username: {account.Username}, Salt: {saltString}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Exception when reading the accounts file: " + ex.ToString());
                accounts = new List<Account>();
            }
        }
        else
        {
            Debug.LogWarning("No account file found, initializing new list.");
            accounts = new List<Account>();
        }
    }



    public void SaveAccountsToFile(string filePath)
    {
        string json = JsonConvert.SerializeObject(accounts, Formatting.Indented, new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new ByteArrayConverter() }
        });
        Debug.Log("Saving data to: " + filePath);
        File.WriteAllText(filePath, json);
    }

    public bool CreateAccount(string username, string password)
    {
        if (accounts.Exists(acc => acc.Username == username))
        {
            return false; // Username already exists
        }

        accounts.Add(new Account(username, password, false));

        SaveAccountsToFile(AccountManagerBehaviour.Instance.accountFilePath);
        return true;
    }
    public bool CreateAdminAccount()
    {
        if (accounts.Exists(acc => acc.Username == "admin"))
        {
            return false; // Username already exists
        }

        accounts.Add(new Account("admin", "password", true));

        SaveAccountsToFile(AccountManagerBehaviour.Instance.accountFilePath);
        return true;
    }
    public bool Login(string username, string password)
    {
        Debug.Log($"Attempting login with Username: {username} Password: {password}");
        Account account = accounts.Find(acc => acc.Username == username);
        return account != null && account.VerifyPassword(password);
    }
    public bool IsAdmin(string username)
    {
        var account = accounts.Find(acc => acc.Username == username);
        return account != null && account.isAdmin;
    }
    public void ResetHighScore(string username)
    {
        var account = accounts.Find(acc => acc.Username == username);
        if (account != null)
        {
            account.HighScore = 0;
            SaveAccountsToFile(AccountManagerBehaviour.Instance.accountFilePath); // Save the changes
            Debug.Log($"High score reset for user {username}.");
        }
        else
        {
            Debug.LogError($"User {username} not found.");
        }
    }
    public void DeleteAccount(string username)
    {
        var account = accounts.Find(acc => acc.Username == username);
        if (account != null)
        {
            accounts.Remove(account);
            SaveAccountsToFile(AccountManagerBehaviour.Instance.accountFilePath); // Save the changes
            Debug.Log($"Account for user {username} deleted.");
        }
        else
        {
            Debug.LogError($"User {username} not found.");
        }
    }

    public void UpdateHighScore(string username, int score)
    {
        Account account = accounts.Find(acc => acc.Username == username);
        if (account != null && score > account.HighScore)
        {
            account.HighScore = score;
        }
    }

    public List<Account> GetLeaderboard()
    {
        accounts.Sort((a, b) => b.HighScore.CompareTo(a.HighScore));
        return accounts;
    }
}

// A JSON converter for byte arrays
public class ByteArrayConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(byte[]);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        byte[] bytes = (byte[])value;
        writer.WriteValue(BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string hex = (string)reader.Value;
        if (string.IsNullOrEmpty(hex))
        {
            throw new ArgumentException("Hex string cannot be null or empty.", nameof(reader.Value));
        }

        try
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        catch (FormatException e)
        {
            throw new JsonSerializationException("Invalid hex format", e);
        }
    }

}

