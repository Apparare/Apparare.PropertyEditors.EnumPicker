using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Logging;

namespace Umbraco.Web.UI
{
    [DataEditor(
        alias: "Apparare.PropertyEditors.EnumPicker",
        name: "Enum Picker",
        view: "~/App_Plugins/Apparare.PropertyEditors.EnumPicker/views/enumpicker.editor.html",
        Group = "Rich Content",
        Icon = "icon-indent")]
    public class EnumPicker : DataEditor
    {
        public EnumPicker(ILogger logger) : base(logger) { }

        protected override IConfigurationEditor CreateConfigurationEditor()
        {
            return new EnumPickerConfigurationEditor();
        }
    }
}
