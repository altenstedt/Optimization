using System.Windows;
using System.Windows.Controls;

namespace Measure.Controls
{
    /// <summary>
    /// A control to provide a visual indicator when an application is busy.
    /// </summary>
    [TemplateVisualState(Name = "Busy", GroupName = "BusyStatusStates")]
    [TemplateVisualState(Name = "Idle", GroupName = "BusyStatusStates")]
    [TemplateVisualState(Name = "Visible", GroupName = "VisibilityStates")]
    [TemplateVisualState(Name = "Hidden", GroupName = "VisibilityStates")]
    public class BusyIndicator : ContentControl
    {
        /// <summary>
        /// Identifies the IsBusy dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(false, OnIsBusyChanged));

        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        /// <summary>
        /// Gets or sets a value indicating whether the busy indicator should show.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }

            set
            {
                SetValue(IsBusyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the BusyContent is visible.
        /// </summary>
        protected bool IsContentVisible { get; set; }

        /// <summary>
        /// Overrides the OnApplyTemplate method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChangeVisualState(false);
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            IsContentVisible = IsBusy;

            ChangeVisualState(true);
        }

        /// <summary>
        /// Changes the control's visual state(s).
        /// </summary>
        /// <param name="useTransitions">True if state transitions should be used.</param>
        protected virtual void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsBusy ? "Busy" : "Idle", useTransitions);
            VisualStateManager.GoToState(this, IsContentVisible ? "Visible" : "Hidden", useTransitions);
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="d">BusyIndicator that changed its IsBusy.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator)d).OnIsBusyChanged(e);
        }
    }
}
