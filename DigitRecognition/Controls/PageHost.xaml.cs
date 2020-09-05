using DigitRecognition.Converters;
using DigitRecognition.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DigitRecognition.Controls
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        #region DependencyProperty
        public ApplicationPage CurrentPage
        {
            get { return (ApplicationPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(ApplicationPage), typeof(PageHost), new UIPropertyMetadata(default(ApplicationPage), null, CurrentPagePropertyChanged));


        public ViewModelBase CurrentPageViewModel
        {
            get { return (ViewModelBase)GetValue(CurrentPageViewModelProperty); }
            set { SetValue(CurrentPageViewModelProperty, value); }
        }
        // Using a DependencyProperty as the backing store for CurrentPageViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register("CurrentPageViewModel", typeof(ViewModelBase), typeof(PageHost), new UIPropertyMetadata());
        #endregion

        #region Contructor
        public PageHost()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        private static object CurrentPagePropertyChanged(DependencyObject d, object baseValue)
        {
            ApplicationPage currentPage = (ApplicationPage)d.GetValue(CurrentPageProperty);
            ViewModelBase currentPageViewModel = (ViewModelBase)d.GetValue(CurrentPageViewModelProperty);

            var pageHost = (d as PageHost).MainFrame;

            pageHost.Content = currentPage.ToBasePage(currentPageViewModel);

            return baseValue;
        }

        #endregion
    }
}
