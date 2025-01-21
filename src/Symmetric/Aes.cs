using System.Text;

namespace Symmetric;

public class Aes
{
    private const int OneByte = 8;
    private const int KeySize = 256;
    private const int BlockSize = 128;
    private const int Rounds = 14;

    private string _key = string.Empty;
    public string Key
    {
        get => _key;
        set
        {
            if (value.Length != (KeySize / OneByte))
            {
                throw new ArgumentException($"Key length should be {KeySize/OneByte} but was {value.Length}");
            }
        
            _key = value;
        }
    }
    
    private string _initializationVector = string.Empty;
    public string InitializationVector
    {
        get => _initializationVector;
        set => _initializationVector = value;
    }

    public Aes(string key)
    {
        Key = key;
    }
    
    public string Encrypt(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var keyBytes = Encoding.UTF8.GetBytes(_key);
        var byteEncryption = Encrypt(plainTextBytes, keyBytes);
        var resultEncryption = Encoding.UTF8.GetString(byteEncryption);
        
        return resultEncryption;
    }

    private byte[] Encrypt(byte[] data, byte[] key)
    {
        if (data.Length != (KeySize / OneByte))
        {
            throw new ArgumentException();
        }

        AddRoundKey();
        
        for (var i = 0; i < Rounds; i++)
        {
            data = SubBytes(data);
            data = ShiftRows(data);
            data = MixColumns(data);
            AddRoundKey();
        }

        return data;
    }

    private byte[] SubBytes(byte[] data)
    {
        throw new NotImplementedException();
    }

    private byte[] ShiftRows(byte[] data)
    {
        throw new NotImplementedException();
    }

    private byte[] MixColumns(byte[] data)
    {
        throw new NotImplementedException();   
    }

    private void AddRoundKey()
    {
        throw new NotImplementedException();
    }
}