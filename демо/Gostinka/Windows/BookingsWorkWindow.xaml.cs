﻿using Gostinka.Models;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

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
                .OrderByDescending(o => o.ArrivalDate)
                .ToList();

            if (bookings.Count > 0)
            {
                bookingsList.ItemsSource = bookings;
            }
        }

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

                if (filteredBookingList.Count > 0)
                {
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
            bookingsList.ItemsSource = context.Bookings
                .Include(r => r.Room)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .OrderByDescending(o => o.ArrivalDate)
                .ToList();
        }

        private void makeButton_Click(object sender, RoutedEventArgs e)
        {
            new BookingWindow().Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (bookingsList.SelectedItem != null)
            {
                Booking booking = bookingsList.SelectedItem as Booking;

                if (MessageBox.Show("Вы уверены, что хотите отменить бронирование?\n\n" +
                    $"Номер брони: {booking.IdBooking}\n" +
                    $"Номер комнаты: {booking.Room.RoomNumber}\n" +
                    $"Категория: {booking.Room.Category.CategoryName}", "Подтверждение отмены", MessageBoxButton.YesNo, MessageBoxImage.Information)
                    == MessageBoxResult.Yes)
                {
                    context.Bookings.Remove(booking);
                    context.SaveChanges();

                    MessageBox.Show("Бронь была отменена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}