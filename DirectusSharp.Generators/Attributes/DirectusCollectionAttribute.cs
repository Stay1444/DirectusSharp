using System;

namespace DirectusSharp.Generators.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DirectusCollectionAttribute(string name) : Attribute
    {
        public string Name { get; private set; } = name;
    }
}