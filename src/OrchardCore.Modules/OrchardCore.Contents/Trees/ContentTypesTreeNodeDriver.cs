using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentTree.Models;
using OrchardCore.ContentTree.Trees;
using OrchardCore.ContentTree.ViewModels;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Navigation;

namespace OrchardCore.Contents.Trees
{
    public class ContentTypesTreeNodeDriver : DisplayDriver<MenuItem, ContentTypesTreeNode>
    {
        public override IDisplayResult Display(ContentTypesTreeNode treeNode)
        {
            return Combine(
                View("ContentTypesTreeNode_Fields_TreeSummary", treeNode).Location("TreeSummary", "Content"),
                View("ContentTypesTreeNode_Fields_TreeThumbnail", treeNode).Location("TreeThumbnail", "Content")
            );
        }

        public override IDisplayResult Edit(ContentTypesTreeNode treeNode)
        {
            return Initialize<ContentTypesTreeNodeViewModel>("ContentTypesTreeNode_Fields_TreeEdit", model =>
            {
                model.ShowAll = treeNode.ShowAll;
                model.ContentTypes = treeNode.ContentTypes;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypesTreeNode treeNode, IUpdateModel updater)
        {
            // Initializes the value to empty otherwise the model is not updated if no type is selected.
            treeNode.ContentTypes = Array.Empty<string>();

            await updater.TryUpdateModelAsync(treeNode, Prefix, x => x.ShowAll, x => x.ContentTypes);
            return Edit(treeNode);
        }
    }
}
