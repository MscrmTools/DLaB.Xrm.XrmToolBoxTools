﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DLaB.XrmToolBoxCommon.Forms;
using NuGet;

namespace DLaB.XrmToolBoxCommon.Editors
{
    internal class EntitiesHashEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var set = (HashSet<string>) value ?? new HashSet<string>();
            if (!(context?.Instance is IGetPluginControl getter))
            {
                throw new InvalidOperationException("Context Instance did not implement IGetPluginControl.  Unable to determine plugin to connect with.");
            }
            using (var dialog = new SpecifyEntitiesDialog(getter.GetPluginControl()) { SpecifiedEntities = set})
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    set = dialog.SpecifiedEntities;
                }

            }
            return set; // can also replace the wrapper object here
        }
    }
}
