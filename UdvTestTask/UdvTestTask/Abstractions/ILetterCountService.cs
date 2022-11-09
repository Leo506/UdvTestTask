using UdvTestTask.Models;

namespace UdvTestTask.Abstractions;

public interface ILetterCountService
{
    Dictionary<char, int> Count(params PostModel[] posts);
}