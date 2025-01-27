using Gostinka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Gostinka.Models;

namespace Gostinka.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        GostinkaContext context;
        public BookingWindow()
        {
            context = new GostinkaContext();
            InitializeComponent();
            roomComboBox.ItemsSource = context.Rooms.Include(c => c.Category).Include(s => s.Status).AsNoTracking().ToList();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void roomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomComboBox.SelectedItem != null)
            {
                Room room = roomComboBox.SelectedItem as Room;
                roomCategory.Text = room.Category.CategoryName;
                roomDesctiption.Text = room.Category.Description;
            }
        }
    }
}
