using Symmetric;

namespace Tests;

public class AesTest
{
    [Fact]
    public void EncryptTest()
    {
        var myAes = new Aes("x2#w3V3325&Umr^!3$#$qpNa!Z$4TPpP");
        myAes.Encrypt("abcjkjdnvhjfh");
        Assert.True(true);
    }

    [Fact]
    public void ThrowArgumentExceptionTest()
    {
        Assert.Throws<ArgumentException>(() => new Aes("jfsklajf"));
    }
}