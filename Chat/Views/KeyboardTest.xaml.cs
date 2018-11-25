using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KeyboardTest : ContentPage
	{

        List<int> itemsSource = new List<int>();

		public KeyboardTest ()
		{
			InitializeComponent ();
            for (int i = 0; i < 5; i++)
            {
                itemsSource.Add(i);
            }
            listView.ItemsSource = itemsSource;

        }
	}
}