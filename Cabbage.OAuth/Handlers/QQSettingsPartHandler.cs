﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Cabbage.OAuth.Models;

namespace Cabbage.OAuth.Handlers
{
    [OrchardFeature("Cabbage.OAuth")]
    public class QQSettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }

        public QQSettingsPartHandler(IRepository<QQSettingsPartRecord> repository)
        {
            Filters.Add(new ActivatingFilter<QQSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
            T = NullLocalizer.Instance;
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("QuickLogOn")) { Id = "QuickLogOn" });
        }
    }
}
