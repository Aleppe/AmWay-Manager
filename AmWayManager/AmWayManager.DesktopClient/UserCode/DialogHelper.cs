using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Threading;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace LightSwitchApplication.UserCode
{
    public class DialogHelper

    {

        private Microsoft.LightSwitch.Client.IScreenObject _screen;

        private Microsoft.LightSwitch.Client.IVisualCollection _collection;

        private string _dialogName;

        private bool _isEditing = false;

        private IEntityObject _entity;

        public DialogHelper(Microsoft.LightSwitch.Client.IVisualCollection visualCollection, string dialogName)

        {

            _screen = visualCollection.Screen;

            _collection = visualCollection;

            _dialogName = dialogName;

        }

        public void InitializeUI()

        {

            //This code may not work in Beta 2.  Fixed in final release.  

            _screen.FindControl(_dialogName).ControlAvailable += (object sender, ControlAvailableEventArgs e) =>

            {

                System.Windows.Controls.ChildWindow childWindow = (ChildWindow)e.Control;

                childWindow.HasCloseButton = false;

                childWindow.Closed += (object s1, EventArgs e1) =>

                {

                    if (_entity != null)
                    {

                        ((IEditableObject)_entity.Details).CancelEdit();

                    }

                };

            };

        }

        public bool CanEditSelected()

        {

            return _collection.CanEdit && ((_collection.SelectedItem != null));

        }

        public bool CanAdd()

        {

            return _collection.CanAddNew;

        }

        public object AddEntity()

        {

            _isEditing = false;

            object result=_collection.AddNew();

            _screen.FindControl(_dialogName).DisplayName = "Add " + _collection.Details.GetModel().ElementType.Name;

            BaseOpenDialog();

            return result;

        }



        public void EditSelectedEntity()

        {

            _isEditing = true;

            _screen.FindControl(_dialogName).DisplayName = "Edit " + _collection.Details.GetModel().ElementType.Name;

            BaseOpenDialog();

        }

        private void BaseOpenDialog()

        {

            _entity = (IEntityObject)_collection.SelectedItem;

            if (_entity != null)
            {

                Dispatchers.Main.Invoke(() =>

                {

                    ((IEditableObject)_entity.Details).EndEdit();

                    ((IEditableObject)_entity.Details).BeginEdit();

                });

                _screen.OpenModalWindow(_dialogName);

            }

        }

        public void DialogOk()

        {

            if (_entity != null && _entity.Details.ValidationResults.Errors != null)
            {

                Dispatchers.Main.Invoke(() => { ((IEditableObject)_entity.Details).EndEdit(); });

                _screen.CloseModalWindow(_dialogName);

            }

        }

        public void DialogCancel()

        {

            if (_entity != null)
            {

                Dispatchers.Main.Invoke(() => { ((IEditableObject)_entity.Details).CancelEdit(); });

                if (_isEditing == false)
                {

                    _entity.Delete();

                }

                _screen.CloseModalWindow(_dialogName);

            }

        }

    }
}
