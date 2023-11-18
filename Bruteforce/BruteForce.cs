using System.Security.Cryptography;

namespace Bruteforce;

public class BruteForce
{
    private readonly SHA256 _sha256;
    private bool _flag;
    private string? _value;
    private int _length;
    
    public string? Value
    {
        get => _value;
    }

    public BruteForce(int length = 5)
    {
        _sha256 = SHA256.Create();
        _length = length;
    }

    public void ApplyForce(string target, int startIndex = 0, int lastIndex = 26)
    {
        _flag = false;
        Force(new List<byte>(), target, startIndex, lastIndex);
    }

    private void Force(List<byte> current, string target, int startIndex = 0, int lastIndex = 26)
    {
        if(_flag)
            return;
        
        if (current.Count != _length)
        {
            for (var i = startIndex; i < lastIndex; i++)
            {
                current.Add((byte)('a' + i));
                Force(current, target);
                current.RemoveAt(current.Count - 1);
            }
        }
        
        var buffer = current.ToArray();
        var computeHash = _sha256.ComputeHash(buffer);
    
        var tmp = Convert.ToHexString(computeHash).ToLower();

        if (tmp != target) 
            return;
        
        _value = new string(current.Select(x => (char)x).ToArray());
        _flag = true;
    }
}