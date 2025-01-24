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
        List<Booking> bookings;

        public BookingsWorkWindow()
        {
            InitializeComponent();

            context = new GostinkaContext();

            bookings = context.Bookings
                .Include(r => r.Room)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .ToList();

            if (bookings.Count > 0)
            {
                bookingsList.ItemsSource = bookings;
            }
        }

        //private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DatePicker date = sender as DatePicker;

        //    if (date.SelectedDate <= DateTime.Now)
        //    {
        //        MessageBox.Show("Нельзя выбрать прошлую дату");
        //        date.SelectedDate = DateTime.Now;
        //    }
        //}

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDate != null & endDate != null)
            {
                DateTime? start = startDate.SelectedDate;
                DateTime? end = endDate.SelectedDate;

                List<Booking> filteredBookingList = new List<Booking>();

                foreach (Booking booking in bookings)
                {
                    DateTime arrivalDate = booking.ArrivalDate.Value.ToDateTime(new TimeOnly());

                    // выбрать брони в определенный период
                    if (arrivalDate >= start & arrivalDate <= end)
                    {
                        filteredBookingList.Add(booking);
                    }
                }

                if (filteredBookingList.Count > 0) {
                    MessageBox.Show($"Найдено {filteredBookingList.Count} совпадений", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    bookingsList.ItemsSource = filteredBookingList;
                }
                else
                {
                    MessageBox.Show("Не обнаружено совпадений", "Провал", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startDate != null && endDate != null)
            {
                if (endDate.SelectedDate < startDate.SelectedDate)
                {
                    MessageBox.Show("Указан недостижимый период.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    endDate.SelectedDate = startDate.SelectedDate;
                }   
            }
        }

        private void showAllButton_Click(object sender, RoutedEventArgs e)
        {
            bookingsList.ItemsSource = bookings;
        }
    }
}