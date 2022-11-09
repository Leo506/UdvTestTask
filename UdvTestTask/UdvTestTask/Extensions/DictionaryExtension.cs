namespace UdvTestTask.Extensions;

public static class DictionaryExtension
{
    public static bool IsEqualsToDict<TKey, TValue>(this IDictionary<TKey, TValue> dict1,
        IDictionary<TKey, TValue> dict2)
    {
        if (dict1.Count != dict2.Count)
            return false;

        foreach (var pair in dict1)
        {
            if (!dict2.ContainsKey(pair.Key))
                return false;
            
            if (!dict2[pair.Key]!.Equals(dict1[pair.Key]))
                return false;
        }

        return true;
    }
}