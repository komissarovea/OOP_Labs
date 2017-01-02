using System;
using System.Xml.Serialization;

namespace Lab3
{
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Kub))]
    [XmlInclude(typeof(Parabola))]
    public abstract class Function
    {
        public abstract int Calculate(int x);
    }
}
