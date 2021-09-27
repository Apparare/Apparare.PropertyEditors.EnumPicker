using Umbraco.Core.PropertyEditors;

namespace Umbraco.Web.UI
{
    public class EnumPickerConfiguration
    {
        [ConfigurationField("assembly", "Assembly", "textstring", Description = "Enter the name of the assembly where the enum is located.")]
        public string Assembly { get; set; }

        [ConfigurationField("enum", "Enum", "textstring", Description = "Enter the name of the enum.")]
        public string Enum { get; set; }
    }
}