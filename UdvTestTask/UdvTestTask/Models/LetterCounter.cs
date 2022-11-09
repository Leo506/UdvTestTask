﻿namespace UdvTestTask.Models;

public class LetterCounter
{
    public IDictionary<char, int> Count(params PostModel[] posts)
    {
        var result = new SortedDictionary<char, int>();

        foreach (var post in posts)
        {
            if (string.IsNullOrEmpty(post.Content)) 
                continue;
            
            foreach (var c in post.Content.Where(char.IsLetterOrDigit))
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