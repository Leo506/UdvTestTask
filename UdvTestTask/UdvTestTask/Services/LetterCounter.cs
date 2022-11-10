using UdvTestTask.Abstractions;
using UdvTestTask.Models;

namespace UdvTestTask.Services;

public class LetterCounter : ILetterCountService
{
    public IDictionary<char, int> Count(params PostModel[]? posts)
    {
        var result = new SortedDictionary<char, int>();
        if (posts is null)
            return result;

        foreach (var post in posts)
        {
            if (string.IsNullOrEmpty(post.Content)) 
                continue;
            
            foreach (var c in post.Content.Where(char.IsLetter))
            {
                var character = char.ToLower(c);
                if (result.ContainsKey(character))
                    result[character]++;
                else
                    result.Add(character, 1);
            }
        }

        return result;
    }
}