using System;
using ThuyTienNguyen_CoursePlannerApp.Services;
using ThuyTienNguyen_CoursePlannerApp.Models;
using ThuyTienNguyen_CoursePlannerApp.Views;
using Xamarin.Forms;

namespace ThuyTienNguyen_CoursePlannerApp
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeData();
            InitializeComponent();
           // VMControls.startupNotifications();

            
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
