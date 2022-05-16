using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfile.Common.ConfigElements
{

    public class QuestionConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("id", IsKey = true, IsRequired = true)]
        public string Id
        {
            get { return ((string)(base["id"])); }
            set { base["id"] = value; }
        }

        [ConfigurationProperty("questionName", IsKey = false, IsRequired = true)]
        public string QuestionName
        {
            get { return ((string)(base["questionName"])); }
            set { base["questionName"] = value; }
        }

        [ConfigurationProperty("questionText", IsKey = false, IsRequired = true)]
        public string QuestionText
        {
            get { return ((string)(base["questionText"])); }
            set { base["questionText"] = value; }
        }

        [ConfigurationProperty("answerType", IsKey = false, IsRequired = true)]
        public string AnswerType
        {
            get { return ((string)(base["answerType"])); }
            set { base["answerType"] = value; }
        }
    }

    [ConfigurationCollection(typeof(QuestionConfigElement))]
    public class QuestionConfigElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new QuestionConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((QuestionConfigElement)(element)).Id;
        }

        public QuestionConfigElement this[int idx]
        {
            get { return (QuestionConfigElement)BaseGet(idx); }
        }
    }

    public class QuestionConfigElementConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("QuestionConfigElement")]
        public QuestionConfigElementCollection CustomItems
        {
            get { return ((QuestionConfigElementCollection)(base["QuestionConfigElement"])); }
        }
    }    
}
