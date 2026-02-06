using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ContainerValidatorApp
{
    public static class ContainerValidator
    {
        private static readonly Dictionary<char, int> LetterValues = new Dictionary<char, int>
        {
            {'A',10}, {'B',12}, {'C',13}, {'D',14}, {'E',15}, {'F',16}, {'G',17}, {'H',18},
            {'I',19}, {'J',20}, {'K',21}, {'L',23}, {'M',24}, {'N',25}, {'O',26}, {'P',27},
            {'Q',28}, {'R',29}, {'S',30}, {'T',31}, {'U',32}, {'V',34}, {'W',35}, {'X',36},
            {'Y',37}, {'Z',38}
        };

        /// <summary>
        /// Valideert een ISO 6346 containernummer (bijv. MSKU1234565).
        /// Lengte: 10 (zonder check digit) of 11 karakters.
        /// Retourneert of het geldig is + berekende check digit + bericht.
        /// </summary>
        public static ValidationResult Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult { IsValid = false, Message = "❌ Ongeldig: lege invoer" };
            }

            string container = input.ToUpperInvariant().Replace(" ", "").Trim();

            if (container.Length != 10 && container.Length != 11)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "❌ Ongeldig: moet 10 of 11 karakters zijn\n   Formaat: 4 letters + 6 cijfers + (optioneel) check digit"
                };
            }

            // Controleer formaat: eerste 4 letters, volgende 6 cijfers
            if (!Regex.IsMatch(container.Substring(0, 4), @"^[A-Z]{4}$") ||
                !Regex.IsMatch(container.Substring(4, 6), @"^\d{6}$"))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "❌ Ongeldig formaat\n   Eerste 4 karakters moeten letters zijn, volgende 6 cijfers"
                };
            }

            string firstTen = container.Substring(0, 10);
            int calculatedCheckDigit = CalculateCheckDigit(firstTen);

            if (container.Length == 10)
            {
                // Geen check digit meegegeven → toon berekende
                return new ValidationResult
                {
                    IsValid = false,
                    Message = $"ℹ️ Check digit ontbreekt\n   Berekend: {calculatedCheckDigit}\n   Volledig nummer: {firstTen}{calculatedCheckDigit}",
                    CalculatedCheckDigit = calculatedCheckDigit
                };
            }

            // Check digit aanwezig → valideren
            char lastChar = container[10];
            if (!char.IsDigit(lastChar))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "❌ Ongeldig: check digit moet een cijfer (0-9) zijn"
                };
            }

            int givenCheckDigit = lastChar - '0';

            if (calculatedCheckDigit == givenCheckDigit)
            {
                return new ValidationResult
                {
                    IsValid = true,
                    Message = $"✅ GELDIG\n   Check digit {givenCheckDigit} klopt!",
                    CalculatedCheckDigit = calculatedCheckDigit
                };
            }

            return new ValidationResult
            {
                IsValid = false,
                Message = $"❌ ONGELDIG check digit\n   Verwacht: {calculatedCheckDigit}\n   Gegeven: {givenCheckDigit}",
                CalculatedCheckDigit = calculatedCheckDigit
            };
        }

        /// <summary>
        /// Berekent de ISO 6346 check digit voor de eerste 10 karakters.
        /// Weights: 1,2,4,8,16,32,64,128,256,512 (van links naar rechts).
        /// </summary>
        public static int CalculateCheckDigit(string firstTen)
        {
            if (firstTen.Length != 10) throw new ArgumentException("Moet exact 10 karakters zijn", nameof(firstTen));

            long sum = 0;

            for (int i = 0; i < 10; i++)
            {
                char c = firstTen[i];
                int value;

                if (char.IsLetter(c))
                {
                    if (!LetterValues.TryGetValue(c, out value))
                        throw new ArgumentException($"Ongeldige letter: {c}");
                }
                else if (char.IsDigit(c))
                {
                    value = c - '0';
                }
                else
                {
                    throw new ArgumentException($"Ongeldig karakter: {c}");
                }

                long weight = 1L << i;  // 2^i : 1,2,4,...,512
                sum += value * weight;
            }

            int remainder = (int)(sum % 11);
            return remainder == 10 ? 0 : remainder;
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? CalculatedCheckDigit { get; set; }
    }
}