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
        List<Room> selectedRooms;
        List<Room> rooms;
        decimal? totalAmount;

        public BookingWindow()
        {
            context = new GostinkaContext();
            InitializeComponent();
            
            rooms = context.Rooms.Include(rs => rs.RoomsStatuses).ThenInclude(s => s.Status).Include(c => c.Category).ToList();

            roomComboBox.ItemsSource = rooms.Where(s => s.RoomsStatuses.FirstOrDefault(f => f.Status.StatusName == "Чистый") != null).ToList();
            categoryComboBox.ItemsSource = context.Categories.ToList();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите забронировать номер?", "", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                if (firstnameTextBox.Text != "" && lastnameTextBox.Text != "" & patronymicTextBox.Text != "")
                {
                    int id_room = (roomComboBox.SelectedItem as Room).IdRoom;
                    int client_id;
                    DateOnly start = new DateOnly(startDate.SelectedDate.Value.Year, startDate.SelectedDate.Value.Month, startDate.SelectedDate.Value.Day);
                    DateOnly end = new DateOnly(endDate.SelectedDate.Value.Year, endDate.SelectedDate.Value.Month, endDate.SelectedDate.Value.Day);
                    
                    User guest = new User()
                    {
                        Firstname = firstnameTextBox.Text,
                        Lastname = lastnameTextBox.Text,
                        Patronymic = patronymicTextBox.Text,
                        Role = "Гость"
                    };

                    var user = context.Users.FirstOrDefault(f =>
                                f.Firstname == guest.Firstname &
                                f.Lastname == guest.Lastname &
                                f.Patronymic == guest.Patronymic);

                    if (user == null)
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
                        MessageBox.Show("Клиент был найден в базе данных. Бронирование будет осуществляться для существующего пользователя.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        client_id = user.IdUser;
                    }

                    Booking booking = new Booking()
                    {
                        RoomId = id_room,
                        ClientId = client_id,
                        ArrivalDate = start,
                        DepartureDate = end,
                        Amount = totalAmount
                    };

                    // замена статуса на занят
                    RoomsStatus roomsStatus = context.RoomsStatuses.FirstOrDefault(r => r.RoomId == id_room);
                    roomsStatus.StatusId = context.Statuses.FirstOrDefault(s => s.StatusName == "Занят").IdStatus;

                    roomsStatus.StatusDate = DateOnly.FromDateTime(DateTime.Now);

                    context.RoomsStatuses.Update(roomsStatus);

                    context.Bookings.Add(booking);

                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Данные не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Номер забронирован", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить бронирование?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category? category = categoryComboBox.SelectedItem as Category;

            selectedRooms = rooms.Where(p => p.Category.CategoryName == category.CategoryName).ToList();
            roomComboBox.ItemsSource = selectedRooms;
            roomDesctiption.Text = category.Description;
        }

        private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startDate != null && endDate != null && roomComboBox.SelectedItem != null && categoryComboBox != null)
            {
                DateTime? start = startDate.SelectedDate;
                DateTime? end = endDate.SelectedDate;
                decimal? price = (categoryComboBox.SelectedItem as Category).PricePerDay;

                if (start < end)
                {
                    totalAmount = price * (end.Value.DayOfYear - start.Value.DayOfYear);
                    totalText.Text = $"Итого: {totalAmount} руб.";
                }
                else
                {
                    MessageBox.Show("Дата начала периода бронирования больше, чем конец периода бронирования. Укажите другое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    endDate.SelectedDate = startDate.SelectedDate.Value.AddDays(1);
                }
            }
        }
    }
}
