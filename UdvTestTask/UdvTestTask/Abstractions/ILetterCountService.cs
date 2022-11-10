using UdvTestTask.Models;

namespace UdvTestTask.Abstractions;

public interface ILetterCountService
{
    IDictionary<char, int> Count(params PostModel[] posts);
}