using Gostinka.Models;
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
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionTranslators.Internal;
using System.Reflection.Metadata;

namespace Gostinka.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookingsWorkWindow.xaml
    /// </summary>
    public partial class BookingsWorkWindow : Window
    {
        GostinkaContext context;
        public BookingsWorkWindow()
        {
            InitializeComponent();

            context = new GostinkaContext();

            List<Booking> bookings = context.Bookings
                .Include(r => r.Room)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .ToList();
            if (bookings.Count > 0)
            {
                bookingsList.ItemsSource = bookings;
            }
        }

        private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker date = sender as DatePicker;

            if (date.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("Нельзя выбрать прошлую дату");
                date.SelectedDate = DateTime.Now;
            }
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDate != null & endDate != null)
            {
                // доделать
            }
        }
    }
}