using System;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib
{
	public static class ContentParserFactory
	{
        private static readonly Dictionary<string, Type> contentParsers = new Dictionary<string, Type>();

        public static void RegisterContentParser(string type, Type parserClass)
        {
            contentParsers.Add(type, parserClass);
        }

        public static IContentParser CreateContentParser(string type)
        {
            if (!contentParsers.TryGetValue(type, out Type contentParserClass))
            {
                throw new ArgumentException("Invalid content parser type: " + type);
            }
            return (IContentParser)Activator.CreateInstance(contentParserClass);
        }
    }
}

