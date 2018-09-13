using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentTree.Models;
using OrchardCore.Environment.Navigation;

namespace OrchardCore.ContentTree.ViewModels
{
    public class EditContentTreePresetTreeNodeViewModel
    {
        public int ContentTreePresetId { get; set; }
        public string TreeNodeId { get; set; }
        public string TreeNodeType { get; set; }
        public dynamic Editor { get; set; }

        [BindNever]
        public MenuItem MenuItem { get; set; }

    }
}
