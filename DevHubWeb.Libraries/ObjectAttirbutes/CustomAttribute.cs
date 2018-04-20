using System;

namespace DevHubWeb.Libraries.ObjectAttirbutes
{
    public class StringValueAttribute : Attribute
    {
        private readonly string _Value;

        public StringValueAttribute(string value)
        {
            this._Value = value;
        }

        public string Value { get { return this._Value; } }
    }

    public class HtmlClassAttribute : Attribute
    {
        private readonly string _Value;

        public HtmlClassAttribute(string value)
        {
            _Value = value;
        }

        public string Value { get { return this._Value; } }
    }
}
