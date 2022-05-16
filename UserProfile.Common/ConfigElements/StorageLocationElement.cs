using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfile.Common.ConfigElements
{

    public class LocationConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["name"])); }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("value", IsKey = false, IsRequired = true)]
        public string Value
        {
            get { return ((string)(base["value"])); }
            set { base["value"] = value; }
        }
    }

    [ConfigurationCollection(typeof(LocationConfigElement))]
    public class LocationConfigElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LocationConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LocationConfigElement)(element)).Name;
        }

        public LocationConfigElement this[int idx]
        {
            get { return (LocationConfigElement)BaseGet(idx); }
        }
    }

    public class LocationConfigElementConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("LocationConfigElement")]
        public LocationConfigElementCollection CustomItems
        {
            get { return ((LocationConfigElementCollection)(base["LocationConfigElement"])); }
        }
    }    
}
