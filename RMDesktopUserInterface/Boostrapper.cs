using Caliburn.Micro;
using RMDesktopUserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUserInterface
{
    // Thiết lập bắt đầu ứng dụng với Caliburn.Micro framework
    public class Boostrapper:BootstrapperBase
    {

        // Tạo container
        private SimpleContainer _container = new SimpleContainer();

        //Thiết lập môi trường ứng dụng
        public Boostrapper() 
        {
            Initialize();
        }

        // Tạo các DI 
        protected override void Configure()
        {
            _container.Instance(_container);

            _container.Singleton<IWindowManager, WindowManager>().Singleton<IEventAggregator, EventAggregator>();

            // Kiểm tra các lớp đang chạy
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
