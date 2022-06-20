using ThuyTienNguyen_CoursePlannerApp.Views;
using Xamarin.Forms;

namespace ThuyTienNguyen_CoursePlannerApp
{
    public partial class App : Application
    {
        public App()
        {
           
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#002F51")
            };

        }
      

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
           
        }

     
        
      
        


    }
}
