using System;
using System.Collections.Generic;

namespace TradingBlockApiTestHarness.DTO.User
{
    public class CreateUserRequestSecurityChallenge
    {
        private static readonly Dictionary<string, byte> SecurityQuestionTypeToId = new Dictionary<string, byte>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "childhood-pet-name",               1 },
            { "mothers-maiden-name",              2 },
            { "birth-city",                       3 },
            { "favorite-sports-team",             4 },
            { "paternal-grandmothers-first-name", 5 },
            { "childhood-future-job-wish",        6 },
            { "favorite-book",                    7 },
            { "drivers-license-last-4",           8 },
            { "fathers-middle-name",              9 },
        };

        public string SecurityQuestionType { get; set; }

        public string Answer { get; set; }

        public override string ToString()
        {
            return $"{nameof(SecurityQuestionType)}: {SecurityQuestionType}, {nameof(Answer)}: {Answer}";
        }

        public static string ConvertIdToType(byte id)
        {
            foreach (var pair in SecurityQuestionTypeToId)
            {
                if (pair.Value == id)
                {
                    return pair.Key;
                }
            }
            return null;
        }

        public static byte ConvertTypeToId(string type)
        {
            return SecurityQuestionTypeToId[type];
        }
    }
}
