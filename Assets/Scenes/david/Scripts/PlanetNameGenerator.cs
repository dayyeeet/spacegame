using UnityEngine;

class PlanetNameGenerator
{
    public PlanetNameGenerator(int seed)
    {
        Random.InitState(seed);
    }

    private static readonly string[] Consonants =
        { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "qu", "r", "s", "t", "v", "w", "x", "y", "z" };

    private static readonly string[] Vowels = { "a", "e", "i", "o", "u" };

    private static string GeneratePlanetName(int syllableCount)
    {
        string name = "";
        for (int i = 0; i < syllableCount; i++)
        {
            name += Consonants[Random.Range(0, Consonants.Length)] + Vowels[Random.Range(0, Vowels.Length)];
            if (i < syllableCount - 1 && Random.Range(0, 2) == 1)
            {
                name += Consonants[Random.Range(0, Consonants.Length)];
            }
        }

        return Capitalize(name);
    }

    public string GeneratePlanetName()
    {
        return GeneratePlanetName(Random.Range(3, 6));
    }

    private static string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s[1..];
    }
}