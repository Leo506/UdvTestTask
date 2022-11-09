namespace UdvTestTask.Models;

public partial class LettersCount
{
    public int Id { get; set; }
    public string LettersData { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        if (obj is not LettersCount lettersCount)
            return false;

        return Id == lettersCount.Id && LettersData == lettersCount.LettersData;
    }
}