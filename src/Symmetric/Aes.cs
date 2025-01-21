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
        const int blockSizeBytes = BlockSize / OneByte;
        List<byte>? extendedPlainText = null;
        string resultEncryption = string.Empty;
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var keyBytes = Encoding.UTF8.GetBytes(_key);

        if (plainTextBytes.Length < blockSizeBytes)
        {
            var diffLen = blockSizeBytes - plainTextBytes.Length;
            
            extendedPlainText = new List<byte>(blockSizeBytes);
            extendedPlainText.AddRange(plainTextBytes);
            extendedPlainText.AddRange(Enumerable.Repeat<byte>(0, diffLen).ToArray());
            
            var byteEncryption = Encrypt(extendedPlainText.ToArray(), keyBytes);
            resultEncryption = Encoding.UTF8.GetString(byteEncryption);
        }
        else if (plainTextBytes.Length > blockSizeBytes)
        {
            throw new NotImplementedException();
        }
        else
        {
            var byteEncryption = Encrypt(plainTextBytes, keyBytes);
            resultEncryption = Encoding.UTF8.GetString(byteEncryption);
        }

        return resultEncryption;
    }

    private byte[] Encrypt(byte[] data, byte[] key)
    {
        if (data.Length != BlockSize / OneByte)
        {
            throw new ArgumentException($"Size of block should be {BlockSize/OneByte} bytes, but was {data.Length} bytes");
        }

        if (key.Length != KeySize / OneByte)
        {
            throw new ArgumentException($"Size of key should be {KeySize/OneByte} bytes, but was {key.Length} bytes");
        }

        AddRoundKey(data, key);
        
        for (var i = 0; i < Rounds; i++)
        {
            data = SubBytes(data);
            data = ShiftRows(data);
            data = MixColumns(data);
            AddRoundKey(data, key);
        }

        return data;
    }

    private byte[] SubBytes(byte[] data)
    {
        return data;
    }

    private byte[] ShiftRows(byte[] data)
    {
        var matrix = ToTwoDimensionalArray(data);

        for (var i = 1; i < matrix.Length; i++)
        {
            List<byte> row = [];
            
            row.AddRange(matrix[i].Take(new Range(i, matrix[i].Length)));
            row.AddRange(matrix[i].Take(new Range(0, i)));

            matrix[i] = row.ToArray();
        }


        return data;
    }

    private byte[] MixColumns(byte[] data)
    {
        return data;
    }

    private byte[] AddRoundKey(byte[] data, byte[] key)
    {
        return data;
    }

    private byte[][] ToTwoDimensionalArray(byte[] data)
    {
        const int matrixSize = BlockSize / OneByte;
        var matrixRowSize = (int)Math.Sqrt(matrixSize);
        
        byte[][] matrix = new byte[matrixRowSize][];

        for (int i = 0; i < matrixRowSize; i++)
        {
            matrix[i] = new byte[matrixRowSize];
            for (int j = 0; j < matrixRowSize; j++)
            {
                matrix[i][j] = data[i*matrixRowSize + j];
            }
        }
        
        return matrix;
    }
}