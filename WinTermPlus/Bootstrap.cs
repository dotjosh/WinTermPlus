using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;
using WinTermPlus.Infrastructure;
using WinTermPlus.UI.Converters;
using WinTermPlus.UI.ViewModels;

namespace WinTermPlus
{
    public class Bootstrap : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrap()
        {
            Initialize();
        }
        protected override void Configure()
        {
            _container = new SimpleContainer();

            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<Config>();

            _container
               .PerRequest<ShellViewModel>()
               .PerRequest<ConfigViewModel>();

            ConventionManager.ApplyValueConverter = CreateApplyValueConverter();
        }

        private static IValueConverter RangeBaseToPercentageConverter = new RangeBaseToPercentageConverter();

        private Action<Binding, DependencyProperty, PropertyInfo> CreateApplyValueConverter()
        {
            var original = ConventionManager.ApplyValueConverter;
            return (binding, wpfProperty, targetProperty) =>
            {
                if (wpfProperty == RangeBase.ValueProperty && typeof(Percentage).IsAssignableFrom(targetProperty.PropertyType))
                {
                    binding.Converter = RangeBaseToPercentageConverter;
                }
                else
                {
                    original(binding, wpfProperty, targetProperty);
                }
            };
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            Application.Current.MainWindow.Hide();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "An error as occurred", MessageBoxButton.OK);
        }
    }
}
