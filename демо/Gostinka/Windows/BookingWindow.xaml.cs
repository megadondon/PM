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
using System.Windows.Media.Animation;
using System.Collections.Immutable;

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
            roomComboBox.ItemsSource = context.Rooms
                .Include(c => c.Category)
                .Include(s => s.RoomsStatuses).ThenInclude(s => s.Status)
                .AsNoTracking().ToList();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (firstnameTextBox.Text != "" && lastnameTextBox.Text != "" & patronymicTextBox.Text != "")
            {
                int id_room = (roomComboBox.SelectedItem as Room).IdRoom;
                int client_id;
                decimal? price = (roomComboBox.SelectedItem as Room).Category.PricePerDay;
                DateOnly start = new DateOnly(startDate.SelectedDate.Value.Year, startDate.SelectedDate.Value.Month, startDate.SelectedDate.Value.Day);
                DateOnly end = new DateOnly(endDate.SelectedDate.Value.Year, endDate.SelectedDate.Value.Month, endDate.SelectedDate.Value.Day); 

                User guest = new User()
                {
                    Firstname = firstnameTextBox.Text,
                    Lastname = lastnameTextBox.Text,
                    Patronymic = patronymicTextBox.Text,
                    Role = "Гость"
                };
                if (context.Users.FirstOrDefault(f => 
                f.Firstname == guest.Firstname & 
                f.Lastname == guest.Lastname & 
                f.Patronymic == guest.Patronymic) == null)
                {
                    context.Users.Add(guest);
                    context.SaveChanges();

                    client_id = context.Users.FirstOrDefault(f =>
                                f.Firstname == guest.Firstname &
                                f.Lastname == guest.Lastname &
                                f.Patronymic == guest.Patronymic).IdUser;
                }
                else
                {
                    MessageBox.Show("Клиент не был найден в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Booking booking = new Booking()
                {
                    RoomId = id_room,
                    ClientId = client_id,
                    ArrivalDate = start,
                    DepartureDate = end,
                    Amount = price * (end.DayOfYear - start.DayOfYear)
                };

                // замена статуса на занят
                RoomsStatus roomsStatus = context.RoomsStatuses.FirstOrDefault(r => r.RoomId == id_room);
                roomsStatus.StatusId = context.Statuses.FirstOrDefault(s => s.StatusName == "Занят").IdStatus;

                roomsStatus.StatusDate = DateOnly.FromDateTime(DateTime.Now);

                context.RoomsStatuses.Add(roomsStatus);
                context.Bookings.Add(booking);

                context.SaveChanges();
            }
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить бронирование?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
