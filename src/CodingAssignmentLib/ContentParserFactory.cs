using System;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib
{
    // Content parser factory with registration
	public static class ContentParserFactory
	{
        private static readonly Dictionary<string, Type> contentParsers = new Dictionary<string, Type>();

        public static void RegisterContentParser(string type, Type parserClass)
        {
            contentParsers.Add(type, parserClass);
        }

        public static IContentParser? CreateContentParser(string type)
        {
            if (!contentParsers.TryGetValue(type, out var contentParserClass))
            {
                throw new ArgumentException("Invalid content parser type: " + type);
            }

            if (contentParserClass is null)
            {
                throw new ArgumentException("No content parser found for type: " + type);
            }

            return (IContentParser?) Activator.CreateInstance(contentParserClass);
        }
    }
}

