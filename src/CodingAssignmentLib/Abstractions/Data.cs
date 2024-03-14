namespace CodingAssignmentLib.Abstractions;

public struct Data
{
    public Data(string key, string value)
    {
        Key = key;
        Value = value;
    }
    public string Key { get; }
    public string Value { get; }
}