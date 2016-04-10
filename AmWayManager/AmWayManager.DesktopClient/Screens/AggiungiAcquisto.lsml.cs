using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;
using LightSwitchApplication.UserCode;

namespace LightSwitchApplication
{
    public partial class AggiungiAcquisto
    {
        private DialogHelper ArticoliAcquistatiDialogHelper;
        private DialogHelper ArticoloDialogHelper;

        partial void AggiungiAcquisto_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            
            this.AcquistoProperty = new Acquisto();
            ArticoliAcquistatiDialogHelper = new DialogHelper(ArticoliAcquistatiCollection, "CustomAggiungiArticoloAcquistato");
            ArticoloDialogHelper = null;
        }

        partial void AggiungiAcquisto_Created()
        {
            ArticoliAcquistatiDialogHelper.InitializeUI();
        }

        partial void AggiungiAcquisto_Saved()
        {
            
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.AcquistoProperty);
        }

        partial void ArticoliAcquistatiCollectionAddAndEditNew_CanExecute(ref bool result)
        {
            ArticoliAcquistatiDialogHelper.CanAdd();
        }

        partial void ArticoliAcquistatiCollectionAddAndEditNew_Execute()
        {
            ArticoliAcquistati ArticoloAcquistato = (ArticoliAcquistati)ArticoliAcquistatiDialogHelper.AddEntity();
            
            //ArticoloAcquistato.Acquisto = AcquistoProperty;
        }

        partial void ArticoliAcquistatiCollectionEditSelected_CanExecute(ref bool result)
        {
            result = ArticoliAcquistatiDialogHelper.CanEditSelected();
            
        }

        partial void ArticoliAcquistatiCollectionEditSelected_Execute()
        {
            
            ArticoliAcquistatiDialogHelper.EditSelectedEntity();
        }

        partial void AggiungiArticoloAcquistatoOK_Execute()
        {
            
            ArticoliAcquistatiDialogHelper.DialogOk();
        }

        partial void AggiungiArticoloAcquistatoCancel_Execute()
        {
            
            ArticoliAcquistatiDialogHelper.DialogCancel();
        }

        partial void AddArticolo_Execute()
        {
            // Scrivere qui il codice.
            ArticoloDialogHelper  = new DialogHelper(ArticoloSet, "CustomAggiungiArticolo");
            ArticoloDialogHelper.CanAdd();
            ArticoloDialogHelper.AddEntity();
        }

        partial void AggiungiArticoloCancel_Execute()
        {
            ArticoloDialogHelper.DialogCancel();
        }

        partial void AggiungiArticoloOk_Execute()
        {
            ArticoloDialogHelper.DialogOk();
        }
    }
}