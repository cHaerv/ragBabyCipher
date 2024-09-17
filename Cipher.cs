using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagBabyCipher
{
    public static class Cipher
    {
        public static string Encode(string text, string key)
        {
            string cipherAlphabet = keyedAlphabet(key);
            string[] words = text.Split(' ');
            List<string> encodedWords = new List<string>();

            foreach (string word in words)
            {
                char[] encodedWord = new char[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    char currentChar = word[i];
                    if (char.IsLetter(currentChar))
                    {
                        int index = cipherAlphabet.IndexOf(char.ToLower(currentChar));
                        int shiftedIndex = i + 1;
                        int newIndex = (index + shiftedIndex) % cipherAlphabet.Length;
                        char newChar = cipherAlphabet[newIndex];
                        encodedWord[i] = char.IsUpper(currentChar) ? char.ToUpper(newChar) : newChar;
                    }
                    else
                    {
                        encodedWord[i] = currentChar;
                    }

                }
                encodedWords.Add(new string(encodedWord));
            }
            return string.Join(" ", encodedWords);

        }


        public static string Decode(string text, string key)
        {
            string cipherAlphabet = keyedAlphabet(key);
            char[] decodedText = new char[text.Length];
            int letterIndex = 1;

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                if (char.IsLetter(currentChar))
                {
                    int index = cipherAlphabet.IndexOf(char.ToLower(currentChar));
                    int newIndex = (index - letterIndex + cipherAlphabet.Length) % cipherAlphabet.Length;
                    char newChar = cipherAlphabet[newIndex];
                    decodedText[i] = char.IsUpper(currentChar) ? char.ToUpper(newChar) : newChar;
                    letterIndex++;
                }
                else
                {
                    decodedText[i] = currentChar;
                    letterIndex = 1;
                }

            }

            return new string(decodedText);
        }

        public static string keyedAlphabet(string key)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            HashSet<char> addedChars = new HashSet<char>();
            List<char> keyedList = new List<char>();

            foreach (char letter in key)
            {
                if (addedChars.Add(letter))
                {
                    keyedList.Add(letter);
                }
            }

            foreach (char letter in alphabet)
            {
                if (addedChars.Add(letter))
                {
                    keyedList.Add(letter);
                }
            }
            return new string(keyedList.ToArray());
        }
    }
}
