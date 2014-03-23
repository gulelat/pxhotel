using System;
using System.Web;
using System.Web.Mvc;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Enums;

namespace PX.Web.ViewModels
{
    public class ViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ViewModelBase class.
        /// </summary>
        public ViewModelBase()
        {
            _currentController = (ControllerBase)HttpContext.Current.Items[DefaultConstants.PxHotelCurrentController];
        }
        #endregion

        #region Private Properties

        private readonly ControllerBase _currentController;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        public String Message
        {
            get
            {
                if (_currentController == null)
                    return null;

                return (string)_currentController.TempData[DefaultConstants.SuccessMessage];
            }
            set
            {
                if (_currentController == null)
                    return;

                _currentController.TempData[DefaultConstants.SuccessMessage] = value;
            }
        }

        public ResponseStatusEnums ResponseStatusEnums { get; set; }

        #endregion
    }
}